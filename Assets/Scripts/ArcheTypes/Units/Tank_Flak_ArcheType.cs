using UnityEngine;


[CreateAssetMenu(fileName = "Tank_Flak_ArcheType", menuName = "ScriptableObjects/ArcheType Configs/Tank_Flak_ArcheType")]
public class Tank_Flak_ArcheType : SelectableArcheType
{
    [Header("Detection")]
    public float detectionRange = 10;
    [Range(0, 100)]
    public int maxHits = 10;


    //Components
    private TurretComponent turretComponent;
    private PoolHolderComponent poolHolderComponent;
    private DetectionComponent detectionComponent;


    public override void Init()
    {
        if (init) return;
        base.Init();

        //Components
        turretComponent = ScriptableObject.CreateInstance<TurretComponent>();
        poolHolderComponent = ScriptableObject.CreateInstance<PoolHolderComponent>();
        detectionComponent = ScriptableObject.CreateInstance<DetectionComponent>();

        //Detection
        detectionComponent.detectionRange = detectionRange;
        detectionComponent.maxHits = maxHits;


        //Add components
        AddComponentsToList(
            turretComponent,
            poolHolderComponent,
            detectionComponent
            );
    }
}
