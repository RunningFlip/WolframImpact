using UnityEngine;
using System;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "Command_Move_Config", menuName = "ScriptableObjects/Command Configs/Command Move Config")]
public class CommandMoveConfig : CommandConfig
{
    [Header("Agent Steering Values")]
    public int agentType = 0;
    [Space]
    public float moveSpeed;
    public float angularSpeed;
    public float acceleration;
    public float stoppingDistance; //will not be setted to navmesh -> will be used in this script, to get a callback, if the agent reached the target.

    [Header("Obstacle Avoidance")]
    public float agentRadius = 1.5f;
    public float agentHeight = 2f;
    public ObstacleAvoidanceType obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;
    [Range(0, 99)]
    public int avoidancePriority = 50;


    [Header("Disables detection")]
    public bool disablesDetection = true;

    //Vectors
    [NonSerialized] private Vector3 distanceOffset;
    [NonSerialized] private Vector3 nullVec = Vector3.zero;

    //Components
    [NonSerialized] private Transform movementTransform;
    [NonSerialized] private StateComponent stateComponent;
    [NonSerialized] private AudioComponent audioComponent;
    [NonSerialized] private NavMeshAgent agent;

    //Other Components
    [NonSerialized] private Transform otherMovementTransform;

    //Destination
    [NonSerialized] public float distanceToTarget = -1;


    public override void CommandSetup(int _userIndex, Entity _entity, CommandTargetComponent _commandTargetComponent)
    {
        base.CommandSetup(_userIndex, _entity, _commandTargetComponent);

        int entityId = _entity.GetEntityID();

        //Components
        movementTransform = _entity.entityGameObject.transform;
        stateComponent = ComponentManager.GetComponent<StateComponent>(entityId);
        audioComponent = ComponentManager.GetComponent<AudioComponent>(entityId);

        //Nav mesh agent
        agent = _entity.entityGameObject.GetComponentInChildren<NavMeshAgent>();

        agent.agentTypeID = agentType; 
        agent.speed = moveSpeed;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;

        agent.radius = agentRadius;
        agent.height = agentHeight;
        agent.obstacleAvoidanceType = obstacleAvoidanceType;
        agent.avoidancePriority = avoidancePriority;
        agent.stoppingDistance = stoppingDistance;
    }


    public override void StartCommand(GameObjectEntityHolder _targetEntityHolder, Vector3 _targetPos)
    {
        base.StartCommand(_targetEntityHolder, _targetPos);

        //Set new position if target is moving object
        if (_targetEntityHolder != null)
        {
            otherMovementTransform = _targetEntityHolder.transform;
            targetPosition = otherMovementTransform.position;
        }

        //Disables the detection if needed.
        if (disablesDetection)
        {
            stateComponent.waitingStates.Add(new StateElement(StateType.Detectable, false));
        }

        //Agent
        agent.isStopped = false;
        agent.SetDestination(targetPosition);

        //Audio
        audioComponent.play = true;
    }


    public override void UpdateCommand() {
        if (stateComponent.movingState < 0) return;


        if (otherMovementTransform == null)
        {
            distanceOffset = movementTransform.position - targetPosition;
        }
        else
        {
            distanceOffset = movementTransform.position - otherMovementTransform.position;
        }

        if (movementTransform != null) {
            //Target reached -> distance to target active
            if (distanceToTarget > 0) {
                if (distanceOffset.sqrMagnitude <= distanceToTarget * distanceToTarget)
                { 
                    isActive = false;
                    return;
                }
            }
            else
            {
                //Target reached -> distance to target NOT active
                if (distanceOffset.sqrMagnitude <= stoppingDistance * stoppingDistance)
                {
                    isActive = false;
                    return;
                }
            }
        }
    }


    public override void AfterCommand()
    {
        base.AfterCommand();

        distanceToTarget = -1;
        otherMovementTransform = null;

        if (disablesDetection)
        {
            stateComponent.waitingStates.Add(new StateElement(StateType.Detectable, true));
        }

        agent.isStopped = true;
        agent.velocity = nullVec;

        audioComponent.play = false;
    }

}
