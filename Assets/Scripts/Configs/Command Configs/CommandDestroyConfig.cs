using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Command_Destroy_Config", menuName = "ScriptableObjects/Command Configs/Command Destroy Config")]
public class CommandDestroyConfig : CommandConfig
{
    //Components
    [NonSerialized] private HealthComponent healthComponent;


    public override void CommandSetup(int _userIndex, Entity _entity, CommandTargetComponent _commandTargetComponent)
    {
        base.CommandSetup(_userIndex, _entity, _commandTargetComponent);

        healthComponent = ComponentManager.GetComponent<HealthComponent>(_entity.GetEntityID());
    }


    public override void StartCommand(GameObjectEntityHolder _targetEntityHolder, Vector3 _targetPos)
    {
        base.StartCommand(_targetEntityHolder, _targetPos);

        if (healthComponent == null)
        {
            ComponentManager.CreateComponent<DeathComponent>(_targetEntityHolder.GetEntityID());
        }
        else
        {
            healthComponent.currentHealth = 0;
        }
        isActive = false;
    }


    public override void UpdateCommand() { }


    public override void AfterCommand()
    {
        base.AfterCommand();
    }
}
