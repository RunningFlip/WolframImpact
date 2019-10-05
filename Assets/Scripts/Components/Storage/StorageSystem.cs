using System.Collections.Generic;


public class StorageSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(StorageComponent), typeof(HealthComponent), typeof(FactionComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            StorageComponent storageComponent = ComponentManager.GetComponent<StorageComponent>(entityID);

            if (!storageComponent.init)
            {
                storageComponent.init = true;

                FactionComponent factionComponent = ComponentManager.GetComponent<FactionComponent>(entityID);
                ResourceComponent resourceComponent = GetResourceComponent(entityID);

                AddToResourceComponent(storageComponent, resourceComponent, entityID);
            }
        }
    }


    /// <summary>
    /// Returns the resource component that depends to the user with the userindex of the entity.
    /// </summary>
    /// <param name="_userIndex"></param>
    /// <returns></returns>
    private ResourceComponent GetResourceComponent(int _userIndex)
    {
        ResourceComponent resourceComponent = null;

        for (int i = 0; i < GameController.Instance.userEntityIds.Count; i++)
        {
            int entityId = GameController.Instance.userEntityIds[i];
            UserComponent userComponent = ComponentManager.GetComponent<UserComponent>(entityId);

            if (userComponent.userObject.userIndex == _userIndex)
            {
                resourceComponent = ComponentManager.GetComponent<ResourceComponent>(entityId);
            }
        }
        return resourceComponent;
    }


    /// <summary>
    /// Add this component to a specific resource list
    /// </summary>
    private void AddToResourceComponent(StorageComponent _storage, ResourceComponent _resourceComponent, int _entityId)
    {
        HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(_entityId);

        switch (_storage.storageType)
        {
            case StorageType.Energy:
                _resourceComponent.energyStoragesToAdd.Add(_storage);
                healthComponent.onDeath.AddListener(delegate { _resourceComponent.energyStoragesToRemove.Add(_storage); });
                break;

            case StorageType.Mass:
                _resourceComponent.massStoragesToAdd.Add(_storage);
                healthComponent.onDeath.AddListener(delegate { _resourceComponent.massStoragesToRemove.Add(_storage); });
                break;

            case StorageType.ObjectCap:
                _resourceComponent.objectStoragesToAdd.Add(_storage);
                healthComponent.onDeath.AddListener(delegate { _resourceComponent.massStoragesToRemove.Add(_storage); });
                break;
        }
    }
}
