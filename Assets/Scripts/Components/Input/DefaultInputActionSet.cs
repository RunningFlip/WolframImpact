using InControl;


public class DefaultInputActionSet : PlayerActionSet
{
    //Gameplay
    public PlayerAction selection;
    public PlayerAction command;

    //Camera movement
    public PlayerAction moveUp;
    public PlayerAction moveDown;
    public PlayerAction moveLeft;
    public PlayerAction moveRight;
    public PlayerTwoAxisAction movement;
    public PlayerAction deactivateMouseMovement;

    //Camera rotation
    public PlayerAction rotateLeft;
    public PlayerAction rotateRight;
    public PlayerOneAxisAction rotation;

    //Camera zoom
    public PlayerAction zoomIn;
    public PlayerAction zoomOut;
    public PlayerOneAxisAction zoom;
    public PlayerAction cameraReset;

    //Shortcuts
    public PlayerAction stopCurrentActions;
    public PlayerAction focusCamera;

    //Numbers
    public PlayerAction num1;
    public PlayerAction num2;
    public PlayerAction num3;
    public PlayerAction num4;
    public PlayerAction num5;
    public PlayerAction num6;
    public PlayerAction num7;
    public PlayerAction num8;
    public PlayerAction num9;
    public PlayerAction num0;
    public PlayerAction[] numbers;

    //General
    public PlayerAction continueInput;
    public PlayerAction cancel;
    public PlayerAction destroy;
    public PlayerAction context;



    /// <summary>
    /// Default Input Actionset setup.
    /// </summary>
    public DefaultInputActionSet()
    {
        //Gameplay
        selection = CreatePlayerAction("Selection");
        command = CreatePlayerAction("Command");

        //Camera movement
        moveUp = CreatePlayerAction("Move Up");
        moveDown = CreatePlayerAction("Move Down");
        moveLeft = CreatePlayerAction("Move Left");
        moveRight = CreatePlayerAction("Move Right");
        movement = CreateTwoAxisPlayerAction(moveLeft, moveRight, moveDown, moveUp);
        deactivateMouseMovement = CreatePlayerAction("Deactivate Mouse Movement");

        //Camera rotation
        rotateLeft = CreatePlayerAction("Rotate Left");
        rotateRight = CreatePlayerAction("Rotate Right");
        rotation = CreateOneAxisPlayerAction(rotateLeft, rotateRight);

        //Camera zoom
        zoomIn = CreatePlayerAction("Zoom In");
        zoomOut = CreatePlayerAction("Zoom Out");
        zoom = CreateOneAxisPlayerAction(zoomOut, zoomIn);
        cameraReset = CreatePlayerAction("Camera Reset");

        //Shortcuts
        stopCurrentActions = CreatePlayerAction("Stop Current Actions");
        focusCamera = CreatePlayerAction("Shortcut Focus Camera");

        //Numbers
        num1 = CreatePlayerAction("Number 1");
        num2 = CreatePlayerAction("Number 2");
        num3 = CreatePlayerAction("Number 3");
        num4 = CreatePlayerAction("Number 4");
        num5 = CreatePlayerAction("Number 5");
        num6 = CreatePlayerAction("Number 6");
        num7 = CreatePlayerAction("Number 7");
        num8 = CreatePlayerAction("Number 8");
        num9 = CreatePlayerAction("Number 9");
        num0 = CreatePlayerAction("Number 0");

        //General
        continueInput = CreatePlayerAction("Continue Input");
        cancel = CreatePlayerAction("Cancel");
        destroy = CreatePlayerAction("Destroy");
        context = CreatePlayerAction("Context");

        //Add bindings
        AddDefaultKeyboardAndMouseBindings();
    }


    /// <summary>
    /// Binds the specific input possibilities to the playeractions.
    /// </summary>
    private void AddDefaultKeyboardAndMouseBindings()
    {
        //Gameplay
        selection.AddDefaultBinding(Mouse.LeftButton);
        command.AddDefaultBinding(Mouse.RightButton);

        //Camera movement
        moveUp.AddDefaultBinding(Key.UpArrow);
        moveDown.AddDefaultBinding(Key.DownArrow);
        moveLeft.AddDefaultBinding(Key.LeftArrow);
        moveRight.AddDefaultBinding(Key.RightArrow);
        deactivateMouseMovement.AddDefaultBinding(Key.Tab);

        //Camera rotation
        rotateLeft.AddDefaultBinding(Key.Q);
        rotateRight.AddDefaultBinding(Key.E);

        //Camera zoom
        zoomIn.AddDefaultBinding(Mouse.PositiveScrollWheel);
        zoomOut.AddDefaultBinding(Mouse.NegativeScrollWheel);
        cameraReset.AddDefaultBinding(Mouse.MiddleButton);

        //Shortcuts
        stopCurrentActions.AddDefaultBinding(Key.S);
        focusCamera.AddDefaultBinding(Key.Space);

        //Numbers
        num1.AddDefaultBinding(Key.Key1);
        num2.AddDefaultBinding(Key.Key2);
        num3.AddDefaultBinding(Key.Key3);
        num4.AddDefaultBinding(Key.Key4);
        num5.AddDefaultBinding(Key.Key5);
        num6.AddDefaultBinding(Key.Key6);
        num7.AddDefaultBinding(Key.Key7);
        num8.AddDefaultBinding(Key.Key8);
        num9.AddDefaultBinding(Key.Key9);
        num0.AddDefaultBinding(Key.Key0);
        numbers = new PlayerAction[] { num0, num1, num2, num3, num4, num5, num6, num7, num8, num9 };

        //General
        continueInput.AddDefaultBinding(Key.LeftShift);
        cancel.AddDefaultBinding(Key.Escape);
        destroy.AddDefaultBinding(Key.Delete);
        context.AddDefaultBinding(Key.F12);
    }

}
