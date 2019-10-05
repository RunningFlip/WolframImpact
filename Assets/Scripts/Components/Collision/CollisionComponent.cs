using System;
using UnityEngine;


[CreateAssetMenu(fileName = "CollisionComponent", menuName = "ScriptableObjects/Components Configs/CollisionComponent")]
public class CollisionComponent : EntityComponent
{
    public bool checkCollision = true;
    public bool checkTrigger = true;


    //Last collision object
    [NonSerialized] public GameObject lastColisionlObject;
    [NonSerialized] public GameObject backupCollisionObject;
    [NonSerialized] public Collision collision;

    //Last trigger object
    [NonSerialized] public GameObject lastTriggerObject;
    [NonSerialized] public GameObject backupTriggerObject;
    [NonSerialized] public Collider trigger;

    //Event
    [NonSerialized] public SimpleEvent onCollision = new SimpleEvent();
    [NonSerialized] public SimpleEvent onTrigger = new SimpleEvent();
}
