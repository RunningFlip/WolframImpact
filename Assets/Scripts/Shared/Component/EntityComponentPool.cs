using System;
using System.Collections.Generic;


[Serializable]
public class EntityComponentPool 
{
    private int entityId;
    private Dictionary<Type, EntityComponent> componentTable;
    private List<EntityComponent> entityComponents;


    public EntityComponentPool(int _entityId)
    {
        entityId = _entityId;
        componentTable = new Dictionary<Type, EntityComponent>();
        entityComponents = new List<EntityComponent>();
    }


    public void Destroy()
    {
        componentTable = null;
    }


    /// <summary>
    /// Adds a component to the component-pool-table by a given id.
    /// </summary>
    /// <param name="_component">Id from the component that has to be added.</param>
    public void AddComponent(EntityComponent _component)
    {
        Type type = _component.GetType();
        if (!componentTable.ContainsKey(type))
        {
            componentTable.Add(_component.GetType(), _component);
            entityComponents.Add(_component);
        }
#if UNITY_EDITOR
        else
        {
            LogMessage.Warning(MessageType.ComponentAlreadyAttached, entityId);
        }
#endif
    }


    /// <summary>
    /// Removes a component.
    /// </summary>
    /// <param name="_component">IComponent that has to be romoved.</param>
    public void RemoveComponent(EntityComponent _component)
    {
        Type t = _component.GetType();
        if (componentTable.ContainsKey(t))
        {
            componentTable.Remove(t);
            entityComponents.Remove(_component);
        }
#if UNITY_EDITOR
        else
        {
            LogMessage.Warning(MessageType.NoComponentFound, entityId);
        }
#endif
    }


    /// <summary>
    /// Removes a component by a given type.
    /// </summary>
    /// <typeparam name="T">IComponent type.</typeparam>
    public void RemoveComponent<T>() where T : EntityComponent
    {
        if (componentTable.ContainsKey(typeof(T)))
        {
            EntityComponent component = componentTable[typeof(T)];
            componentTable.Remove(typeof(T));
            entityComponents.Remove(component);
        }
#if UNITY_EDITOR
        else
        {
            LogMessage.Warning(MessageType.NoComponentFound, entityId);
        }
#endif
    }


    /// <summary>
    /// Returns a component by a given type.
    /// </summary>
    /// <typeparam name="T">Type of the IComponent. </typeparam>
    /// <returns></returns>
    public T GetComponent<T>() where T : EntityComponent
    {
        if (componentTable.ContainsKey(typeof(T)))
        {
            return (T)componentTable[typeof(T)];
        }
#if UNITY_EDITOR
        else
        {
            LogMessage.Warning(MessageType.NoComponentFound, entityId);
            return default(T);
        }
#endif
    }


    /// <summary>
    /// Returns true if the component pool contains the given type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool HasComponent<T>() where T : EntityComponent
    {
        return componentTable.ContainsKey(typeof(T));
    }


    /// <summary>
    /// Returns true if the component pool contains the given type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool HasComponent(Type _type)
    {
        return componentTable.ContainsKey(_type);
    }


    /// <summary>
    /// Returns the entity id that belongs to the pool.
    /// </summary>
    /// <returns></returns>
    public int GetEntityId()
    {
        return entityId;
    }


    /// <summary>
    /// Returns a list of entity components.
    /// </summary>
    /// <returns></returns>
    public List<EntityComponent> GetEntityComponents()
    {
        return entityComponents;
    }
}
