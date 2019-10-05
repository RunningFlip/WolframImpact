using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SelectionSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(SelectionComponent), typeof(UserComponent), typeof(InputComponent), typeof(CameraComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            SelectionComponent selection = ComponentManager.GetComponent<SelectionComponent>(entityID);
            UserComponent user = ComponentManager.GetComponent<UserComponent>(entityID);
            InputComponent input = ComponentManager.GetComponent<InputComponent>(entityID);
            CameraComponent cam = ComponentManager.GetComponent<CameraComponent>(entityID);

            if (!selection.init)
            {
                selection.init = true;
                SelectionComponentSetup(selection);
            }


            //Resets ans clears the current selection.
            if (selection.resetSelection)
            {
                selection.resetSelection = false;
                ResetCurrentSelection(selection);
            }

            if (selection.destroySelection)
            {
                selection.destroySelection = false;
                DestroyCurrentSelection(selection, user.userObject.userIndex);
            }

            //Selects one or more specific unit/s or building/s.
            if (selection.selectByClick)
            {
                selection.selectByClick = false;
                TryGetSelectable(selection, cam, SelectionType.Click, user.userObject.userIndex);
            }

            //Num selections
            if (selection.selectionSetNum != -1)
            {
                if (selection.continueSelection)
                {
                    SaveSelectionByNumber(selection, user.userObject.userIndex); //Saves a selection to a specific num on the keyboard.
                }
                else
                {
                    GetSelectionByNumber(selection, user.userObject.userIndex); //Gets a selection to a specific num on the keyboard.
                }
                selection.selectionSetNum = -1;
            }

            //Drag Selection
            if (!selection.dragStarted && selection.mouseDown)
            {
                selection.dragStarted = true;
                selection.mouseStartPosition = Input.mousePosition;
            }
            if (selection.dragStarted && !selection.mouseDown)
            {
                selection.dragStarted = false;
                selection.currentMousePosition = Input.mousePosition;

                if (DragSelectionIsValid(selection.mouseStartPosition, selection.currentMousePosition, selection.magnitudeToSelect))
                {
                    bool objectsFound = DragSelectionBoxCast(selection, cam);
                    if (objectsFound)
                    {
                        TryGetSelectable(selection, cam, SelectionType.DragSelection, user.userObject.userIndex);
                    }
                }
            }
        }

        /*
         * Muss unbedingt in OnGUI passieren!!
         * 
        //Draws the selection field
        if (selComp.mouseDown && selComp.dragStarted)
        {
            //Helper to draw the drag-selection-field.
            DragSelectionHelper.DrawDragSelectionBox(selComp.mouseStartPosition, Input.mousePosition, selComp.dragSelectionBackgroundColor, selComp.dragSelectionStrokeColor, selComp.dragSelectionStrokeThickness);
        }
        */
    }


    /// <summary>
    /// Setups the given selection component.
    /// </summary>
    /// <param name="_selectionComponent"></param>
    private void SelectionComponentSetup(SelectionComponent _selectionComponent)
    {
        //Parameters
        GameParameter gameParameter = GameController.Instance.GameParameter;
        _selectionComponent.selectionLayerMask = gameParameter.selectionLayerMask;
        _selectionComponent.groundLayerMask = gameParameter.groundLayerMask;
        _selectionComponent.maxSelectableObjects = gameParameter.maxSelectableObjects;
        _selectionComponent.maxRayCastLength = gameParameter.maxMouseRayCastLength;
        _selectionComponent.magnitudeToSelect = gameParameter.magnitudeToSelect;
        _selectionComponent.dragSelectionBackgroundColor = gameParameter.backgroundColor;
        _selectionComponent.dragSelectionStrokeColor = gameParameter.strokeColor;
        _selectionComponent.dragSelectionStrokeThickness = gameParameter.strokeThickness;

        //Saved selections
        _selectionComponent.selectionSetArray = new SelectionSetArray(gameParameter.maxSavableSelections);

        //Array
        _selectionComponent.dragSelectionObjects = new GameObject[_selectionComponent.maxSelectableObjects];
    }


    #region Selection

    /// <summary>
    /// Resets the current selection list.
    /// </summary>
    private void ResetCurrentSelection(SelectionComponent _selection)
    {
        for (int i = _selection.selectionTable.Count - 1; i > -1; i--)
        {
            RemoveSelectionFromList(_selection, _selection.selectedEntityIds[i]);
        }
    }


    /// <summary>
    /// Destroy the objects of the current selection.
    /// </summary>
    private void DestroyCurrentSelection(SelectionComponent _selection, int _userIndex)
    {
        for (int i = 0; i < _selection.selectedEntityIds.Count; i++)
        {
            int id = _selection.selectedEntityIds[i];

            if (ComponentManager.GetComponent<FactionComponent>(id).userIndex == _userIndex)
            {
                ComponentManager.GetComponent<HealthComponent>(id).currentHealth = 0;
            }
        }
    }


    /// <summary>
    /// Tries to get a selectable on mouse position.
    /// </summary>
    private void TryGetSelectable(SelectionComponent _selection, CameraComponent _cam, SelectionType _selectionType, int _userIndex)
    {
        switch (_selectionType)
        {
            case SelectionType.Click:
                GameObject foundedObject = SelectionRaycast(_selection, _cam);

                if (foundedObject != null)
                {
                    int selectionId = GetEntityId(ref foundedObject);
                    AddSelectionToList(_selection, selectionId, _selection.continueSelection, _userIndex);
                }
                break;


            case SelectionType.DragSelection:
                for (int i = 0; i < _selection.dragSelectionObjects.Length; i++)
                {
                    if (_selection.dragSelectionObjects[i] == null) break;

                    int newSelectedId = GetEntityId(ref _selection.dragSelectionObjects[i]);

                    if (i == 0) AddSelectionToList(_selection, newSelectedId, _selection.continueSelection, _userIndex);
                    else AddSelectionToList(_selection, newSelectedId, true, _userIndex);
                }

                for (int i = 0; i < _selection.dragSelectionObjects.Length; i++)
                {
                    _selection.dragSelectionObjects[i] = null;
                }
                break;
        }
    }


    /// <summary>
    /// Adds a specific selection to the current selection list.
    /// </summary>
    /// <param name="_selection"></param>
    private void AddSelectionToList(SelectionComponent _selection, int _selectedEntityId, bool _continueSelection, int _userIndex)
    {
        if (_continueSelection && _selection.selectedEntityIds.Contains(_selectedEntityId)) return; //Already selected!

        if (_selection.enemySelected)
        {
            _selection.enemySelected = false;
            ResetCurrentSelection(_selection);
        }

        if (ComponentManager.GetComponent<FactionComponent>(_selectedEntityId).userIndex != _userIndex) //Not my unit or building!
        {
            _selection.enemySelected = true;
            if (_selection.continueSelection) ResetCurrentSelection(_selection);
        }

        if (!_continueSelection) ResetCurrentSelection(_selection);
        else if (_selection.selectedEntityIds.Count == _selection.maxSelectableObjects - 1) return; //Max selections to add already reached!

        InformationPackage informationPackage = ComponentManager.GetComponent<InformationComponent>(_selectedEntityId).informationPackage;
        _selection.selectionTable.Add(_selectedEntityId, informationPackage);

        RegisterEvents(_selection, _selectedEntityId);
        _selection.selectedEntityIds.Add(_selectedEntityId);

        _selection.onSelectionChanged.Invoke(); //Event call
    }


    /// <summary>
    /// Removes a speicific selection from the current selection list.
    /// </summary>
    /// <param name="_selection"></param>
    private void RemoveSelectionFromList(SelectionComponent _selection, int _entityId)
    {
        UnregisterEvents(_selection, _entityId);
        _selection.selectedEntityIds.Remove(_entityId);
        _selection.selectionTable.Remove(_entityId);

        _selection.onSelectionChanged.Invoke();
    }


    /// <summary>
    /// Returns the entity id of the selected object.
    /// </summary>
    /// <param name="_selectedObject"></param>
    /// <returns></returns>
    private int GetEntityId(ref GameObject _selectedObject)
    {
        EntityHolderLink link = _selectedObject.GetComponent<EntityHolderLink>();
        if (link == null)
        {
            GameObjectEntityHolder entityHolder = _selectedObject.GetComponent<GameObjectEntityHolder>();
            return entityHolder.GetEntityID();
        }
        return link.entityHolder.GetEntityID();
    }

    #endregion Selection


    #region Raycasts

    /// <summary>
    /// Raycast to get an object thats metchs to the "selectionLayerMask".
    /// </summary>
    /// <returns></returns>
    private GameObject SelectionRaycast(SelectionComponent _selection, CameraComponent _cam)
    {
        if (_cam.cam == null) return null;

        RaycastHit hit;
        Ray ray = _cam.cam.ScreenPointToRay(Input.mousePosition); //Raycast

        if (Physics.Raycast(ray, out hit, _selection.maxRayCastLength, _selection.selectionLayerMask))
        {
            if (hit.transform != null)
            {
                return hit.transform.gameObject;
            }
        }
        if (!_selection.continueSelection) ResetCurrentSelection(_selection);
        return null;
    }


    /// <summary>
    /// Checks if the DragSelectionField can be used
    /// </summary>
    /// <param name="_startMousePosition"></param>
    /// <param name="_currentMousePosition"></param>
    /// <returns></returns>
    private bool DragSelectionIsValid(Vector3 _startMousePosition, Vector3 _currentMousePosition, float _magnitudeToSelect)
    {
        if (Vector3.Distance(_startMousePosition, _currentMousePosition) >= _magnitudeToSelect)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// Boxcast to get all objects from "selectionLayerMask" within the dragselectionfield. 
    /// </summary>
    /// <returns></returns>
    private bool DragSelectionBoxCast(SelectionComponent _selection, CameraComponent _cam)
    {
        //Set depth from camera to mouse positions
        _selection.mouseStartPosition.z = _cam.cam.transform.position.z;                //HAS TO BE CHANGED IF THE CAMERA ROTATES!!!!
        _selection.currentMousePosition.z = _cam.cam.transform.position.z;              //HAS TO BE CHANGED IF THE CAMERA ROTATES!!!!


        //Calculate DragSelectionField--------------------------------------------------
        Vector3 size = _cam.cam.ScreenToWorldPoint(_selection.currentMousePosition) - _cam.cam.ScreenToWorldPoint(_selection.mouseStartPosition);
        size.x = Mathf.Abs(size.x);
        size.y = Mathf.Abs(size.y);
        //size.z = Mathf.Abs(size.z);
        //Calculate DragSelectionField--------------------------------------------------


        //Calculate direction for BoxCast-----------------------------------------------
        Vector3 direction = _selection.nullVec;
        Vector3 rayVec = _selection.mouseStartPosition + ((_selection.currentMousePosition - _selection.mouseStartPosition) / 2);

        //Mathf Abs - Vector units have to be positive
        rayVec.x = Mathf.Abs(rayVec.x);
        rayVec.y = Mathf.Abs(rayVec.y);
        //rayVec.z = Mathf.Abs(rayVec.z);

        RaycastHit hit;
        Ray ray = _cam.cam.ScreenPointToRay(rayVec); //Raycast

        if (Physics.Raycast(ray, out hit, _selection.maxRayCastLength, _selection.groundLayerMask))
        {
            if (hit.transform != null)
            {
                direction = (hit.point - _cam.cam.transform.position).normalized;
            }
        }
        //Calculate direction for BoxCast-----------------------------------------------


        //Hits
        RaycastHit[] hits = Physics.BoxCastAll(_cam.cam.transform.position, size, direction, Quaternion.identity, _selection.maxRayCastLength, _selection.selectionLayerMask);

        //Resets dragselection array
        for (int i = 0; i < _selection.dragSelectionObjects.Length; i++) _selection.dragSelectionObjects[i] = null;

        //Hits found
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length && i < _selection.dragSelectionObjects.Length; i++)
            {
                _selection.dragSelectionObjects[i] = hits[i].transform.gameObject;
            }
            return true;
        }
        else
        {
            if (!_selection.continueSelection) ResetCurrentSelection(_selection);
            return false;
        }
    }

    #endregion Raycasts


    #region NumSelections

    /// <summary>
    /// Binds the current selection on a number and saves them.
    /// </summary>
    /// <param name="_number"></param>
    private void SaveSelectionByNumber(SelectionComponent _selection, int _userIndex)
    {
        if (_selection.selectionTable.Values.First().factionComponent.userIndex != _userIndex) return; //Not my unit or building!

        SelectionSetArray.ClearSet(_selection.selectionSetArray, _selection.selectionSetNum);

        for (int i = 0; i < _selection.selectedEntityIds.Count; i++)
        {
            SelectionSetArray.AddElementToSet(_selection.selectionSetArray, _selection.selectionSetNum, _selection.selectedEntityIds[i]);
        }
    }


    /// <summary>
    /// Get a selection by a specific number.
    /// </summary>
    /// <param name="_number"></param>
    private void GetSelectionByNumber(SelectionComponent _selComp, int _userIndex)
    {
        ResetCurrentSelection(_selComp);

        for (int i = 0; i < _selComp.selectionSetArray.sets[_selComp.selectionSetNum].Count; i++)
        {
            int selectionFromSet = _selComp.selectionSetArray.sets[_selComp.selectionSetNum][i];
            AddSelectionToList(_selComp, selectionFromSet, false, _userIndex);
        }
    }

    #endregion NumSelection


    #region Events

    /// <summary>
    /// Registers a speicific selection to neccessary events.
    /// </summary>
    private void RegisterEvents(SelectionComponent _selectionComponent, int _entityId)
    {     
        HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(_entityId);
        SelectionTargetComponent selectionTargetComponent = ComponentManager.GetComponent<SelectionTargetComponent>(_entityId);

        //Register the unregister by death
        healthComponent.onDeath.AddListener(delegate
        {
            UnregisterEvents(_selectionComponent, _entityId);
            RemoveSelectionFromList(_selectionComponent, _entityId);
        });

        //Sillhouette
        selectionTargetComponent.onSelectionStarts.Invoke();
    }


    /// <summary>
    /// Unegisters a speicific selection from neccessary events.
    /// </summary>
    private void UnregisterEvents(SelectionComponent _selectionComponent, int _entityId)
    {
        HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(_entityId);
        SelectionTargetComponent selectionTargetComponent = ComponentManager.GetComponent<SelectionTargetComponent>(_entityId);

        //Unregister by death
        healthComponent.onDeath.RemoveListener(delegate
        {
            UnregisterEvents(_selectionComponent, _entityId);
            RemoveSelectionFromList(_selectionComponent, _entityId);
        });

        //Sillhouette
        selectionTargetComponent.onSelectionEnds.Invoke();
    }

    #endregion Events
}
