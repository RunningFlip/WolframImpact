using System.Collections.Generic;
using UnityEngine;


public class GameObjectEntityHolder : MonoBehaviour
{
    //Entity specs
    [SerializeField] private int entityId;
    private EntityComponentPool entityComponentPool;
    public List<EntityComponent> entityComponents = new List<EntityComponent>();

    //Archetype
    public ArcheType archeType;


    private void Start()
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
}
