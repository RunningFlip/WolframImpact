using System.Collections.Generic;


public class GeneratorSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(GeneratorComponent), typeof(HealthComponent), typeof(FactionComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            GeneratorComponent generatorComponent = ComponentManager.GetComponent<GeneratorComponent>(entityID);

            if (!generatorComponent.init)
            {
                FactionComponent factionComponent = ComponentManager.GetComponent<FactionComponent>(entityID);

                if (!factionComponent.init) continue;

                generatorComponent.init = true;

                int userEntityId = GameController.Instance.GetUserEntitId(factionComponent.userIndex);
                ResourceComponent resourceComponent = ComponentManager.GetComponent<ResourceComponent>(userEntityId);

                AddToResourceComponent(generatorComponent, resourceComponent, entityID);
            }
        }
    }


    /// <summary>
    /// Add this component to a specific resource list
    /// </summary>
    private void AddToResourceComponent(GeneratorComponent _generator, ResourceComponent _resourceComponent, int _entityId)
    {
        HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(_entityId);

        switch (_generator.generatorType)
        {
            case GeneratorType.Energy:
                _resourceComponent.energyGeneratorsToAdd.Add(_generator);
                healthComponent.onDeath.AddListener(delegate { _resourceComponent.energyGeneratorsToRemove.Add(_generator); });
                break;

            case GeneratorType.Mass:
                _resourceComponent.massGeneratorsToAdd.Add(_generator);
                healthComponent.onDeath.AddListener(delegate { _resourceComponent.massGeneratorsToRemove.Add(_generator); });
                break;
        }
    }
}
