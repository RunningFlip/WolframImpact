using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(InputComponent), typeof(CameraComponent), typeof(SelectionComponent), typeof(CommandComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            InputComponent input = ComponentManager.GetComponent<InputComponent>(entityID);
            CameraComponent cameraComponent = ComponentManager.GetComponent<CameraComponent>(entityID);
            SelectionComponent selectionComponent = ComponentManager.GetComponent<SelectionComponent>(entityID);
            CommandComponent commandComponent = ComponentManager.GetComponent<CommandComponent>(entityID);

            if (!input.init)
            {
                input.init = true;
                input.actionSet = new DefaultInputActionSet();
                input.eventSystem = EventSystem.current;

                UserComponent user = ComponentManager.GetComponent<UserComponent>(entityID);
                GameController.Instance.uiController.InitializeUI(user.userObject.userIndex, input);
            }

            #region General

            if (!input.readInput)
            {
                if (cameraComponent.useCameraMovement)
                {
                    input.camWasInactive = true;
                    cameraComponent.useCameraMovement = false;
                }
                continue;
            }
            else
            {
                if (input.camWasInactive)
                {
                    input.camWasInactive = false;
                    cameraComponent.useCameraMovement = true;
                }
            }

            bool wasOverUI = false;

            #endregion General

            #region Camera

            //Camera
            if (input.actionSet.movement.IsPressed)                                                                 //Movement
            {
                Vector3 movementMovement = new Vector3(input.actionSet.movement.X, 0, input.actionSet.movement.Y);
                cameraComponent.movement = movementMovement;
            }
            if (input.actionSet.zoom.IsPressed) cameraComponent.zoom = -1 * input.actionSet.zoom;                   //Zoom   
            if (input.actionSet.rotation.IsPressed) cameraComponent.rotation = input.actionSet.rotation;            //Rotation   
            if (input.actionSet.cameraReset.WasPressed)                                                             //Reset Camera
            {
                cameraComponent.resetZoom = true;                                               //Zoom reset
                cameraComponent.resetRotation = true;                                           //Rotation reset
            }
            if (input.actionSet.deactivateMouseMovement.WasPressed)                             //Disable mouse camera movement
            {
                cameraComponent.useCameraMovement = false;
            }
            if (input.actionSet.deactivateMouseMovement.WasReleased)                            //Enables mouse camera movement
            {
                cameraComponent.useCameraMovement = true;
            }

            #endregion Camera

            #region Keyboard

            //Gameplay
            //---Keyboard
            if (input.actionSet.continueInput.WasPressed)                                     //Continue input -> pressed
            {
                selectionComponent.continueSelection = true;
                commandComponent.continueCommanding = true;
                input.continueIsPressed = true;
            }
            if (input.actionSet.continueInput.WasReleased)                                    //Continue input -> released
            {
                selectionComponent.continueSelection = false;
                commandComponent.continueCommanding = false;
                input.continueIsPressed = false;
            }

            //Numbers
            input.pressedNum = -1;
            for (int j = 0; j < input.actionSet.numbers.Length; j++)
            {
                if (input.actionSet.numbers[j].WasPressed)
                {
                    input.pressedNum = j;
                    selectionComponent.selectionSetNum = input.pressedNum;
                    break;
                }
            }

            //Shortcuts
            if (input.actionSet.stopCurrentActions.WasPressed)                                //Stop current action
            {
                input.onStopPressed.Invoke();

                for (int j = 0; j < selectionComponent.selectedEntityIds.Count; j++)
                {
                    int id = selectionComponent.selectedEntityIds[j];
                    CommandTargetComponent commandTargetComponent = ComponentManager.GetComponent<CommandTargetComponent>(id);
                    commandTargetComponent.stopCommand = true;
                }
            }
            if (input.actionSet.focusCamera.WasPressed)                                       //Focus camera
            {
                cameraComponent.focus = true;
            }

            if (input.listenForGameplayCancel)                                                //Cancel current selection
            {
                if (input.actionSet.cancel.WasPressed || input.actionSet.command.WasPressed)
                {
                    input.onCancelPressedGameplay.Invoke();
                }
            }
            else
            {
                if (input.actionSet.cancel.WasPressed)
                {
                    input.onCancelPressed.Invoke();
                    selectionComponent.resetSelection = true;
                }
            }

            if (input.actionSet.destroy.WasPressed)
            {
                selectionComponent.destroySelection = true;
            }

            //UI
            if (input.actionSet.context.WasPressed) Debug.Log("Context pressed");             //Context menu

            #endregion Keyboard

            #region Mouse

            //---Mouse
            if (input.inDoubleClick) input.passedDoubleClickTime += Time.deltaTime;

            if (input.actionSet.selection.WasPressed)                                         //Selection
            {
                if (input.eventSystem.IsPointerOverGameObject())
                {
                    wasOverUI = true;
                }
                else
                {
                    if (input.readSelectionInput)
                    {
                        selectionComponent.selectByClick = true;
                        selectionComponent.mouseDown = true;
                    }
                    else
                    {
                        selectionComponent.mouseDown = false;
                    }
                }
                input.selectWasPressed = true;
                input.onSelectWasPressed.Invoke();

                CheckDoubleClick(input);                                                     //DoubleClick
            }

            if (input.actionSet.selection.WasReleased)
            {
                if (!wasOverUI && input.readCommandInput) selectionComponent.mouseDown = false;
                input.selectWasPressed = false;
                input.onSelectWasReleased.Invoke();

                TryStartDoubleClick(input);                                                  //DoubleClick
            }


            if (input.actionSet.command.WasPressed)                                         //Command
            {                                         
                if (input.eventSystem.IsPointerOverGameObject()) wasOverUI = true;
                else
                {
                    if (input.readCommandInput && commandComponent != null && selectionComponent.selectedEntityIds.Count > 0)
                    {
                        commandComponent.giveCommand = true;
                    }
                }
                input.commandPressed = true;
                input.onCommandWasPressed.Invoke();
            }

            if (input.actionSet.command.WasReleased)
            {
                input.commandPressed = false;
                input.onCommandWasReleased.Invoke();
            }

            #endregion Mouse
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_inputComponent"></param>
    private void CheckDoubleClick(InputComponent _inputComponent)
    {
        _inputComponent.doubleClickWasSuccessful = false;
        if (_inputComponent.inDoubleClick)
        {
            if (_inputComponent.passedDoubleClickTime <= _inputComponent.doubleClickTime)
            {
                _inputComponent.onDoubleClickPressed.Invoke();
                _inputComponent.doubleClickWasSuccessful = true;
            }
            _inputComponent.inDoubleClick = false;
            _inputComponent.passedDoubleClickTime = 0;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_inputComponent"></param>
    private void TryStartDoubleClick(InputComponent _inputComponent)
    {
        if (!_inputComponent.doubleClickWasSuccessful)
        {
            _inputComponent.inDoubleClick = true;
        }
    }
}
