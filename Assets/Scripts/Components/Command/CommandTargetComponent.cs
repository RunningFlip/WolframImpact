using UnityEngine;
using System;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "CommandTargetComponent", menuName = "ScriptableObjects/Components Configs/CommandTargetComponent")]
public class CommandTargetComponent : EntityComponent
{
    [Header("Action Configs")]
    public List<Command> commands = new List<Command>();

    [Header("Tasks")]
    public Task currentTask;
    [Space(10)]
    public List<Task> waitingTasks = new List<Task>();


    //Flags  
    [NonSerialized] public bool inCommand;
    [NonSerialized] public bool clearAllAndAddNew;
    [NonSerialized] public bool stopCommand;

    //Tasks
    [NonSerialized] public Task nullTask = new Task(null, null, Vector3.zero);
    [NonSerialized] public Task newFirstTask;

    //Vector
    [NonSerialized] public Vector3 nullVec = Vector3.zero;

    //Events
    [NonSerialized] public SimpleEvent onCancelCommands = new SimpleEvent();
}
