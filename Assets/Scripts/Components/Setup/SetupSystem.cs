using System.Collections.Generic;
using UnityEngine;

public class SetupSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(FactionComponent), typeof(InformationComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            Entity entity = entities[i];
            int entityID = entity.GetEntityID();

            FactionComponent faction = ComponentManager.GetComponent<FactionComponent>(entityID);

            if (faction.userIndex != faction.lastUserIndex)
            {
                faction.lastUserIndex = faction.userIndex;
                ColourObjects(entity, faction.userIndex);
            }

            if (!faction.init)
            {
                //Information
                InformationComponent information = ComponentManager.GetComponent<InformationComponent>(entityID);
                if (!information.init)
                {
                    SetupInformationComponent(information, faction, entityID);
                    information.init = true;
                }

                //Pool Holder
                PoolHolderComponent poolHolder = ComponentManager.GetComponent<PoolHolderComponent>(entityID);
                if (poolHolder != null && !poolHolder.init)
                {
                    SetupPoolHolder(poolHolder, entity);
                    poolHolder.init = true;
                }

                //Faction
                if (faction.userIndex == -1) continue;
                faction.init = true;
            }
        }
    }


    private void SetupPoolHolder(PoolHolderComponent _poolHolder, Entity _entity)
    {
        HealthComponent health = ComponentManager.GetComponent<HealthComponent>(_entity.GetEntityID());

        _poolHolder.poolingObjects = _entity.entityGameObject.GetComponentInChildren<PoolHolderLink>().pool;
        health.onDeath.AddListener(delegate { DestroyPool(_poolHolder); });
    }


    private void SetupInformationComponent(InformationComponent _information, FactionComponent _faction, int _entityId)
    {
        IdentifierComponent identifier = ComponentManager.GetComponent<IdentifierComponent>(_entityId);
        TypeComponent type = ComponentManager.GetComponent<TypeComponent>(_entityId);
        HealthComponent health = ComponentManager.GetComponent<HealthComponent>(_entityId);
        EnergyComponent energy = ComponentManager.GetComponent<EnergyComponent>(_entityId);
        CommandTargetComponent commandTarget = ComponentManager.GetComponent<CommandTargetComponent>(_entityId);

        _information.informationPackage.factionComponent = _faction;
        _information.informationPackage.identifierComponent = identifier;
        _information.informationPackage.typeComponent = type;
        _information.informationPackage.healthComponent = health;
        _information.informationPackage.energyComponent = energy;
        _information.informationPackage.commandTargetComponent = commandTarget;
    }


    private void ColourObjects(Entity _entity, int _userIndex)
    {
        FactionMaterialLink[] colourableLink = _entity.entityGameObject.GetComponentsInChildren<FactionMaterialLink>();

        for (int j = 0; j < colourableLink.Length; j++)
        {
            colourableLink[j].meshRenderer.material = GameController.Instance.userMaterials[_userIndex];
        }
    }


    private void DestroyPool(PoolHolderComponent _poolHolder)
    {
        for (int i = 0; i < _poolHolder.poolingObjects.Length; i++)
        {
            GameObject.Destroy(_poolHolder.poolingObjects[i]);
        }
    }
}
