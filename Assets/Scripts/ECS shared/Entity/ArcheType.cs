using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class ArcheType : ScriptableObject
{
    [NonSerialized] public bool init;
    [NonSerialized] public List<EntityComponent> components = new List<EntityComponent>();


    /// <summary>
    /// Initializes the archetype and adds the components to the list.
    /// </summary>
    public virtual void Init()
    {
        if (!init)
        {
            init = true;
        }
    }


    /// <summary>
    /// Adds a list of entitycomponents to the list of components stored in the archetype.
    /// </summary>
    /// <param name="_components"></param>
    public void AddComponentsToList(params EntityComponent[] _components)
    {
        for (int i = 0; i < _components.Length; i++)
        {
            components.Add(_components[i]);
        }
    }
}
