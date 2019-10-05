using System;
using UnityEngine;


public abstract class CommandConfig : ScriptableObject
{
    [Header("External Command Informations")]
    public bool longPressAllowed;
    public CommandTriggerType triggerType = CommandTriggerType.NeedsTarget;
    public Texture2D cursorTexture;

    [Header("Command Informations")]
    public bool cancelAllTasksBefore;
    public string commandLetter;
    public string commandName;
    [TextArea]
    public string discription;

    [Header("Command Visual UI")]
    public bool useVisuals;
    public GameObject nodePrefab;
    public GameObject strokePrefab;


    //Flags
    [NonSerialized] public bool isActive;
    [NonSerialized] public bool isTriggeredByButton;
    [NonSerialized] private bool waiting;

    //Jobs
    [NonSerialized] private DefaultJob waitForInputJob;

    //Targets
    [NonSerialized] public GameObjectEntityHolder targetEntityHolder;
    [NonSerialized] public Vector3 targetPosition;

    //Vectors
    [NonSerialized] private Vector3 nullVec = Vector3.zero;

    //User
    [NonSerialized] public int userIndex;

    //Components
    [NonSerialized] public CommandTargetComponent commandTargetComponent;
    [NonSerialized] public InputComponent inputComponent;


    public virtual void CommandSetup(int _userIndex, Entity _targetEntity, CommandTargetComponent _commandTargetComponent)
    {
        userIndex = _userIndex;
        commandTargetComponent = _commandTargetComponent;
    }


    public void StartActionBeforeCommand(InputComponent _inputComponent)
    {
        if (waiting) return;

        waiting = true;
        inputComponent = _inputComponent;

        StartActionBeforeCommand();


        waitForInputJob = new DefaultJob(delegate { WhileActionBeforeCommand(); });
    }

    public void StopActionBeforeCommand()
    {
        waiting = false;
        inputComponent = null;

        EndActionBeforeCommand();

        if (waitForInputJob != null)
        {
            waitForInputJob.CancelJob();
            waitForInputJob = null;
        }
    }


    protected virtual void StartActionBeforeCommand() { }
    protected virtual void WhileActionBeforeCommand() { }
    protected virtual void EndActionBeforeCommand() { }


    public virtual void StartCommand(GameObjectEntityHolder _targetEntityHolder, Vector3 _targetPosition)
    {
        if (waiting) StopActionBeforeCommand();

        isActive = true;

        targetEntityHolder = _targetEntityHolder;
        targetPosition = _targetPosition;
    }


    public abstract void UpdateCommand();


    public virtual void AfterCommand()
    {
        if (waiting) StopActionBeforeCommand();

        isTriggeredByButton = false;

        targetPosition = nullVec;
        targetEntityHolder = null;
    }


}
