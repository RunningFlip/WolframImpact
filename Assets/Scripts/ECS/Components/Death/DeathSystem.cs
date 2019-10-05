using System.Collections.Generic;


public class DeathSystem : IComponentSystem
{
    //List of matching entities
    private List<IEntity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(DeathComponent), typeof(HealthComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(entityID);
            DeathComponent deathComponent = ComponentManager.GetComponent<DeathComponent>(entityID);
            
            if (!healthComponent.isDead && !healthComponent.deathTrigger)
            {
                healthComponent.deathTrigger = true;
                healthComponent.deathDelayTime = deathComponent.delayTime;
                healthComponent.deathType = deathComponent.deathType;
            }

            ComponentManager.DestroyComponent<DeathComponent>(entityID);
        }
    }
}
