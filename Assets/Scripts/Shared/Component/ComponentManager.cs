using System;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager
{
    //Component table
    //<Entity ID, ComponentPool>
    private static Dictionary<int, EntityComponentPool> componentPoolTable;


    public ComponentManager()
    {
        componentPoolTable = new Dictionary<int, EntityComponentPool>();
    }


    /// <summary>
    /// Creates a component and adds it to a given entity id.
    /// </summary>
    /// <param name="_entityId"></param>
    /// <returns>Id from the component that was to be created.</returns>
    public static T CreateComponent<T>(int _entityId) where T : EntityComponent
    {
        T newComponent = ScriptableObject.CreateInstance<T>();

        if (!componentPoolTable.ContainsKey(_entityId))
        {
            EntityComponentPool newPool = CreatePool(_entityId);
            newPool.AddComponent(newComponent);
        }
        else
        {
            componentPoolTable[_entityId].AddComponent(newComponent);
        }
        return newComponent;
    }


    /// <summary>
    /// Returns a component by a given id.
    /// </summary>
    /// <param name="_entityId"></param>
    /// <returns>Id from the component that has to be returned.</returns>
    public static T GetComponent<T>(int _entityId) where T : EntityComponent
    {
        if (componentPoolTable.ContainsKey(_entityId))
        {
            return componentPoolTable[_entityId].GetComponent<T>();
        }
        return default(T);
    }


    /// <summary>
    /// Destroys a component by a given id.
    /// </summary>
    /// <param name="_entityId">Id from the component that has to be destroyed.</param>
    public static void DestroyComponent<T>(int _entityId) where T : EntityComponent
    {
        if (componentPoolTable.ContainsKey(_entityId))
        {
            EntityComponentPool pool = componentPoolTable[_entityId];
            EntityComponent component = pool.GetComponent<T>();
            pool.RemoveComponent(component);
            component = null;
        }
#if UNITY_EDITOR
        else
        {
            LogMessage.Info(MessageType.NoComponentFound, _entityId, typeof(T));
        }
#endif
    }


    /// <summary>
    /// Returns true if the entity has the given component attached.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_entityId"></param>
    /// <returns></returns>
    public static bool HasComponent<T>(int _entityId) where T : EntityComponent
    {
        if (componentPoolTable.ContainsKey(_entityId))
        {
            EntityComponentPool pool = componentPoolTable[_entityId];
            return pool.HasComponent<T>();
        }
        return false;       
    }


    /// <summary>
    /// Returns true if the entity has the given component attached.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_entityId"></param>
    /// <returns></returns>
    public static bool HasComponents(int _entityId, params Type[] _componentTypes)
    {
        if (componentPoolTable.ContainsKey(_entityId))
        {
            bool hasComponent = true;
            EntityComponentPool pool = componentPoolTable[_entityId];

            for (int i = 0; i < _componentTypes.Length; i++)
            {
                if (!pool.HasComponent(_componentTypes[i]))
                {
                    hasComponent = false;
                    break;
                }
            }
            return hasComponent;
        }
        return false;
    }


    /// <summary>
    /// Creates a component pool that belongs to the given entity id.
    /// </summary>
    /// <param name="_entityId"></param>
    /// <returns></returns>
    public static EntityComponentPool CreatePool(int _entityId, ArcheType _archeType = null)
    {
        if (componentPoolTable.ContainsKey(_entityId))
        {
            EntityComponentPool pool = componentPoolTable[_entityId];
            if (_archeType != null) AddComponentsFromArcheType(pool, _archeType);
            return pool;
        }
        else
        {
            EntityComponentPool newPool = new EntityComponentPool(_entityId);
            componentPoolTable.Add(_entityId, newPool);
            if (_archeType != null) AddComponentsFromArcheType(newPool, _archeType);
            return newPool;
        }
    }


    /// <summary>
    /// Adds a set of components of a given archetype to an entity.
    /// </summary>
    private static void AddComponentsFromArcheType(EntityComponentPool _pool, ArcheType _archeType)
    {
        _archeType.Init();

        int count = _archeType.components.Count;

        for (int i = 0; i < count; i++)
        {
            _pool.AddComponent(ScriptableObject.Instantiate(_archeType.components[i]));
        }
    }


    /// <summary>
    /// Removes a whole entity-component-pool.
    /// </summary>
    /// <param name="_entityId">Removes the pool by a given id.</param>
    public static void DestroyPool(int _entityId)
    {
        if (componentPoolTable.ContainsKey(_entityId))
        {
            componentPoolTable.Remove(_entityId);
        }
    }
}
