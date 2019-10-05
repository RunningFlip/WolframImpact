using UnityEngine;


[CreateAssetMenu(fileName = "Bullet_Archetype", menuName = "ScriptableObjects/ArcheType Configs/Bullet_Archetype")]
public class Bullet_Archetype : ArcheType
{
    //Components
    private BulletComponent bulletComponent;
    private CollisionComponent collisionComponent;


    public override void Init()
    {
        if (init) return;
        base.Init();

        //Components
        bulletComponent = ScriptableObject.CreateInstance<BulletComponent>();
        collisionComponent = ScriptableObject.CreateInstance<CollisionComponent>();


        //Add components
        AddComponentsToList(
            bulletComponent,
            collisionComponent
            );
    }
}
