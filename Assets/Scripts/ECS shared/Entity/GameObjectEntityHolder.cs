using System.Collections.Generic;
using UnityEngine;


public class GameObjectEntityHolder : MonoBehaviour
{
    //Entity specs
    [SerializeField] private int entityId;
    private EntityComponentPool entityComponentPool;
    [SerializeField] private List<EntityComponent> entityComponents = new List<EntityComponent>();

    //Archetype
    public ArcheType archeType;


    private void Awake()
    {
        //ID
        entityId = EntityManager.CreateEntity(gameObject);

        //Components
        entityComponentPool = ComponentManager.CreatePool(entityId, archeType);
        entityComponents = entityComponentPool.GetEntityComponents();
    }


    private void OnDestroy()
    {
        EntityManager.DestroyEntity(entityId);
    }


    /// <summary>
    /// Returns the entity id of the gameobject entity holder.
    /// </summary>
    /// <returns></returns>
    public int GetEntityID()
    {
        return entityId;
    }
}
