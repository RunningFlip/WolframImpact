using UnityEngine;
using System;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Command_Shoot_Config", menuName = "ScriptableObjects/Command Configs/Command Shoot Config")]
public class CommandShootConfig : CommandConfig
{
    [Header("Requirements")]
    public float requiredDistance;
    public float requiredEnergy;

    [Header("Bullet Values")]
    public float damage;
    public DamageType damageType;
    public float speed;
    public float maxBulletRange = -1;

    [Header("Bullet Pattern")]
    public float firerate;

    [Header("Audio")]
    public float volume;
    public AudioClip attackAudioClip;


    //Spawn
    [NonSerialized] public Transform[] spawns;

    //Vectors
    [NonSerialized] private Vector3 distanceOffset;

    //Flags
    [NonSerialized] private bool inShoot;

    //Shooting storings
    [NonSerialized] private float lastShootTime = -float.MaxValue;

    //Pool lists
    [NonSerialized] public List<GameObject> bulletObjects = new List<GameObject>();

    //Components
    [NonSerialized] private PoolHolderComponent poolHolderComponent;
    [NonSerialized] private StateComponent stateComponent;
    [NonSerialized] private TurretComponent turretComponent;
    [NonSerialized] private SelectionTargetComponent selectionTargetComponent;
    [NonSerialized] private AudioComponent audioComponent;
    [NonSerialized] private TypeComponent typeComponent;
    [NonSerialized] private Transform movementTransform;

    //Target components
    [NonSerialized] private HealthComponent targetHealthComponent;
    [NonSerialized] private Transform targetmovementTransform; 

    //Action config "movement"
    [NonSerialized] private CommandMoveConfig moveConfig = null;


    public override void CommandSetup(int _userIndex, Entity _entity, CommandTargetComponent _commandTargetComponent)
    {
        base.CommandSetup(_userIndex, _entity, _commandTargetComponent);

        //Entity id
        int entityId = _entity.GetEntityID();

        //Components
        movementTransform = _entity.entityGameObject.transform;
        stateComponent = ComponentManager.GetComponent<StateComponent>(entityId);
        poolHolderComponent = ComponentManager.GetComponent<PoolHolderComponent>(entityId);
        turretComponent = ComponentManager.GetComponent<TurretComponent>(entityId);
        selectionTargetComponent = ComponentManager.GetComponent<SelectionTargetComponent>(entityId);
        audioComponent = ComponentManager.GetComponent<AudioComponent>(entityId);
        typeComponent = ComponentManager.GetComponent<TypeComponent>(entityId);
        
        //Information
        InformationComponent informationComponent = ComponentManager.GetComponent<InformationComponent>(entityId);
        informationComponent.informationPackage.damage = damage;
        informationComponent.informationPackage.damageType = damageType;

        //Spawns
        spawns = _entity.entityGameObject.GetComponentInChildren<BulletSpawnLink>().bulletSpawns;

        //Movement config
        CommandConfig actionConfig = CommandHelper.GetCommandConfig(commandTargetComponent, CommandType.Command_On_Ground); //Gets movement config;
        if (actionConfig is CommandMoveConfig)
        {
            moveConfig = (CommandMoveConfig)actionConfig;
        }     

        //Pool holder
        for (int i = 0; i < poolHolderComponent.poolingObjects.Length; i++)
        {
            bulletObjects.Add(poolHolderComponent.poolingObjects[i]);
            bulletObjects[i].transform.parent = null;

            int bulletEntityId = bulletObjects[i].GetComponent<GameObjectEntityHolder>().GetEntityID();
            SetBulletValues(ComponentManager.GetComponent<BulletComponent>(bulletEntityId));
        }
    }


