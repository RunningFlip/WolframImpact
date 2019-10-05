using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Command_Marker_Config", menuName = "ScriptableObjects/Command Configs/Command Marker Config")]
public class CommandMarkerConfig : CommandConfig
{
    //Components
    [NonSerialized] private MarkerComponent markerComponent;


    public override void CommandSetup(int _userIndex, Entity _entity, CommandTargetComponent _commandTargetComponent)
    {
        base.CommandSetup(_userIndex, _entity, _commandTargetComponent);

        markerComponent = ComponentManager.GetComponent<MarkerComponent>(_entity.GetEntityID());
    }


    public override void StartCommand(GameObjectEntityHolder _targetEntityHolder, Vector3 _targetPos)
    {
        base.StartCommand(_targetEntityHolder, _targetPos);

        if (!markerComponent.firstSetted)
        {
            markerComponent.firstSetted = true;
            markerComponent.markerObject.SetActive(true);
        }

        if (_targetEntityHolder != null)
        {
            markerComponent.markerTransform.position = _targetEntityHolder.transform.position;
        }
        else
        {
            markerComponent.markerTransform.position = _targetPos;
        }
        isActive = false;
    }


    public override void UpdateCommand() { }


    public override void AfterCommand()
    {
        base.AfterCommand();
    }
}
