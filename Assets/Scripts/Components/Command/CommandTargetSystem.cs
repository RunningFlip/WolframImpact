using System.Collections.Generic;
using UnityEngine;


public class CommandTargetSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(CommandTargetComponent), typeof(FactionComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            CommandTargetComponent commandTarget = ComponentManager.GetComponent<CommandTargetComponent>(entityID);

            if (!commandTarget.init)
            {
                Setup(commandTarget, entities[i]);
                continue;
            }

            Update(commandTarget);
        }
    }


    private void Setup(CommandTargetComponent _commandTarget, Entity _entity)
    {
        int entityId = _entity.GetEntityID();

        FactionComponent faction = ComponentManager.GetComponent<FactionComponent>(entityId);

        if (faction.init)
        {
            //Wait until poolholder holds objects          
            PoolHolderComponent poolHolder = ComponentManager.GetComponent<PoolHolderComponent>(entityId);

            if (poolHolder != null && poolHolder.poolingObjects.Length == 0) return;

            //Setups the command configs
            SetupCommands(_entity, faction.userIndex, _commandTarget);
            _commandTarget.init = true;
        };
    }


    private void Update(CommandTargetComponent _commandTarget)
    {
        if (_commandTarget.clearAllAndAddNew)                               //Clears all tasks and adds a new one
        {
            CancelAction(_commandTarget, true);
            return;
        }

        if (_commandTarget.inCommand)
        {
            if (_commandTarget.stopCommand)                                 //Stops all commands
            {
                _commandTarget.stopCommand = false;
                CancelAction(_commandTarget, true);
                return;
            }

            if (!_commandTarget.currentTask.command.isActive)               //Cancels the current command
            {
                CancelAction(_commandTarget, false);
                return;
            }
            else
            {
                _commandTarget.currentTask.command.UpdateCommand();         //Updates the current command
                return;
            }
        }
        else
        {
            CheckWaitingTasks(_commandTarget);                              //Is looking for waiting tasks if the current one if finished
        }

        if (!_commandTarget.inCommand && _commandTarget.stopCommand)        //Resets the stop command config if nothing no command if active, to avoid resetting a new task.
        {
            _commandTarget.stopCommand = false;
        }
    }


    /// <summary>
    /// Inits the setup for all commands.
    /// </summary>
    private void SetupCommands(Entity _entity, int _userIndex, CommandTargetComponent _commandTarget)
    {
        for (int i = 0; i < _commandTarget.commands.Count; i++)                                 //Creates instances of each command configs
        {
            CommandConfig configInstance = UnityEngine.Object.Instantiate(_commandTarget.commands[i].commandConfig);
            Command commandInstance = new Command(configInstance, _commandTarget.commands[i].commandTypes);

            _commandTarget.commands[i] = commandInstance;
        }

        for (int i = 0; i < _commandTarget.commands.Count; i++)                                 //Setups the command configs and links the entity to them
        {
            _commandTarget.commands[i].commandConfig.CommandSetup(_userIndex, _entity, _commandTarget);
        }
    }


    /// <summary>
    /// Checks the waiting tasks and sets the first one to the current task.
    /// </summary>
    private void CheckWaitingTasks(CommandTargetComponent _commandTarget)
    {
        if (_commandTarget.currentTask.command != null) CancelAction(_commandTarget, false);        //Cancels ONLY current task (false = only current one)

        if (_commandTarget.currentTask.command == null && _commandTarget.waitingTasks.Count > 0)
        {
            if (_commandTarget.waitingTasks[0].command == null)
            {
                _commandTarget.waitingTasks.RemoveAt(0);                                            //Removes first waiting task if it has no command
                return;
            }

            _commandTarget.currentTask = _commandTarget.waitingTasks[0];                            //First waiting task gets current task
            _commandTarget.waitingTasks.Remove(_commandTarget.currentTask);                         //Removes the first waiting task because it is executing.
            _commandTarget.currentTask.command.StartCommand(_commandTarget.currentTask.currentTargetEntityHolder, _commandTarget.currentTask.currentTargetPosition);    //Start command 

            _commandTarget.inCommand = true;                                                        //Flag
        }
    }


    /// <summary>
    /// Will be called from the actionConfig to end the current action or all waiting tasks.
    /// </summary>
    private void CancelAction(CommandTargetComponent _commandTarget, bool _allActions)
    {
        _commandTarget.onCancelCommands.Invoke();

        if (_commandTarget.currentTask.command != null) _commandTarget.currentTask.command.AfterCommand();  //After commands

        _commandTarget.inCommand = false;
        _commandTarget.currentTask = _commandTarget.nullTask;

        if (_allActions) _commandTarget.waitingTasks.Clear();                                           //Clear commands

        if (_commandTarget.clearAllAndAddNew)                                                           //Adds new task if ordered.
        {
            _commandTarget.clearAllAndAddNew = false;
            _commandTarget.waitingTasks.Add(_commandTarget.newFirstTask);
            _commandTarget.newFirstTask = _commandTarget.nullTask;
        }
    }
}
