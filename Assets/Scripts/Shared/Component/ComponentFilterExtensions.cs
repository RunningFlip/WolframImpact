using System;
using System.Collections.Generic;


public static class ComponentFilterExtensions
{
    /// <summary>
    /// Returns a list of entities with specfic component types attached.
    /// </summary>
    /// <param name="_entities"></param>
    /// <param name="_componentTypes"></param>
    /// <returns></returns>
    public static List<IEntity> ForEach(this List<IEntity> _entities, params Type[] _componentTypes)
    {
        if (_entities.Count != 0)
        {
            _entities.RemoveAll(e => !ComponentManager.HasComponents(e.GetEntityID(), _componentTypes));
        }
        return _entities;
    }


    /// <summary>
    /// Returns a list of entities without specific component types attached.
    /// </summary>
    /// <param name="_entities"></param>
    /// <param name="_componentTypes"></param>
    /// <returns></returns>
    public static List<IEntity> Without(this List<IEntity> _entities, params Type[] _componentTypes)
    {
        if (_entities.Count != 0)
        {
            _entities.RemoveAll(e => ComponentManager.HasComponents(e.GetEntityID(), _componentTypes));
        }
        return _entities;
    }
}



//HELPFUL???
//  ar weapons = items.FindAll(item => (item.Find(attribute => attribute.GetType() == typeof(DamageAttribute)) != null))
