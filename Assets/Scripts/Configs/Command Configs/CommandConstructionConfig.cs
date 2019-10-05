using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Command_Construction_Config", menuName = "ScriptableObjects/Command Configs/Command Construction Config")]
public class CommandConstructionConfig : CommandConfig
{
    [Header("Build Config")]
    public ConstructionConfig config;


    //Construction values
    [NonSerialized] private float maxConstructionDistance;

    //Vectors
    [NonSerialized] private Vector3 distanceOffset;

    //Components 
    [NonSerialized] private UserComponent userComponent;
    [NonSerialized] private CameraComponent cameraComponent;
    [NonSerialized] private ResourceComponent resourceComponent;
    [NonSerialized] private FactionComponent factionComponent;
    [NonSerialized] private Transform movementTransform;

    //Temporary
    [NonSerialized] private Camera mainCamera;
    [NonSerialized] private LayerMask groundLayer;
    [NonSerialized] private float maxRayCastLength;
    [NonSerialized] private GameObject temporaryPrefab;
    [NonSerialized] private Transform prefabTransform;

    //Job
    [NonSerialized] public DefaultJob waitForObjectSetupJob;


    //Audio
    //--> Serializable, cause this sound settings are used in every of this configs
    private float volume;
    private AudioClip developmentFailedClip;


    public override void CommandSetup(int _userIndex, Entity _entity, CommandTargetComponent _commandTargetComponent)
    {
        base.CommandSetup(_userIndex, _entity, _commandTargetComponent);

        //Components
        factionComponent = ComponentManager.GetComponent<FactionComponent>(_entity.GetEntityID());
        int userEntityId = GameController.Instance.GetUserEntitId(factionComponent.userIndex);
        userComponent = ComponentManager.GetComponent<UserComponent>(userEntityId);
        cameraComponent = ComponentManager.GetComponent<CameraComponent>(userEntityId);
        resourceComponent = ComponentManager.GetComponent<ResourceComponent>(userEntityId);
        movementTransform = _entity.entityGameObject.transform;

        //Raycast
        groundLayer = GameController.Instance.GameParameter.groundLayerMask;
        maxRayCastLength = GameController.Instance.GameParameter.maxMouseRayCastLength;

        //Paramater
        GameParameter gameParameter = GameController.Instance.GameParameter;
        volume = gameParameter.developmentFailedVolume;
        developmentFailedClip = gameParameter.developmentFailedClip;
        maxConstructionDistance = gameParameter.maxConstructionDistance;
    }


    protected override void StartActionBeforeCommand()
    {
        if (mainCamera == null) mainCamera = cameraComponent.cam;

        temporaryPrefab = Instantiate(config.temporaryPrefab, Vector3.zero, Quaternion.identity);
        prefabTransform = temporaryPrefab.transform;
    }


    protected override void WhileActionBeforeCommand()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); //Raycast

        if (Physics.Raycast(ray, out hit, maxRayCastLength, groundLayer))
        {
            if (hit.transform != null)
            {
                prefabTransform.position = hit.point;
            }
        }
    }


    protected override void EndActionBeforeCommand()
    {
        if (temporaryPrefab != null)
        {
            Destroy(temporaryPrefab);
            prefabTransform = null;
        }
    }


    public override void StartCommand(GameObjectEntityHolder _targetEntityHolder, Vector3 _targetPos)
    {
        base.StartCommand(_targetEntityHolder, _targetPos);

        distanceOffset = movementTransform.position - targetPosition;

        if (distanceOffset.sqrMagnitude <= maxConstructionDistance * maxConstructionDistance)
        {
            bool enoughResources = CheckResources();

            if (enoughResources)
            {
                SpawnBuilding();
            }
            else
            {
                TryPlayFailSound();
            }
        }
        else
        {
            CommandMoveConfig moveConfig = (CommandMoveConfig)CommandHelper.GetCommandConfigByType(commandTargetComponent, typeof(CommandMoveConfig));

            if (moveConfig != null)
            {
                moveConfig.distanceToTarget = maxConstructionDistance;

                TaskHelper.AddTaskWithConfigAtIndex(commandTargetComponent, moveConfig, null, targetPosition, 0); //Creates a move task
                TaskHelper.AddTaskWithConfigAtIndex(commandTargetComponent, this, null, targetPosition, 1); //Creates an attack task next in row
            }
        }

        isActive = false;
    }


    public override void UpdateCommand() { }


    public override void AfterCommand()
    {
        base.AfterCommand();
    }


    private bool CheckResources()
    {
        bool enoughResources = false;

        if (resourceComponent.currentEnergy >= config.requiredEnergy && resourceComponent.currentMass >= config.requiredMass)
        {

            resourceComponent.currentEnergy -= config.requiredEnergy;
            resourceComponent.currentMass -= config.requiredMass;

            enoughResources = true;
        }
        return enoughResources;
    }


    private void SpawnBuilding()
    {
        GameObject newObject = GameObject.Instantiate(config.prefab, targetPosition, Quaternion.identity);
        int entityId = newObject.GetComponent<GameObjectEntityHolder>().GetEntityID();

        waitForObjectSetupJob = new DefaultJob(delegate
        {
            if (ComponentManager.ComponentReady<InformationComponent>(entityId))
            {
                userComponent.factionEntities.Add(entityId);

                //CoWorker infopackage
                InformationComponent info = ComponentManager.GetComponent<InformationComponent>(entityId);
                info.informationPackage.factionComponent.userIndex = factionComponent.userIndex;
                info.informationPackage.identifierComponent.objectName = config.prefabName;
                info.informationPackage.identifierComponent.objectSprite = config.picture;
                info.informationPackage.typeComponent.isMilitaryObject = config.isMilitaryObject;

                //Job
                waitForObjectSetupJob.CancelJob();
                waitForObjectSetupJob = null;
            }
        });

        //EntityController entity = building.GetComponent<EntityController>();
        //ConstructionComponent.AddConstructionComponent(entity); TODOO
    }


    private void TryPlayFailSound()
    {
        if (developmentFailedClip != null)
        {
            AudioSource.PlayClipAtPoint(developmentFailedClip, movementTransform.position, volume);
        }
    }
}
