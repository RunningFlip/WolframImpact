using System.Collections.Generic;
using UnityEngine;


public struct HealthSystem : IComponentSystem
{
    //List of matching entities
    private List<IEntity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {        
        entities = EntityManager.Entities.ForEach(typeof(HealthComponent), typeof(DamageReceiverComponent), typeof(StateComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            IEntity iEntity = entities[i];
            int entityID = iEntity.GetEntityID();

            HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(entityID);
            DamageReceiverComponent damageReceiverComponent = ComponentManager.GetComponent<DamageReceiverComponent>(entityID);
            StateComponent stateComponent = ComponentManager.GetComponent<StateComponent>(entityID);

            int damageCount = damageReceiverComponent.damageElements.Count;

            //Apply damage
            if (!healthComponent.isDead && stateComponent.damageState >= 0 && damageCount > 0)
            {
                for (int j = 0; j < damageCount; j++)
                {
                    ApplyDamage(healthComponent, damageReceiverComponent.damageElements[j]);
                }
                damageReceiverComponent.damageElements.Clear();
            }

            //Current health
            if (healthComponent.currentHealth != healthComponent.lastCurrentHealth)
            {
                healthComponent.lastCurrentHealth = Mathf.Clamp(healthComponent.currentHealth, 0, healthComponent.maxHealth);
                healthComponent.currentHealth = healthComponent.lastCurrentHealth;
                UpdateCurrentHealth(healthComponent, damageReceiverComponent, stateComponent, iEntity);
            }

            //Max health
            if (healthComponent.maxHealth != healthComponent.lastMaxHealth)
            {
                healthComponent.lastMaxHealth = healthComponent.maxHealth;
                UpdateMaxHealth(healthComponent);
            }

            //Death trigger
            if (healthComponent.deathTrigger)
            {
                if (healthComponent.passedDeathTimer >= healthComponent.deathDelayTime)
                {
                    healthComponent.deathTrigger = false;
                    healthComponent.passedDeathTimer = 0;

                    Kill(healthComponent, damageReceiverComponent, stateComponent, iEntity);
                }
                healthComponent.passedDeathTimer += Time.deltaTime;
            }
        }
    }


    /// <summary>
    /// Applies damage to a healthcomponent.
    /// </summary>
    /// <param name="_healthComponent"></param>
    /// <param name="_damageElement"></param>
    private void ApplyDamage(HealthComponent _healthComponent, DamageElement _damageElement)
    {
        float resistanceMultiplier = 0;

        for (int i = 0; i < _healthComponent.resistances.Length; i++)
        {
            Resistance resistance = _healthComponent.resistances[i];

            if (resistance.damageType == _damageElement.damageType)
            {
                resistanceMultiplier = resistance.resistanceValue;
                resistanceMultiplier /= 100;
                break;
            }
        }

        float damageValue = _damageElement.damage * (1 - resistanceMultiplier);
        _healthComponent.currentHealth -= _damageElement.damage;
    }


    /// <summary>
    /// Updates the current health and triggers the redrawing of the health GUI.
    /// </summary>
    /// <param name="_healthComponent"></param>
    /// <param name="_damageReceiverComponent"></param>
    private void UpdateCurrentHealth(HealthComponent _healthComponent, DamageReceiverComponent _damageReceiverComponent, StateComponent _stateComponent, IEntity _iEntity)
    {
        if (!_healthComponent.deathTrigger)
        {
            if (_healthComponent.currentHealth <= 0)
            {
                Kill(_healthComponent, _damageReceiverComponent, _stateComponent, _iEntity);
            }
            else if (_healthComponent.isDead && _healthComponent.currentHealth > 0)
            {
                Revive(_healthComponent, _damageReceiverComponent, _stateComponent);
            }
            TriggerUpdateGUI(_healthComponent);
        }        
    }


    /// <summary>
    /// Updates the max health and triggers the redrawing of the health GUI.
    /// </summary>
    /// <param name="_healthComponent"></param>
    /// <param name="_damageReceiverComponent"></param>
    private void UpdateMaxHealth(HealthComponent _healthComponent)
    {
        _healthComponent.currentHealth = Mathf.Clamp(_healthComponent.currentHealth, 0, _healthComponent.maxHealth);
        TriggerUpdateGUI(_healthComponent);
    }


    /// <summary>
    /// Triggers the death of the entity.
    /// </summary>
    private void Kill(HealthComponent _healthComponent, DamageReceiverComponent _damageReceiverComponent, StateComponent _stateComponent, IEntity _iEntity)
    {
        _healthComponent.isDead = true;
        _damageReceiverComponent.receivable = false;
        _healthComponent.currentHealth = 0;
        _healthComponent.lastCurrentHealth = 0;

        _stateComponent.waitingStates.Add(new StateElement(StateType.Damageable, false));
        _stateComponent.waitingStates.Add(new StateElement(StateType.Collideable, false));

        switch (_healthComponent.deathType)
        {
            case DeathType.Destroy:
                GameObject entityGameObject = ((Entity)_iEntity).entityGameObject;
                EntityManager.DestroyEntity(_iEntity.GetEntityID());
                GameObject.Destroy(entityGameObject);
                break;

            case DeathType.Disable:
                break;
        }
    }


    /// <summary>
    /// Triggers the revive of the entity.
    /// </summary>
    /// <param name="_healthComponent"></param>
    /// <param name="_damageReceiverComponent"></param>
    private void Revive(HealthComponent _healthComponent, DamageReceiverComponent _damageReceiverComponent, StateComponent _stateComponent)
    {
        _healthComponent.isDead = false;
        _damageReceiverComponent.receivable = true;
        _healthComponent.currentHealth = Mathf.Clamp(_healthComponent.currentHealth, 0, _healthComponent.maxHealth);
        _healthComponent.lastCurrentHealth = _healthComponent.currentHealth;

        _stateComponent.waitingStates.Add(new StateElement(StateType.Damageable, true));
        _stateComponent.waitingStates.Add(new StateElement(StateType.Collideable, true));
    }


    /// <summary>
    /// Triggers to update the health GUI.
    /// </summary>
    /// <param name="_healthComponent"></param>
    private void TriggerUpdateGUI(HealthComponent _healthComponent)
    {
        //TODO
    }
}
