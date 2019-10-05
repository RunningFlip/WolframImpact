using System.Collections.Generic;
using UnityEngine;


public class SelectableArcheType : ArcheType
{
    [Header("General")]
    public string objectName;
    public Sprite objectSprite;

    [Header("Type")]
    public bool isMilitaryObject;
    public TagType tagType;
    public ObjectType objectType;
    public ObjectType[] targetObjectTypes;

    [Header("Health")]
    public float maxHealth;
    public DeathType deathType;
    public float deathDelayTime;
    public Resistance[] resistances;

    [Header("Energy")]
    public float maxEnergy;

    [Header("Commands")]
    public List<Command> commands;

    [Header("Damage Receiver")]
    public bool canReceiveDamage;


    //default components
    private FactionComponent factionComponent;
    private IdentifierComponent identifierComponent;
    private StateComponent stateComponent;
    private TypeComponent typeComponent;
    private EnergyComponent energyComponent;
    private HealthComponent healthComponent;
    private SelectionTargetComponent selectionTargetComponent;
    private CommandTargetComponent commandTargetComponent;
    private InformationComponent informationCompnent;
    private CollisionComponent collisionComponent;
    private DamageReceiverComponent damageReceiverComponent;
    private AudioComponent audioComponent;


    public override void Init()
    {
        if (init) return;

        //default
        factionComponent = ScriptableObject.CreateInstance<FactionComponent>();
        identifierComponent = ScriptableObject.CreateInstance<IdentifierComponent>();
        stateComponent = ScriptableObject.CreateInstance<StateComponent>();
        typeComponent = ScriptableObject.CreateInstance<TypeComponent>();
        healthComponent = ScriptableObject.CreateInstance<HealthComponent>();
        energyComponent = ScriptableObject.CreateInstance<EnergyComponent>();
        selectionTargetComponent = ScriptableObject.CreateInstance<SelectionTargetComponent>();
        commandTargetComponent = ScriptableObject.CreateInstance<CommandTargetComponent>();
        informationCompnent = ScriptableObject.CreateInstance<InformationComponent>();
        collisionComponent = ScriptableObject.CreateInstance<CollisionComponent>();
        damageReceiverComponent = ScriptableObject.CreateInstance<DamageReceiverComponent>();
        audioComponent = ScriptableObject.CreateInstance<AudioComponent>();

        //Setups
        identifierComponent.objectName = objectName;
        identifierComponent.objectSprite = objectSprite;

        //Type
        typeComponent.isMilitaryObject = isMilitaryObject;
        typeComponent.tagType = tagType;
        typeComponent.objectType = objectType;
        typeComponent.targetObjectTypes = targetObjectTypes;

        //Energy
        energyComponent.maxEnergy = maxEnergy;

        //Health
        healthComponent.maxHealth = maxHealth;
        healthComponent.deathType = deathType;
        healthComponent.deathDelayTime = deathDelayTime;
        healthComponent.resistances = resistances;

        //Command Target
        commandTargetComponent.commands = commands;

        //Damage Receiver
        damageReceiverComponent.receivable = canReceiveDamage;


        //Add components
        AddComponentsToList(
            factionComponent,
            identifierComponent,
            stateComponent,
            typeComponent,
            energyComponent,
            healthComponent,
            informationCompnent,
            collisionComponent,
            selectionTargetComponent,
            commandTargetComponent,
            damageReceiverComponent,
            audioComponent       
            );

        base.Init();
    }
}