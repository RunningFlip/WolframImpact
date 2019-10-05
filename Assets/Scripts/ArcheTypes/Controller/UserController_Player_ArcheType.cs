using UnityEngine;


[CreateAssetMenu(fileName = "UserController_Player_ArcheType", menuName = "ScriptableObjects/ArcheType Configs/UserController_Player_ArcheType")]
public class UserController_Player_ArcheType : ArcheType
{
    //Components
    private UserComponent userComponent;
    private InputComponent inputComponent;
    private CameraComponent cameraComponent;
    private UITransmitterComponent uiTransmitterComponent;
    private SelectionComponent selectionComponent;
    private CommandComponent commandComponent;
    private ResourceComponent resourceComponent;

    //TagComponent
    private MainCameraTagComponent mainCameraTagComponent;


    public override void Init()
    {
        if (init) return;

        //User
        userComponent = ScriptableObject.CreateInstance<UserComponent>();

        //Input
        inputComponent = ScriptableObject.CreateInstance<InputComponent>();

        //Camera
        cameraComponent = ScriptableObject.CreateInstance<CameraComponent>();
        mainCameraTagComponent = ScriptableObject.CreateInstance<MainCameraTagComponent>();

        //UITransmitter
        uiTransmitterComponent = ScriptableObject.CreateInstance<UITransmitterComponent>();

        //Selection
        selectionComponent = ScriptableObject.CreateInstance<SelectionComponent>();

        //Command
        commandComponent = ScriptableObject.CreateInstance<CommandComponent>();

        //Resource
        resourceComponent = ScriptableObject.CreateInstance<ResourceComponent>();


        //Add components
        AddComponentsToList(
            userComponent,
            inputComponent, 
            cameraComponent, 
            mainCameraTagComponent,
            uiTransmitterComponent,
            selectionComponent,
            commandComponent, 
            resourceComponent
            );


        base.Init();
    }
}
