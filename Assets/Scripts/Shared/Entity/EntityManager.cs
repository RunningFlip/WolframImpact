using System.Collections.Generic;
using UnityEngine;


public class EntityManager
{
    //Table that holds all entities.
    //<Entity ID, IEntity>
    private static Dictionary<int, IEntity> entityTable;
    private static List<IEntity> allEntities;
    public static List<IEntity> Entities
    {
        get
        {
            return new List<IEntity>(allEntities);
        }
    }


    public EntityManager()
    {
        entityTable = new Dictionary<int, IEntity>();
        allEntities = new List<IEntity>();
    }


    /// <summary>
    /// Creates an entity and returns its id.
    /// </summary>
    /// <returns></returns>
    public static int CreateEntity(GameObject _gameObject)
    {
        IEntity newEntity = new Entity(_gameObject);
        return newEntity.GetEntityID();
    }


    /// <summary>
    /// Destroys an entity by a given id.
    /// </summary>
    /// <param name="_entityID">Id from the entity that has be to destroyed.</param>
    public static void DestroyEntity(int _entityID)
    {
        IEntity iEntity = GetEntity(_entityID);
        Entity entity = (Entity)iEntity;

        RemoveEntity(iEntity);
        ComponentManager.DestroyPool(_entityID);

        entity = null;
    }


    /// <summary>
    /// Returns an entity by a given id.
    /// </summary>
    /// <param name="_entityID">Id from the entity that has be to returned.</param>
    /// <returns></returns>
    public static IEntity GetEntity(int _entityID)
    {
        return entityTable[_entityID];
    }


    /// <summary>
    /// Adds a entity with its specific id to the entity-table.
    /// </summary>
    /// <param name="_entity">Entity to add.</param>
    public static void AddEntity(IEntity _entity)
    {
        int id = _entity.GetEntityID();
        if (!entityTable.ContainsKey(id))
        {
            entityTable.Add(id, _entity);
            allEntities.Add(_entity);
        }
#if UNITY_EDITOR
        else
        {
            LogMessage.Error(MessageType.EntityAlreadyExists, id);
        }
#endif
    }


    /// <summary>
    /// Removes a entity with its specific id from the entity-table.
    /// </summary>
    /// <param name="_entity">Entity to remove.</param>
    public static void RemoveEntity(IEntity _entity)
    {
        entityTable.Remove(_entity.GetEntityID());
        allEntities.Remove(_entity);
    }
}
