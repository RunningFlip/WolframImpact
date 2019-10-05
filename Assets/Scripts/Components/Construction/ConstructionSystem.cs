using System.Collections.Generic;
using UnityEngine;


public class ConstructionSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(ConstructionComponent), typeof(DevelopmentComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            ConstructionComponent constructionComponent = ComponentManager.GetComponent<ConstructionComponent>(entityID);
            DevelopmentComponent developmentComponent = ComponentManager.GetComponent<DevelopmentComponent>(entityID);

            if (!constructionComponent.init)
            {
                constructionComponent.init = true;
                constructionComponent.inConstruction = true;
            }


            //Constructing was canceld.
            if (constructionComponent.cancel)
            {
                CancelConstructing(constructionComponent, entityID);
                continue;
            }

            //Will build the construction as long as inConstruction is true.
            if (constructionComponent.inConstruction)
            {
                if (constructionComponent.passedTime >= constructionComponent.constructionTime)
                {
                    developmentComponent.developmentIsActive = true; //Activates the development component;
                    CancelConstructing(constructionComponent, entityID);
                    continue;
                }
                constructionComponent.passedTime += Time.deltaTime;
            }
        }
    }


    /// <summary>
    /// Cancel the constructing of the object and removes this component.
    /// </summary>
    private void CancelConstructing(ConstructionComponent constructionComponent, int _entityId)
    {
        constructionComponent.inConstruction = false;
        ComponentManager.DestroyComponent<ConstructionComponent>(_entityId);
    }
}
