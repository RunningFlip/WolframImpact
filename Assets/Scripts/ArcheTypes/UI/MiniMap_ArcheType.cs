using UnityEngine;


[CreateAssetMenu(fileName = "MiniMap_ArcheType", menuName = "ScriptableObjects/ArcheType Configs/MiniMap_ArcheType")]
public class MiniMap_ArcheType : ArcheType
{
    //Components
    private MiniMapComponent miniMapComponent;


    public override void Init()
    {
        if (init) return;

        //Map
        miniMapComponent = ScriptableObject.CreateInstance<MiniMapComponent>();

        //Add components
        AddComponentsToList(
            miniMapComponent

            );

        base.Init();
    }
}
