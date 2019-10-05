using System.Collections.Generic;
using UnityEngine;


public class EntityManager
{
    //Table that holds all entities.
    //<Entity ID, IEntity>
    private static Dictionary<int, Entity> entityTable = new Dictionary<int, Entity>();
    private static List<Entity> allEntities = new List<Entity>();
    public static List<Entity> Entities
    {
        get
        {
            return new List<Entity>(allEntities);
        }
    }



    /// <summary>
    /// Creates an entity and returns its id.
    /// </summary>
    /// <returns></returns>
    public static int CreateEntity(GameObject _gameObject)
    {
        Entity newEntity = new Entity(_gameObject);
        return newEntity.GetEntityID();
    }


    /// <summary>
    /// Destroys an entity by a given id.
    /// </summary>
    /// <param name="_entityID">Id from the entity that has be to destroyed.</param>
    public static void DestroyEntity(int _entityID)
    {
        Entity entity = GetEntity(_entityID);

        RemoveEntity(entity);
        ComponentManager.DestroyPool(_entityID);

        entity = null;
    }


    /// <summary>
    /// Returns an entity by a given id.
    /// </summary>
    /// <param name="_entityID">Id from the entity that has be to returned.</param>
    /// <returns></returns>
    public static Entity GetEntity(int _entityID)
    {
        Entity entity = entityTable[_entityID];
        return entity;
    }


    /// <summary>
    /// Returns true if the entity with the given id exists.
    /// </summary>
    /// <param name="_entityID"></param>
    /// <returns></returns>
    public static bool EntityExists(int _entityID)
    {
        return entityTable.ContainsKey(_entityID);
    }


    /// <summary>
    /// Adds a entity with its specific id to the entity-table.
    /// </summary>
    /// <param name="_entity">Entity to add.</param>
    public static void AddEntity(Entity _entity)
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
    public static void RemoveEntity(Entity _entity)
    {
        entityTable.Remove(_entity.GetEntityID());
        allEntities.Remove(_entity);
    }
}
