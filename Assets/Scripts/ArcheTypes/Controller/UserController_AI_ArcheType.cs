using UnityEngine;


[CreateAssetMenu(fileName = "UserController_AI_ArcheType", menuName = "ScriptableObjects/ArcheType Configs/UserController_AI_ArcheType")]
public class UserController_AI_ArcheType : ArcheType
{
    //Components
    private UserComponent userComponent;
    private ResourceComponent resourceComponent;


    public override void Init()
    {
        if (init) return;

        //User
        userComponent = ScriptableObject.CreateInstance<UserComponent>();

        //Resource
        resourceComponent = ScriptableObject.CreateInstance<ResourceComponent>();


        //Add components
        AddComponentsToList(
            userComponent,
            resourceComponent
            );


        base.Init();
    }
}
