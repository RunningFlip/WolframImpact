using UnityEngine;


[CreateAssetMenu(fileName = "Command_Stop_Task_Config", menuName = "ScriptableObjects/Command Configs/Command Stop Task Config")]
public class CommandStopTasksConfig : CommandConfig
{
    public override void CommandSetup(int _userIndex, Entity _entity, CommandTargetComponent _commandTargetComponent)
    {
        base.CommandSetup(_userIndex, _entity, _commandTargetComponent);
    }


    public override void StartCommand(GameObjectEntityHolder _targetEntityHolder, Vector3 _targetPos)
    {
        base.StartCommand(_targetEntityHolder, _targetPos);
        commandTargetComponent.stopCommand = true;
        isActive = false;
    }


    public override void UpdateCommand() { }


    public override void AfterCommand()
    {
        base.AfterCommand();
    }

}
