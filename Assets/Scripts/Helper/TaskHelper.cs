using UnityEngine;


public static class TaskHelper {

	public static Task AddTask(CommandTargetComponent _commandTargetComponent, CommandType _commandType, GameObjectEntityHolder _currentTargetEntityHolder, Vector3 _currentTargetPosition) {
        for (int i = 0; i < _commandTargetComponent.commands.Count; i++) {
            if (_commandTargetComponent.commands[i].commandTypes.Contains(_commandType)) {

                Task newTask = new Task(_commandTargetComponent.commands[i].commandConfig, _currentTargetEntityHolder, _currentTargetPosition);
                _commandTargetComponent.waitingTasks.Add(newTask);

                return newTask;
            }
        }
        return new Task();
    }


    public static Task AddTaskWithConfig(CommandTargetComponent _commandTargetComponent, CommandConfig _actionConfig, GameObjectEntityHolder _currentTargetEntityHolder, Vector3 _currentTargetPosition) {
        Task newTask = new Task(_actionConfig, _currentTargetEntityHolder, _currentTargetPosition);
        _commandTargetComponent.waitingTasks.Add(newTask);

        return newTask;
    }


    public static Task AddTaskAtIndex(CommandTargetComponent _commandTargetComponent, CommandType _commandType, GameObjectEntityHolder _currentTargetEntityHolder, Vector3 _currentTargetPosition, int _index) {
        for (int i = 0; i < _commandTargetComponent.commands.Count; i++) {
            if (_commandTargetComponent.commands[i].commandTypes.Contains(_commandType)) {

                Task newTask = new Task(_commandTargetComponent.commands[i].commandConfig, _currentTargetEntityHolder, _currentTargetPosition);
                _commandTargetComponent.waitingTasks.Insert(_index, newTask);

                return newTask;
            }
        }
        return new Task();
    }


    public static Task AddTaskWithConfigAtIndex(CommandTargetComponent _commandTargetComponent, CommandConfig _actionConfig, GameObjectEntityHolder _currentTargetEntityHolder, Vector3 _currentTargetPosition, int _index) {
        Task newTask = new Task(_actionConfig, _currentTargetEntityHolder, _currentTargetPosition);
        _commandTargetComponent.waitingTasks.Insert(_index, newTask);

        return newTask;
    }


    public static void ClearTasksAndAddNewTaskWithConfig(CommandTargetComponent _commandTargetComponent, CommandConfig _actionConfig, GameObjectEntityHolder _currentTargetEntityHolder, Vector3 _currentTargetPosition) {
        _commandTargetComponent.clearAllAndAddNew = true;

        Task newTask = new Task(_actionConfig, _currentTargetEntityHolder, _currentTargetPosition);
        _commandTargetComponent.newFirstTask = newTask;
    }


    public static void ClearTasksAndAddNewTask(CommandTargetComponent _commandTargetComponent, CommandType _commandType, GameObjectEntityHolder _currentTargetEntityHolder, Vector3 _currentTargetPosition) {
        for (int i = 0; i < _commandTargetComponent.commands.Count; i++) {
            if (_commandTargetComponent.commands[i].commandTypes.Contains(_commandType)) {

                _commandTargetComponent.clearAllAndAddNew = true;

                Task newTask = new Task(_commandTargetComponent.commands[i].commandConfig, _currentTargetEntityHolder, _currentTargetPosition);
                _commandTargetComponent.newFirstTask = newTask;
            }
        }
    }


    public static void NewCurrentTaskWithPushBack(CommandTargetComponent _commandTargetComponent, Task _task) {
        Task currentTask = _commandTargetComponent.currentTask;
        _commandTargetComponent.currentTask.command.isActive = false;

        currentTask.command.isTriggeredByButton = false;

        _commandTargetComponent.waitingTasks.Insert(0, currentTask);
        _commandTargetComponent.waitingTasks.Insert(0, _task);
    }


    public static Task CreatTask(CommandTargetComponent _commandTargetComponent, CommandType _commandType, GameObjectEntityHolder _currentTargetEntityHolder, Vector3 _currentTargetPosition) {
        CommandConfig config = null;

        for (int i = 0; i < _commandTargetComponent.commands.Count; i++) {
            if (_commandTargetComponent.commands[i].commandTypes.Contains(_commandType)) {
                config = _commandTargetComponent.commands[i].commandConfig;
            }
        }
        Task task = new Task(config, _currentTargetEntityHolder, _currentTargetPosition);

        return task;
    }


    public static Task CreatTaskWithConfig(CommandConfig _actionConfig, GameObjectEntityHolder _currentTargetEntityHolder, Vector3 _currentTargetPosition) {
        Task task = new Task(_actionConfig, _currentTargetEntityHolder, _currentTargetPosition);

        return task;
    }


    /// <summary>
    /// Gets the index of the task.
    /// index = -1 => currentTask
    /// index > -1 => waitingTask
    /// </summary>
    /// <param name="_commandTargetComponent"></param>
    /// <param name="_task"></param>
    /// <returns></returns>
    public static int GetTaskIndex(CommandTargetComponent _commandTargetComponent, Task _task) {
        int index = -2;
        bool found = false;

        if (_commandTargetComponent.currentTask == _task) {
            found = true;
            index = -1;
        }

        if (!found) {
            for (int i = 0; i < _commandTargetComponent.waitingTasks.Count; i++) {
                if (_commandTargetComponent.waitingTasks[i] == _task) {
                    index = i;
                    break;
                }
            }
        }      
        return index;
    }


    /// <summary>
    /// Gets the task by index.
    /// index = -1 => currentTask
    /// index > -1 => waitingTask
    /// </summary>
    /// <param name="_comandTargetComponent"></param>
    /// <param name="_index"></param>
    /// <returns></returns>
    public static Task GetTaskAtIndex(CommandTargetComponent _comandTargetComponent, int _index) {
        Task task = new Task();

        if (_index == -1) {
            task = _comandTargetComponent.currentTask;
        }
        if (_index >= 0) {
            task = _comandTargetComponent.waitingTasks[_index];
        }
        return task;
    }
}
