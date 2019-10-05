using System.Collections.Generic;


public class StateSystem : IComponentSystem
{
    //List of matching entities
    private List<IEntity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(StateComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            StateComponent stateComponent = ComponentManager.GetComponent<StateComponent>(entityID);

            for (int j = 0; j < stateComponent.waitingStates.Count; j++)
            {
                SetState(stateComponent, stateComponent.waitingStates[j]);
            }
            stateComponent.waitingStates.Clear();
        }
    }


    /// <summary>
    /// Enables or disables a specific state.
    /// </summary>
    /// <param name="_stateType">State to enable.</param>
    /// /// <param name="_enable">Is true if the state will be enabled.</param>
    private void SetState(StateComponent _stateComponent, StateElement _stateElement)
    {
        int step = (_stateElement.enabled) ? 1 : -1;

        switch (_stateElement.stateType)
        {
            case StateType.Everything:
                _stateComponent.collidingState += step;
                _stateComponent.movingState += step;
                _stateComponent.attackingState += step;
                _stateComponent.damageState += step;
                _stateComponent.selectionState += step;
                break;

            case StateType.Collideable:
                _stateComponent.collidingState += step;
                break;

            case StateType.Moveable:
                _stateComponent.movingState += step;
                break;

            case StateType.Attackable:
                _stateComponent.attackingState += step;
                break;

            case StateType.Damageable:
                _stateComponent.damageState += step;
                break;

            case StateType.Selectable:
                _stateComponent.selectionState += step;
                break;
        }
    }
}
