using System;
using UnityEngine;

[Serializable]
public class Entity : IEntity
{
    //ID
    private int entityID;
    private static int staticEntityID;
    public GameObject entityGameObject;


    /// <summary>
    /// Entity constructor
    /// </summary>
    public Entity(GameObject _gameObject)
    {
        staticEntityID++;
        entityID = staticEntityID;

        entityGameObject = _gameObject;

#if UNITY_EDITOR
        if (entityGameObject == null)
        {
            LogMessage.Error(MessageType.NoEntityGameObjectFound, entityID);
        }
#endif
        Register();
    }

    /// <summary>
    /// Entity destructor.
    /// </summary>
    ~Entity()
    {
        Unregister();
    }


    /// <summary>
    /// Returns the ID of this entity.
    /// </summary>
    /// <returns></returns>
    public int GetEntityID()
    {
        return entityID;
    }


    /// <summary>
    /// Registers the entity at the entity-manager.
    /// </summary>
    public void Register()
    {
        EntityManager.AddEntity(this);
    }


    /// <summary>
    /// Unregister the entity at the entity-manager.
    /// </summary>
    public void Unregister()
    {
        EntityManager.RemoveEntity(this);
    }
}
