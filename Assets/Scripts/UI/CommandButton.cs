using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;


public class CommandButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button values")]
    public Button button;
    public Text text;

    [Header("Command Popup")]
    public CommandPopup commandPopup;


    [NonSerialized]
    public CommandTriggerType currentTriggerType;


    //Flags
    private bool isWaiting;
    private bool countdownStarted;
    private bool showsInfoPopup;

    //Cursor
    private Texture2D defaultCursor;
    private Vector2 cursorPivot;

    //RayCast
    private float maxRayCastLength;
    private LayerMask mouseClickLayerMask;
    private Camera mainCamera;

    //Vector
    private Vector3 nullVec = Vector3.zero;

    //Components
    private InputComponent inputComponent;

    //Action config reference
    private Command command;
    private CommandTargetComponent commandTargetComponent;

    //EventSystem
    private EventSystem eventSystem;

    //PopupTimer
    private float popupTimer;


    /// <summary>
    /// Initializes the setup for this CommandButton.
    /// </summary>
    /// <param name="_inputComponent"></param>
    public void InitializeCommandButton(InputComponent _inputComponent)
    {
        //Components
        inputComponent = _inputComponent;
        eventSystem = EventSystem.current;

        //Cursor
        defaultCursor = GameController.Instance.GameParameter.cursorTexture;

        //RayCast
        maxRayCastLength = GameController.Instance.GameParameter.maxMouseRayCastLength;
        mouseClickLayerMask = GameController.Instance.GameParameter.mouseClickLayerMask;
        mainCamera = Camera.main;

        //Popup
        popupTimer = GameController.Instance.GameParameter.popupTimer;

        //Popup template
        command = new Command(null, new List<CommandType>());

        //Event
        button.onClick.AddListener(delegate { OnClick(); });
    }




    #region Popup

    /// <summary>
    /// Mouse enters CommandButton and triggers Popup.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!showsInfoPopup && !countdownStarted)
        {
            countdownStarted = true;
            StartCoroutine("PopupCountdown");
        }
    }

    /// <summary>
    /// Mouse exits the CommandButton and disabled the Popup.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (countdownStarted && !showsInfoPopup)
        {
            StopCoroutine("PopupCountdown");
            countdownStarted = false;
        }
        else if (!countdownStarted && showsInfoPopup)
        {
            showsInfoPopup = false;
            commandPopup.SetPopUp(false);
        }
    }

    /// <summary>
    /// Starts the countdown before the popup will be shown.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PopupCountdown()
    {
        yield return new WaitForSeconds(popupTimer);
        countdownStarted = false;
        showsInfoPopup = true;

        commandPopup.SetPopUp(true, command.commandConfig.commandName, command.commandConfig.discription, command.commandConfig.commandLetter);
    }


    /// <summary>
    /// Resets the Popup
    /// </summary>
    private void ResetPopup()
    {
        StopCoroutine("PopupCountdown");
        countdownStarted = false;
        showsInfoPopup = false;

        commandPopup.SetPopUp(false);
    }

    #endregion Popup




    #region CommandConfig

    /// <summary>
    /// Connects the CommandButton with the given command.
    /// </summary>
    /// <param name="_command">Command of the button.</param>
    public void ConnectWithActionConfig(Command _command)
    {
        //Rendering
        gameObject.SetActive(true);

        //Command
        command = _command;
        commandTargetComponent = command.commandConfig.commandTargetComponent;

        //General
        currentTriggerType = command.commandConfig.triggerType;
        text.text = command.commandConfig.commandLetter;

        //Cursor
        if (command.commandConfig.cursorTexture)
        {
            cursorPivot = new Vector2(command.commandConfig.cursorTexture.width / 2, command.commandConfig.cursorTexture.height / 2);
        }
    }


    /// <summary>
    /// Disconnects the CommandButton from it's command.
    /// </summary>
    public void DisconnectFromActionConfig()
    {
        //Rendering
        gameObject.SetActive(false);

        //Command
        commandTargetComponent = null;
        command = new Command(null, new List<CommandType>());

        //General
        text.text = "-";
        currentTriggerType = CommandTriggerType.NeedsTarget;

        //Cursor
        Cursor.SetCursor(defaultCursor, nullVec, CursorMode.Auto);

        //InputStatus
        SetInputReadingStatus(true);

        //Popup
        ResetPopup();
    }

    #endregion CommandConfig




    #region Trigger

    private void TriggerTypeImmediatly(bool _continueCommanding)
    {
        //Flags
        SetInputReadingStatus(true);

        if (command.commandConfig.cancelAllTasksBefore)
        {
            TaskHelper.ClearTasksAndAddNewTaskWithConfig(commandTargetComponent, command.commandConfig, null, nullVec);
        }
        else
        {
            AddTask(inputComponent.continueIsPressed, command.commandConfig, null, nullVec);
        }
    }


    private void TriggerTypeNeedsTarget()
    {
        //Events
        inputComponent.onStopPressed.AddListener(delegate { OnCancel(); });
        inputComponent.onCancelPressedGameplay.AddListener(delegate { OnCancel(); });

        //Flag
        isWaiting = true;
        inputComponent.listenForGameplayCancel = true;

        //Routines
        StartCoroutine("WaitForNewCommanding");
        StartCoroutine("ListenForCancel");
    }

    private void TriggerTypeActionBeforeExecution()
    {
        command.commandConfig.StartActionBeforeCommand(inputComponent); 
        TriggerTypeNeedsTarget();
    }

    #endregion Trigger




    #region Listener

    /// <summary>
    /// 
    /// </summary>
    private void OnCancel()
    {
        isWaiting = false;

        if (command.commandConfig != null) command.commandConfig.StopActionBeforeCommand();

        StopCoroutine("ListenForCancel");
        UnlockInput();
    }

    /// <summary>
    /// Waits to cancel the active command, if ordered.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ListenForCancel() {
        if (inputComponent.continueIsPressed)
        {
            yield return new WaitUntil(() => inputComponent.commandPressed || !inputComponent.continueIsPressed);
        }
        else
        {
            yield return new WaitUntil(() => inputComponent.commandPressed);
        }

        isWaiting = false;
        UnlockInput();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForNewCommanding()
    {
        //Sets the command cursor
        if (command.commandConfig.cursorTexture != null)
        {
            Cursor.SetCursor(command.commandConfig.cursorTexture, cursorPivot, CursorMode.Auto);
        }

        //Flags
        SetInputReadingStatus(false);

        yield return null;
        yield return new WaitUntil(() => !inputComponent.selectWasPressed);
        yield return new WaitUntil(() => inputComponent.selectWasPressed);

        StopCoroutine("ListenForCancel");

        //EventSystem
        if (eventSystem.IsPointerOverGameObject())
        {
            UnlockInput();
        }
        else
        {
            SetTask();
        }
    }

    #endregion Listener




    #region General

    /// <summary>
    /// Behaviour of a button click.
    /// The button will trigger the CommandConfig of the command.
    /// </summary>
    private void OnClick()
    {
        ResetPopup();

        if (command.commandConfig != null)
        {
            if (!isWaiting)
            {
                //Flags
                SetInputReadingStatus(false);

                command.commandConfig.isTriggeredByButton = true;

                //TriggerTypes
                switch (currentTriggerType)
                {
                    case CommandTriggerType.TriggerImmediatly:
                        TriggerTypeImmediatly(inputComponent.continueIsPressed);
                        break;

                    case CommandTriggerType.NeedsTarget:
                        TriggerTypeNeedsTarget();
                        break;

                    case CommandTriggerType.NeedsTargetAndActionBefore:
                        TriggerTypeActionBeforeExecution();
                        break;
                }
            }
            else
            {
                isWaiting = false;
                UnlockInput();
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_status"></param>
    private void SetInputReadingStatus(bool _status)
    {
        inputComponent.readSelectionInput = _status;
        inputComponent.readCommandInput = _status;
    }


    /// <summary>
    /// Allows the InputComponent to read common input.
    /// </summary>
    private void UnlockInput()
    {
        //Events
        inputComponent.onStopPressed.RemoveListener(delegate { OnCancel(); });
        inputComponent.onCancelPressedGameplay.RemoveListener(delegate { OnCancel(); });

        //Flags
        inputComponent.listenForGameplayCancel = false;
        inputComponent.readSelectionInput = true;
        inputComponent.readCommandInput = true;

        //Reset cursor
        if (command.commandConfig != null && command.commandConfig.cursorTexture != null)
        {
            Cursor.SetCursor(defaultCursor, nullVec, CursorMode.Auto);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_continueCommanding"></param>
    /// <param name="_config"></param>
    /// <param name="_targetEntityHolder"></param>
    /// <param name="_position"></param>
    private void AddTask(bool _continueCommanding, CommandConfig _config, GameObjectEntityHolder _targetEntityHolder, Vector3 _position)
    {
        if (inputComponent.continueIsPressed)
        {
            TaskHelper.AddTaskWithConfig(_config.commandTargetComponent, _config, _targetEntityHolder, _position);
        }
        else
        {
            TaskHelper.ClearTasksAndAddNewTaskWithConfig(_config.commandTargetComponent, _config, _targetEntityHolder, _position);
        }
    }


    /// <summary>
    /// Generates a Task from the successful commading with this button.
    /// </summary>
    private void SetTask()
    {
        GameObjectEntityHolder currentTargetEntityHolder = null;
        Vector3 currentPosition = nullVec;

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        bool commandIsValid = false;

        if (Physics.Raycast(ray, out hit, maxRayCastLength, mouseClickLayerMask))
        {
            if (hit.transform != null)
            {
                if (hit.transform.tag.Equals("Selectable"))
                {
                    currentTargetEntityHolder = hit.transform.GetComponent<EntityHolderLink>().entityHolder;
                    currentPosition = currentTargetEntityHolder.transform.position;
                    commandIsValid = true;
                }

                if (hit.transform.tag.Equals("Ground"))
                {
                    if (currentTriggerType == CommandTriggerType.NeedsTargetAndActionBefore) command.commandConfig.StopActionBeforeCommand();

                    currentPosition = hit.point;
                    commandIsValid = true;
                }
            }
        }

        if (commandIsValid) AddTask(inputComponent.continueIsPressed, command.commandConfig, currentTargetEntityHolder, currentPosition);

        isWaiting = false;

        if (inputComponent.continueIsPressed)
        {
            StartCoroutine("WaitForNewCommanding");
            StartCoroutine("ListenForCancel");
        }
        else UnlockInput();
    }

    #endregion General

}
