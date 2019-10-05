using UnityEngine;
using System;


[Serializable]
public struct Task
{
    public CommandConfig command;
    public GameObjectEntityHolder currentTargetEntityHolder;
    public Vector3 currentTargetPosition;


    public Task(CommandConfig _commandConfig, GameObjectEntityHolder _currentTargetEntityHolder, Vector3 _currentPosition)
    {
        command = _commandConfig;
        currentTargetEntityHolder = _currentTargetEntityHolder;
        currentTargetPosition = _currentPosition;
    }


    public static bool operator ==(Task s1, Task s2)
    {
        return s1.Equals(s2);
    }

    public static bool operator !=(Task s1, Task s2)
    {
        return !s1.Equals(s2);
    }


    public override bool Equals(System.Object _obj)
    {
        if (_obj == null) return false;

        Task task = (Task)_obj;

        if ((System.Object)task == null) return false;

        return (task.command == command)
            && (task.currentTargetEntityHolder == currentTargetEntityHolder)
            && (task.currentTargetPosition == currentTargetPosition);
    }


    public bool Equals(Task _task)
    {
        if ((object)_task == null)
        {
            return false;
        }
        return (_task.command == command) 
            && (_task.currentTargetEntityHolder == currentTargetEntityHolder) 
            && (_task.currentTargetPosition == currentTargetPosition);
    }


    public override int GetHashCode()
    {
        return command.GetHashCode() + currentTargetEntityHolder.GetEntityID() + currentTargetPosition.GetHashCode();
    }

}
