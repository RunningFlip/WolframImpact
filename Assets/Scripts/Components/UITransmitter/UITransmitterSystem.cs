using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UITransmitterSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(UITransmitterComponent), typeof(UserComponent), typeof(SelectionComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            UITransmitterComponent transmitter = ComponentManager.GetComponent<UITransmitterComponent>(entityID);           

            if (!transmitter.init)
            {
                transmitter.init = true;

                UserComponent user = ComponentManager.GetComponent<UserComponent>(entityID);
                SelectionComponent selection = ComponentManager.GetComponent<SelectionComponent>(entityID);

                selection.onSelectionChanged.AddListener(delegate { OnSelectionChanged(transmitter, selection, user.userObject.userIndex); });
            }
        }
    }


    /// <summary>
    /// Removes all existing listeners of the selection list
    /// </summary>
    private void BeforeSelectionChanged(UITransmitterComponent _transmitter, SelectionComponent _selection, int _userIndex)
    {
        //UI
        GameController.Instance.uiController.UpdateInfoUI(false);
        GameController.Instance.uiController.UpdateCommandUI(false);

        //Removes all listeners, if there are still elements in the list
        for (int i = 0; i < _transmitter.sortedSelections.Count; i++)
        {
            _transmitter.sortedSelections[i].healthComponent.onCurrentHealthChanged.RemoveListener(delegate 
            {
                UpdateInfoUI(_transmitter);
                UpdateCommandUI(_transmitter, _userIndex);
            });
        }
    }



    /// <summary>
    /// Sets all neccessary values to show the right informations of the selected object in the info UI.
    /// Links with events to reacts on changes.
    /// </summary>
    private void OnSelectionChanged(UITransmitterComponent _transmitter, SelectionComponent _selection, int _userIndex)
    {
        //Get Information packages
        _transmitter.sortedSelections = _selection.selectionTable.Values.ToList();

        //Before changes
        BeforeSelectionChanged(_transmitter, _selection, _userIndex);

        //Flag reset
        _transmitter.multipleInfos = false;

        //Sort list
        _transmitter.sortedSelections = _transmitter.sortedSelections.OrderByDescending(x => (int)x.typeComponent.tagType)  //Sorts at first by tagType (Units then buildings)
                            .ThenByDescending(x => x.typeComponent.isMilitaryObject)                                        //Then sorts by militaryObjects
                            .ThenBy(x => x.identifierComponent.objectName).ToList();                                        //Then sorts by objectname

        //Set flag
        if (_transmitter.sortedSelections.Count > 1)
        {
            _transmitter.multipleInfos = true;
        }

        //Adds all listeners
        for (int i = 0; i < _transmitter.sortedSelections.Count; i++)
        {
            _transmitter.sortedSelections[i].healthComponent.onCurrentHealthChanged.AddListener(delegate 
            {
                UpdateInfoUI(_transmitter);
                UpdateCommandUI(_transmitter, _userIndex);
            });
        }

        //Updates the info UI in the UIcontroller
        UpdateInfoUI(_transmitter);
        UpdateCommandUI(_transmitter, _userIndex);
    }



    /// <summary>
    /// Updates the info UI, if objects are selected.
    /// </summary>
    private void UpdateInfoUI(UITransmitterComponent _transmitter)
    {
        if (_transmitter.sortedSelections.Count > 0)
        {
            if (_transmitter.multipleInfos)
            {
                GameController.Instance.uiController.UpdateInfoUI(true, _transmitter.sortedSelections);
            }
            else
            {
                GameController.Instance.uiController.UpdateInfoUI(true, _transmitter.sortedSelections[0]);
            }
        }
        else
        {
            GameController.Instance.uiController.UpdateInfoUI(false);
        }
    }


    /// <summary>
    /// Updates the command UI, if objects are selected
    /// </summary>
    private void UpdateCommandUI(UITransmitterComponent _transmitter, int _userIndex)
    {
        if (_transmitter.sortedSelections.Count > 0)
        {
            if (_transmitter.sortedSelections[0].factionComponent.userIndex != _userIndex)
            {
                GameController.Instance.uiController.UpdateCommandUI(false);
                return;
            }
            GameController.Instance.uiController.UpdateCommandUI(true, _transmitter.sortedSelections[0].commandTargetComponent);
        }
        else
        {
            GameController.Instance.uiController.UpdateCommandUI(false);
        }
    }
}
