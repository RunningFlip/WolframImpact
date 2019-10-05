using UnityEngine;


[CreateAssetMenu(fileName = "Turret_AntiTank_ArcheType", menuName = "ScriptableObjects/ArcheType Configs/Turret_AntiTank_ArcheType")]
public class Turret_AntiTank_ArcheType : SelectableArcheType
{
    [Header("Detection")]
    public float detectionRange = 10;
    [Range(0, 100)]
    public int maxHits = 10;


    //Components
    private TurretComponent turretComponent;
    private PoolHolderComponent poolHolderComponent;
    private DetectionComponent detectionComponent;
    private MarkerComponent markerComponent;
    

    public override void Init()
    {
        if (init) return;
        base.Init();

        //Components      
        turretComponent = ScriptableObject.CreateInstance<TurretComponent>();
        poolHolderComponent = ScriptableObject.CreateInstance<PoolHolderComponent>();
        detectionComponent = ScriptableObject.CreateInstance<DetectionComponent>();
        markerComponent = ScriptableObject.CreateInstance<MarkerComponent>();

        //Detection
        detectionComponent.detectionRange = detectionRange;
        detectionComponent.maxHits = maxHits;


        //Add components
        AddComponentsToList(
            turretComponent,
            poolHolderComponent,
            detectionComponent,
            markerComponent
            );
    }
}
