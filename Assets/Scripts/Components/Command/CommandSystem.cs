using System.Collections.Generic;
using UnityEngine;

public class CommandSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(CommandComponent), typeof(CameraComponent), typeof(SelectionComponent), typeof(UserComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            CommandComponent commandTarget = ComponentManager.GetComponent<CommandComponent>(entityID);
            SelectionComponent selection = ComponentManager.GetComponent<SelectionComponent>(entityID);
            CameraComponent cam = ComponentManager.GetComponent<CameraComponent>(entityID);
            UserComponent user = ComponentManager.GetComponent<UserComponent>(entityID);

            if (!commandTarget.init)
            {
                commandTarget.init = true;

                //RayCast
                commandTarget.maxRayCastLength = GameController.Instance.GameParameter.maxMouseRayCastLength;
                commandTarget.mouseClickLayerMask = GameController.Instance.GameParameter.mouseClickLayerMask;
            }


            //Command was given
            if (commandTarget.giveCommand) 
            {
                commandTarget.giveCommand = false;
                CheckCommand(commandTarget, selection, user, cam.cam);
            }
        }
    }


    /// <summary>
    /// Checks the given command and if it's legit, the command will be converted to a task.
    /// </summary>
    private void CheckCommand(CommandComponent _command, SelectionComponent _selection, UserComponent _user, Camera _cam)
    {
        if (_cam == null) return;

        for (int i = 0; i < _selection.selectedEntityIds.Count; i++)
        {
            int selectedId = _selection.selectedEntityIds[i];
            FactionComponent myFactionComponent = ComponentManager.GetComponent<FactionComponent>(selectedId);
            bool isMyIndex = myFactionComponent.userIndex == _user.userObject.userIndex;

            if (!isMyIndex) return; //return because the unit or building is not mine

            bool commandIsValid = false;

            int targetEntityId = -1;
            GameObjectEntityHolder currentTarget = null;
            Vector3 currentPosition = _command.nullVec;

            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition); //Raycast

            if (Physics.Raycast(ray, out hit, _command.maxRayCastLength, _command.mouseClickLayerMask))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.CompareTag("Selectable"))
                    {
                        GameObjectEntityHolder entityHolder = hit.transform.GetComponent<EntityHolderLink>().entityHolder;
                        targetEntityId = entityHolder.GetEntityID();
                        currentTarget = entityHolder;
                        currentPosition = currentTarget.transform.position;
                        commandIsValid = true;
                    }

                    if (hit.transform.CompareTag("Ground"))
                    {
                        targetEntityId = -1;
                        currentTarget = null;
                        currentPosition = hit.point;
                        commandIsValid = true;
                    }
                }
            }

            if (commandIsValid)
            {
                bool targetIsMyIndex = false;

                if (currentTarget != null)
                {
                    FactionComponent otherFactionComponent = ComponentManager.GetComponent<FactionComponent>(targetEntityId);
                    targetIsMyIndex = otherFactionComponent.userIndex == _user.userObject.userIndex;
                }

                CommandTargetComponent myCommandTargetComponent = ComponentManager.GetComponent<CommandTargetComponent>(selectedId);

                SetCommand(_command, myCommandTargetComponent, currentTarget, currentPosition, targetEntityId, targetIsMyIndex);
            }
        }
    }


    /// <summary>
    /// Creates a task from the given command.
    /// </summary>
    /// <param name="_commandTarget"></param>
    /// <param name="_currentTarget"></param>
    /// <param name="_currentTargetPosition"></param>
    /// <param name="_targetIsMyIndex"></param>
    private void SetCommand(CommandComponent _command, CommandTargetComponent _commandTarget, GameObjectEntityHolder _currentTargetEntityHolder, Vector3 _currentTargetPosition, int _targetEntityId, bool _targetIsMyIndex)
    {
        TypeComponent typeComponent = ComponentManager.GetComponent<TypeComponent>(_targetEntityId);

        if (_commandTarget == null) return;

        CommandType commandType = CommandType.None;

        if (_currentTargetEntityHolder == null)
        {
            commandType = CommandType.Command_On_Ground;
        }
        else
        {
            HealthComponent targetHealth = ComponentManager.GetComponent<HealthComponent>(_targetEntityId);

            if (targetHealth.isDead) return;

            if (typeComponent.tagType == TagType.Building)
            {
                if (_targetIsMyIndex) commandType = CommandType.Command_Friendly_Building;
                else commandType = CommandType.Command_Enemy_Building;
            }

            if (typeComponent.tagType == TagType.Unit)
            {
                if (_targetIsMyIndex) commandType = CommandType.Command_Friendly_Unit;
                else commandType = CommandType.Command_Enemy_Unit;
            }
        }

        if (!_command.continueCommanding)
        {
            TaskHelper.ClearTasksAndAddNewTask(_commandTarget, commandType, _currentTargetEntityHolder, _currentTargetPosition);
        }
        else
        {
            TaskHelper.AddTask(_commandTarget, commandType, _currentTargetEntityHolder, _currentTargetPosition);
        }
    }
}
