using UnityEngine;
using System;


[CreateAssetMenu(fileName = "BulletComponent", menuName = "ScriptableObjects/Components Configs/BulletComponent")]
public class BulletComponent : EntityComponent
{
    //Flag
    [NonSerialized] public bool destroy;

    //Damage
    [NonSerialized] public float damage;
    [NonSerialized] public DamageType damageType;

    //Movement
    [NonSerialized] public float speed;
    [NonSerialized] public float maxRange;

    //Given parameters
    [NonSerialized] public int userIndex;
    [NonSerialized] public Vector3 startPosition;
    [NonSerialized] public Vector3 direction;
    [NonSerialized] public CommandShootConfig shootConfig;

    //Components
    [NonSerialized] public Transform bulletTransform;
    [NonSerialized] public GameObject bulletObject;

    //Vector
    [NonSerialized] public Vector3 rotationVec;
    [NonSerialized] public Vector3 distanceOffset;

    //Events
    [NonSerialized] public SimpleEvent onCollision = new SimpleEvent();
}
