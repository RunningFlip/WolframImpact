using UnityEngine;


[CreateAssetMenu(fileName = "Turret_AntiTank_ArcheType", menuName = "ScriptableObjects/ArcheType Configs/Turret_AntiTank_ArcheType")]
public class Turret_AntiTank_ArcheType : ArcheType
{
    //Components
    private SetupComponent setupComponent;
    private StateComponent stateComponent;
    private TypeComponent typeComponent;
    private EnergyComponent energyComponent;
    private HealthComponent healthComponent;
    private DamageReceiverComponent damageReceiverComponent;
    private CollisionComponent collisionComponent;
    private AudioComponent audioComponent;


    public override void Init()
    {
        if (init) return;

        //Setup
        setupComponent = ScriptableObject.CreateInstance<SetupComponent>();

        //State
        stateComponent = ScriptableObject.CreateInstance<StateComponent>();

        //Type
        typeComponent = ScriptableObject.CreateInstance<TypeComponent>();
        typeComponent.Setup(
            TagType.Unit,
            ObjectType.Ground,
            null
            );

        //Energy
        energyComponent = ScriptableObject.CreateInstance<EnergyComponent>();
        energyComponent.Setup(
            100f
            );

        //Health
        healthComponent = ScriptableObject.CreateInstance<HealthComponent>();
        healthComponent.Setup(
            100f,
            DeathType.Destroy,
            0f,
            new Resistance[] { new Resistance(DamageType.Projectile, 5) }
            );

        //Damage receiver
        damageReceiverComponent = ScriptableObject.CreateInstance<DamageReceiverComponent>();
        damageReceiverComponent.Setup(
            true
            );

        //Collision
        collisionComponent = ScriptableObject.CreateInstance<CollisionComponent>();

        //Audio
        audioComponent = ScriptableObject.CreateInstance<AudioComponent>();


        //Add components
        AddComponentsToList(
            setupComponent,
            stateComponent,
            typeComponent,
            energyComponent,
            healthComponent,
            damageReceiverComponent,
            collisionComponent,
            audioComponent
            );


        base.Init();
    }
}