    public override void StartCommand(GameObjectEntityHolder _targetEntityHolder, Vector3 _targetPos)
    {
        base.StartCommand(_targetEntityHolder, _targetPos);

        if (_targetEntityHolder != null)
        {
            int targetEntityId = _targetEntityHolder.GetEntityID();

            targetmovementTransform = _targetEntityHolder.transform;
            TypeComponent targetTypeComponent = ComponentManager.GetComponent<TypeComponent>(targetEntityId);

            if (!Array.Exists(typeComponent.targetObjectTypes, type => type == targetTypeComponent.objectType))
            {
                isActive = false;
                return;
            }

            targetHealthComponent = ComponentManager.GetComponent<HealthComponent>(targetEntityId);

            if (targetHealthComponent.isDead)
            {
                isActive = false;
                return;
            }
            targetHealthComponent.onDeath.AddListener(StopShooting);

            stateComponent.waitingStates.Add(new StateElement(StateType.Detectable, false));
        }
        else
        {
            isActive = false;
            return;
        }       
    }


    public override void UpdateCommand()
    {
        //Rotation
        if (turretComponent != null)
        {
            turretComponent.targetVector = targetmovementTransform.position;
            turretComponent.updateTurrets = true;
        }


        //In range check
        distanceOffset = movementTransform.position - targetmovementTransform.position;

        if (distanceOffset.sqrMagnitude > requiredDistance * requiredDistance)
        {
            if (moveConfig != null)
            {
                moveConfig.distanceToTarget = requiredDistance;

                TaskHelper.AddTaskWithConfigAtIndex(commandTargetComponent, moveConfig, targetEntityHolder, targetPosition, 0); //Creates a move task
                TaskHelper.AddTaskWithConfigAtIndex(commandTargetComponent, this, targetEntityHolder, targetPosition, 1); //Creates an attack task next in row
            }

            isActive = false; //Cancels current task
            return;
        }


        //Checks move state
        if (stateComponent.attackingState < 0) return;

        //Shoot
        if (!inShoot && lastShootTime + firerate < Time.time)
        {
            inShoot = true;
            Shoot();
        }
    }


    public override void AfterCommand()
    {
        base.AfterCommand();

        if (turretComponent != null)
        {
            turretComponent.resetTurrets = true;
        }

        //Enable detecting state
        stateComponent.waitingStates.Add(new StateElement(StateType.Detectable, true));

        //Reset target components
        targetmovementTransform = null;
        targetHealthComponent = null;
    }


    private void Shoot()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            Transform spawn = spawns[i];
            Vector3 direction = TransformHelper.GetDirection(targetmovementTransform.position, spawn.position);

            if (bulletObjects.Count > 0)
            {
                GameObject bullet = bulletObjects[0];
                int bulletEntityId = bullet.GetComponent<GameObjectEntityHolder>().GetEntityID();

                bullet.transform.position = spawn.position;

                BulletComponent bulletComponent = ComponentManager.GetComponent<BulletComponent>(bulletEntityId);
                bulletComponent.startPosition = spawn.position;
                bulletComponent.direction = direction;

                bulletObjects.Remove(bullet);

                //Remove deactive tag component
                ComponentManager.DestroyComponent<DeactiveTagComponent>(bulletEntityId);

                //Activate gameobject
                bullet.SetActive(true);
            }
        }

        PlayAudio(spawns[0].position);

        lastShootTime = Time.time;
        inShoot = false;
    }


    private void StopShooting()
    {
        inShoot = false;
        isActive = false;

        if (targetHealthComponent != null && targetHealthComponent.isDead)
        {
            targetHealthComponent.onDeath.RemoveListener(StopShooting);
        } 
    }


    private void PlayAudio(Vector3 _position)
    {
        if (attackAudioClip != null)
        {
            audioComponent.audioSource.PlayOneShot(attackAudioClip, volume);
        }
    }


    private void SetBulletValues(BulletComponent _bulletComponent)
    {
        _bulletComponent.shootConfig = this;

        _bulletComponent.damage = damage;
        _bulletComponent.damageType = damageType;

        _bulletComponent.speed = speed;
        _bulletComponent.maxRange = maxBulletRange;

        _bulletComponent.userIndex = userIndex;
    }

}
