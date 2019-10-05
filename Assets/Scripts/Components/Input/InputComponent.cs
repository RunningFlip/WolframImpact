using System;
using UnityEngine;
using UnityEngine.EventSystems;


[CreateAssetMenu(fileName = "InputComponent", menuName = "ScriptableObjects/Components Configs/InputComponent")]
public class InputComponent : EntityComponent
{
    [Header("Reading Status")]
    public bool readInput;
    public bool readSelectionInput = true;
    public bool readCommandInput = true;

    [Header("Continue Activity")]
    public bool continueIsPressed;

    [Header("Cancel Status")]
    public bool listenForGameplayCancel;


    //Action Set   
    [NonSerialized] public DefaultInputActionSet actionSet;

    //Flag
    [NonSerialized] public bool camWasInactive;

    //Click Flags
    [NonSerialized] public bool selectWasPressed;
    [NonSerialized] public bool clickSelectWasReleased;
    [NonSerialized] public bool commandPressed;
    [NonSerialized] public bool clickCommandWasReleased;

    //Number pressed
    [NonSerialized] public int pressedNum = -1;

    //Timers
    [NonSerialized] public bool inDoubleClick;
    [NonSerialized] public bool doubleClickWasSuccessful;
    [NonSerialized] public float doubleClickTime = 0.3f;
    [NonSerialized] public float passedDoubleClickTime = 0;

    //EventSystem
    [NonSerialized] public EventSystem eventSystem;

    //Events
    [NonSerialized] public SimpleEvent onSelectWasPressed = new SimpleEvent();
    [NonSerialized] public SimpleEvent onSelectWasReleased = new SimpleEvent();

    [NonSerialized] public SimpleEvent onCommandWasPressed = new SimpleEvent();
    [NonSerialized] public SimpleEvent onCommandWasReleased = new SimpleEvent();

    [NonSerialized] public SimpleEvent onDoubleClickPressed = new SimpleEvent();

    [NonSerialized] public SimpleEvent onStopPressed = new SimpleEvent();
    [NonSerialized] public SimpleEvent onCancelPressed = new SimpleEvent();
    [NonSerialized] public SimpleEvent onCancelPressedGameplay = new SimpleEvent();
}
