using System.Collections.Generic;
using UnityEngine;


public class CollisionSystem : IComponentSystem
{
    //List of matching entities
    private List<IEntity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(CollisionComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            IEntity iEntity = entities[i];
            int entityID = iEntity.GetEntityID();

            CollisionComponent collisionComponent = ComponentManager.GetComponent<CollisionComponent>(entityID);

            if (!collisionComponent.init)
            {
                collisionComponent.init = true;
                InitializeLinks(iEntity, collisionComponent);
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
    /// <param name="_iEntity"></param>
    private void InitializeLinks(IEntity _iEntity, CollisionComponent _collisionComponent)
    {
        GameObject entityGameObject = ((Entity)_iEntity).entityGameObject;

        CollisionLink collisionLink = entityGameObject.GetComponent<CollisionLink>();
        TriggerLink triggerLink = entityGameObject.GetComponent<TriggerLink>();

        CollisionLink[] childrenCollisionLinks = entityGameObject.GetComponentsInChildren<CollisionLink>();
        TriggerLink[] childrenTriggerLinks = entityGameObject.GetComponentsInChildren<TriggerLink>();

        //Parent
        if (collisionLink != null)
        {
            collisionLink.Initialize(_collisionComponent);
        }
        if (triggerLink != null)
        {
            triggerLink.Initialize(_collisionComponent);
        }

        //Childrens
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
