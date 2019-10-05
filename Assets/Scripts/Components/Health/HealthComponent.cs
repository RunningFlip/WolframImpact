using UnityEngine;
using System;


[CreateAssetMenu(fileName = "HealthComponent", menuName = "ScriptableObjects/Components Configs/HealthComponent")]
public class HealthComponent : EntityComponent
{
    [Header("Health")]
    public float currentHealth;
    [NonSerialized] public float lastCurrentHealth;
    public float maxHealth;
    [NonSerialized] public float lastMaxHealth;

    [Header("Resistance")]
    public Resistance[] resistances;

    [Header("Death")]
    public bool isDead;
    public bool deathTrigger;
    public float deathDelayTime;
    public float passedDeathTimer;
    public DeathType deathType;


    //Event
    [NonSerialized] public SimpleEvent onDeath = new SimpleEvent();
    [NonSerialized] public SimpleEvent onRevive = new SimpleEvent();
    [NonSerialized] public SimpleEvent onCurrentHealthChanged = new SimpleEvent();
    [NonSerialized] public SimpleEvent onMaxHealthChanged = new SimpleEvent();
}


