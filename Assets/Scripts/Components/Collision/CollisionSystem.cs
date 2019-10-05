using System.Collections.Generic;
using UnityEngine;


public class CollisionSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(CollisionComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            Entity entity = entities[i];
            int entityID = entity.GetEntityID();

            CollisionComponent collisionComponent = ComponentManager.GetComponent<CollisionComponent>(entityID);

            if (!collisionComponent.init)
            {
                collisionComponent.init = true;
                InitializeLinks(entity, collisionComponent);
            }
            else
            {
                //Collision
                if (collisionComponent.checkCollision && collisionComponent.lastColisionlObject != collisionComponent.backupCollisionObject)
                {
                    collisionComponent.backupCollisionObject = collisionComponent.lastColisionlObject;
                    collisionComponent.onCollision.Invoke();
                }

                //Trigger
                if (collisionComponent.checkTrigger && collisionComponent.lastTriggerObject != collisionComponent.backupTriggerObject)
                {
                    collisionComponent.backupTriggerObject = collisionComponent.lastTriggerObject;
                    collisionComponent.onTrigger.Invoke();
                }
            }
        }
    }


    /// <summary>
    /// Initializes the trigger and collision links that are attached on the entity
    /// gameobject and its childs.
    /// </summary>
    /// <param name="_entity"></param>
    private void InitializeLinks(Entity _entity, CollisionComponent _collisionComponent)
    {
        GameObject entityGameObject = _entity.entityGameObject;

        CollisionLink[] childrenCollisionLinks = entityGameObject.GetComponentsInChildren<CollisionLink>();
        TriggerLink[] childrenTriggerLinks = entityGameObject.GetComponentsInChildren<TriggerLink>();

        for (int i = 0; i < childrenCollisionLinks.Length; i++)
        {
            childrenCollisionLinks[i].Initialize(_collisionComponent);
        }
        for (int i = 0; i < childrenTriggerLinks.Length; i++)
        {
            childrenTriggerLinks[i].Initialize(_collisionComponent);
        }
    }
}
