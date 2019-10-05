using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(DetectionComponent), typeof(FactionComponent), typeof(CommandTargetComponent), typeof(HealthComponent), typeof(StateComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            Entity entity = entities[i];
            int entityID = entity.GetEntityID();

            DetectionComponent detection = ComponentManager.GetComponent<DetectionComponent>(entityID);
            FactionComponent faction = ComponentManager.GetComponent<FactionComponent>(entityID);
            CommandTargetComponent comTarget = ComponentManager.GetComponent<CommandTargetComponent>(entityID);
            HealthComponent health = ComponentManager.GetComponent<HealthComponent>(entityID);
            StateComponent state = ComponentManager.GetComponent<StateComponent>(entityID);

            if (!detection.init)
            {
                if (comTarget.init)
                {
                    detection.init = true;
                    detection.masterDetection = true;
                    DetectionComponentSetup(detection, health, entity.entityGameObject.transform);
                }
            }
            else
            {
                if (state.detectionState >= 0 && !detection.detects)
                {
                    detection.detects = true;
                    continue;
                }
                if (state.detectionState < 0)
                {
                    detection.detects = false;
                    continue;
                }
                if (detection.masterDetection && detection.detects && detection.lastUpdate + detection.updateTime < Time.time)
                {
                    CheckSurroundings(detection, comTarget, state, faction.userIndex);
                }
            }
        }
    }


    private void CheckSurroundings(DetectionComponent _detection, CommandTargetComponent _comTarget, StateComponent _state, int _userIndex)
    {
        //Hits
        int hitAmount = Physics.SphereCastNonAlloc(_detection.movementTransform.position, 
            _detection.detectionRange, 
            _detection.forwardVec, 
            _detection.hits, 
            0f, 
            _detection.detectionMask
            );

        //Target
        GameObjectEntityHolder nearestTargetEntityHolder = null;
        float distance = float.MaxValue;

        //Components
        GameObjectEntityHolder currentEntityHolder = null;

        bool hitsContainsEnemy = false;

        //Check
        if (hitAmount > 0)
        {
            for (int i = 0; i < hitAmount; i++)
            {
                currentEntityHolder = null;
                currentEntityHolder = _detection.hits[i].transform.GetComponent<EntityHolderLink>().entityHolder;

                bool enemyFound = ComponentManager.GetComponent<FactionComponent>(currentEntityHolder.GetEntityID()).userIndex != _userIndex;

                if (enemyFound)
                {
                    _detection.detectionOffset = _detection.movementTransform.position - currentEntityHolder.transform.position;

                    float currentDistance = _detection.detectionOffset.magnitude;

                    if (currentDistance < distance)
                    {
                        distance = currentDistance;
                        nearestTargetEntityHolder = currentEntityHolder;
                        hitsContainsEnemy = true;
                    }
                }
            }

            //Command
            if (hitsContainsEnemy && nearestTargetEntityHolder != null)
            {
                CommandAttack(_detection, _comTarget, _state, nearestTargetEntityHolder, currentEntityHolder.GetEntityID());
            }
        }
        _detection.lastUpdate = Time.time;
    }


    private void CommandAttack(DetectionComponent _detection, CommandTargetComponent _comTarget, StateComponent _state, GameObjectEntityHolder _targetEntityHolder, int _targetEntityId)
    {
        TypeComponent typeComponent = ComponentManager.GetComponent<TypeComponent>(_targetEntityId);
        CommandType commandType = CommandType.None;

        switch (typeComponent.tagType)
        {
            case TagType.Unit:
                commandType = CommandType.Command_Enemy_Unit;
                break;
            case TagType.Building:
                commandType = CommandType.Command_Enemy_Building;
                break;
        }

        if (_comTarget.currentTask.command != null)
        {
            Task task = TaskHelper.CreatTask(_comTarget, commandType, _targetEntityHolder, _targetEntityHolder.transform.position);
            TaskHelper.NewCurrentTaskWithPushBack(_comTarget, task);
        }
        else
        {
            TaskHelper.AddTask(_comTarget, commandType, _targetEntityHolder, _targetEntityHolder.transform.position);
        }
    }


    /// <summary>
    /// Setups all neccessary properties of the detection component.
    /// </summary>
    /// <param name="_detection"></param>
    private void DetectionComponentSetup(DetectionComponent _detection, HealthComponent _health, Transform _transform)
    {
        //Hits
        _detection.hits = new RaycastHit[_detection.maxHits];

        //Detection
        _detection.detectionMask = GameController.Instance.GameParameter.detectionMask;

        //Time
        _detection.updateTime = GameController.Instance.GameParameter.detectionUpdateTime;

        //Components
        _detection.movementTransform = _transform;

        //OnDeath
        _health.onDeath.AddListener(delegate {
            _detection.wasDetectingBeforeDeath = _detection.masterDetection;
            _detection.masterDetection = false; });

        _health.onRevive.AddListener(delegate {
            _detection.masterDetection = _detection.wasDetectingBeforeDeath; });
    }
}
