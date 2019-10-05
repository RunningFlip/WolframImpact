using UnityEngine;


[CreateAssetMenu(fileName = "Factory_Tank_ArcheType", menuName = "ScriptableObjects/ArcheType Configs/Factory_Tank_ArcheType")]
public class Factory_Tank_ArcheType : SelectableArcheType
{

    //Components
    private DevelopmentComponent developmentComponent;
    private MarkerComponent markerComponent;


    public override void Init()
    {
        if (init) return;
        base.Init();

        //Components      
        developmentComponent = ScriptableObject.CreateInstance<DevelopmentComponent>();
        markerComponent = ScriptableObject.CreateInstance<MarkerComponent>();


        //Add components
        AddComponentsToList(
            developmentComponent,
            markerComponent
            );
    }
}
