using System.Collections.Generic;
using UnityEngine;


public class BulletSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(BulletComponent), typeof(CollisionComponent)).Without(typeof(DeactiveTagComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            Entity entity = entities[i];
            int entityID = entity.GetEntityID();

            BulletComponent bullet = ComponentManager.GetComponent<BulletComponent>(entityID);
            CollisionComponent collision = ComponentManager.GetComponent<CollisionComponent>(entityID);

            if (!bullet.init)
            {
                bullet.init = true;
                BulletComponentSetup(bullet, collision, entity.entityGameObject, entityID);

                ComponentManager.CreateComponent<DeactiveTagComponent>(entityID);
                entity.entityGameObject.SetActive(false);
                continue;
            }


            if (bullet.destroy)
            {
                DestroyBullet(bullet, entityID);
                continue;
            }

            bullet.distanceOffset = bullet.startPosition - bullet.bulletTransform.position;

            if (bullet.maxRange > 0)
            {
                if (bullet.distanceOffset.sqrMagnitude >= bullet.maxRange * bullet.maxRange)
                {
                    DestroyBullet(bullet, entityID);
                    continue;
                }
            }

            //Bullet movement
            bullet.bulletTransform.position += bullet.direction * bullet.speed * Time.deltaTime;
            bullet.rotationVec = TransformHelper.GetDirection(bullet.bulletTransform.position + bullet.direction, bullet.bulletTransform.position);
            bullet.bulletTransform.rotation = TransformHelper.LookToDirection(bullet.rotationVec);
        }
    }


    /// <summary>
    /// Setups all neccessary properties of the bullet component.
    /// </summary>
    /// <param name="_bulletComponent"></param>
    private void BulletComponentSetup(BulletComponent _bulletComponent, CollisionComponent _collision, GameObject _entityGameObject, int _entityId)
    {
        //Components
        _bulletComponent.bulletObject = _entityGameObject;
        _bulletComponent.bulletTransform = _entityGameObject.transform;

        //Event on CollisionComponent
        _collision.onTrigger.AddListener(delegate { CheckCollision(_bulletComponent, _collision, _entityId); });
    }


    /// <summary>
    /// Disables the bullet, so it can be reused through the pool.
    /// </summary>
    /// <param name="_bullet"></param>
    private void DestroyBullet(BulletComponent _bullet, int _entityId)
    {
        _bullet.onCollision.Invoke();

        _bullet.shootConfig.bulletObjects.Add(_bullet.bulletObject);
        _bullet.bulletObject.SetActive(false);

        ComponentManager.CreateComponent<DeactiveTagComponent>(_entityId);
    }


    /// <summary>
    /// Checks if the collision is valid and applys damage if neccessary.
    /// </summary>
    /// <param name="_bullet"></param>
    /// <param name="_collision"></param>
    /// <param name="_entityId"></param>
    private void CheckCollision(BulletComponent _bullet, CollisionComponent _collision, int _entityId)
    {
        GameObject otherObject = _collision.lastTriggerObject;
        GameObjectEntityHolder otherEntityHolder = otherObject.GetComponent<GameObjectEntityHolder>();

        if (otherEntityHolder == null)
        {
            EntityHolderLink link = otherObject.GetComponent<EntityHolderLink>();

            if (link != null)
            {
                otherEntityHolder = link.entityHolder;
            }
        }

        if (otherEntityHolder != null)
        {
            HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(otherEntityHolder.GetEntityID());

            if (healthComponent != null)
            {
                FactionComponent factionComponent = ComponentManager.GetComponent<FactionComponent>(otherEntityHolder.GetEntityID());

                if (factionComponent.userIndex != _bullet.userIndex)
                {
                    DamageReceiverComponent damageReceiverComponent = ComponentManager.GetComponent<DamageReceiverComponent>(otherEntityHolder.GetEntityID());
                    damageReceiverComponent.damageElements.Add(new DamageElement(_bullet.damageType, _bullet.damage));
                    DestroyBullet(_bullet, _entityId);
                }
            }
        }
        else
        {
            DestroyBullet(_bullet, _entityId);
        }
    }
}
