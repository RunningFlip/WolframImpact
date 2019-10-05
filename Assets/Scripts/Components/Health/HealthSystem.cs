using System.Collections.Generic;
using UnityEngine;


public struct HealthSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {        
        entities = EntityManager.Entities.ForEach(typeof(HealthComponent), typeof(DamageReceiverComponent), typeof(StateComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            Entity entity = entities[i];
            int entityID = entity.GetEntityID();

            HealthComponent health = ComponentManager.GetComponent<HealthComponent>(entityID);
            DamageReceiverComponent damageReceiver = ComponentManager.GetComponent<DamageReceiverComponent>(entityID);
            StateComponent state = ComponentManager.GetComponent<StateComponent>(entityID);

            if (!health.init)
            {
                health.init = true;
                health.currentHealth = health.maxHealth;
            }

            int damageCount = damageReceiver.damageElements.Count;

            //Apply damage
            if (!health.isDead && state.damageState >= 0 && damageCount > 0)
            {
                for (int j = 0; j < damageCount; j++)
                {
                    ApplyDamage(health, damageReceiver.damageElements[j]);
                }
                damageReceiver.damageElements.Clear();
            }

            //Current health
            if (health.currentHealth != health.lastCurrentHealth)
            {
                health.lastCurrentHealth = Mathf.Clamp(health.currentHealth, 0, health.maxHealth);
                health.currentHealth = health.lastCurrentHealth;
                UpdateCurrentHealth(health, damageReceiver, state, entity);
                health.onCurrentHealthChanged.Invoke();
            }

            //Max health
            if (health.maxHealth != health.lastMaxHealth)
            {
                health.lastMaxHealth = health.maxHealth;
                UpdateMaxHealth(health);
                health.onMaxHealthChanged.Invoke();
            }

            //Death trigger
            if (health.deathTrigger)
            {
                if (health.passedDeathTimer >= health.deathDelayTime)
                {
                    health.deathTrigger = false;
                    health.passedDeathTimer = 0;

                    Kill(health, damageReceiver, state, entity);
                }
                health.passedDeathTimer += Time.deltaTime;
            }
        }
    }


    /// <summary>
    /// Applies damage to a healthcomponent.
    /// </summary>
    /// <param name="_health"></param>
    /// <param name="_damageElement"></param>
    private void ApplyDamage(HealthComponent _health, DamageElement _damageElement)
    {
        float resistanceMultiplier = 0;

        for (int i = 0; i < _health.resistances.Length; i++)
        {
            Resistance resistance = _health.resistances[i];

            if (resistance.damageType == _damageElement.damageType)
            {
                resistanceMultiplier = resistance.resistanceValue;
                resistanceMultiplier /= 100;
                break;
            }
        }

        float damageValue = _damageElement.damage * (1 - resistanceMultiplier);
        _health.currentHealth -= _damageElement.damage;
    }


    /// <summary>
    /// Updates the current health and triggers the redrawing of the health GUI.
    /// </summary>
    /// <param name="_health"></param>
    /// <param name="_damageReceiverComponent"></param>
    private void UpdateCurrentHealth(HealthComponent _health, DamageReceiverComponent _damageReceiverComponent, StateComponent _stateComponent, Entity _entity)
    {
        if (!_health.deathTrigger)
        {
            if (_health.currentHealth <= 0)
            {
                Kill(_health, _damageReceiverComponent, _stateComponent, _entity);
            }
            else if (_health.isDead && _health.currentHealth > 0)
            {
                Revive(_health, _damageReceiverComponent, _stateComponent);
            }
            TriggerUpdateGUI(_health);
        }        
    }


    /// <summary>
    /// Updates the max health and triggers the redrawing of the health GUI.
    /// </summary>
    /// <param name="_health"></param>
    /// <param name="_damageReceiverComponent"></param>
    private void UpdateMaxHealth(HealthComponent _health)
    {
        _health.currentHealth = Mathf.Clamp(_health.currentHealth, 0, _health.maxHealth);
        TriggerUpdateGUI(_health);
    }


    /// <summary>
    /// Triggers the death of the entity.
    /// </summary>
    private void Kill(HealthComponent _health, DamageReceiverComponent _damageReceiver, StateComponent _state, Entity _entity)
    {
        _health.isDead = true;
        _damageReceiver.receivable = false;
        _health.currentHealth = 0;
        _health.lastCurrentHealth = 0;

        _health.onDeath.Invoke();

        _state.waitingStates.Add(new StateElement(StateType.Damageable, false));
        _state.waitingStates.Add(new StateElement(StateType.Collideable, false));

        switch (_health.deathType)
        {
            case DeathType.Destroy:
                UnregisterEntityFromUser(_entity.GetEntityID());
                GameObject entityGameObject = _entity.entityGameObject;
                GameObject.Destroy(entityGameObject);
                break;

            case DeathType.Disable:
                UnregisterEntityFromUser(_entity.GetEntityID());
                GameObject.Destroy(_entity.entityGameObject.GetComponent<GameObjectEntityHolder>());
                break;
        }
    }


    /// <summary>
    /// Unregister this entity from its user by the given entityid.
    /// </summary>
    /// <param name="_entityId"></param>
    private void UnregisterEntityFromUser(int _entityId)
    {
        int userIndex = ComponentManager.GetComponent<FactionComponent>(_entityId).userIndex;
        UserHelper.RemoveObject(userIndex, _entityId);
    }


    /// <summary>
    /// Triggers the revive of the entity.
    /// </summary>
    /// <param name="_health"></param>
    /// <param name="_damageReceiver"></param>
    private void Revive(HealthComponent _health, DamageReceiverComponent _damageReceiver, StateComponent _state)
    {
        _health.isDead = false;
        _damageReceiver.receivable = true;
        _health.currentHealth = Mathf.Clamp(_health.currentHealth, 0, _health.maxHealth);
        _health.lastCurrentHealth = _health.currentHealth;

        _health.onRevive.Invoke();

        _state.waitingStates.Add(new StateElement(StateType.Damageable, true));
        _state.waitingStates.Add(new StateElement(StateType.Collideable, true));
    }


    /// <summary>
    /// Triggers to update the health GUI.
    /// </summary>
    /// <param name="_health"></param>
    private void TriggerUpdateGUI(HealthComponent _health)
    {
        //TODO
    }
}
