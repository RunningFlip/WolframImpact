using UnityEngine;


[CreateAssetMenu(fileName = "HealthComponent", menuName = "ScriptableObjects/Components Configs/HealthComponent")]
public class HealthComponent : EntityComponent
{
    [Header("Health")]
    public float currentHealth;
    public float lastCurrentHealth;
    [Space]
    public float maxHealth;
    public float lastMaxHealth;

    [Header("Resistance")]
    public Resistance[] resistances;

    [Header("Death")]
    public bool isDead;
    public bool deathTrigger;
    public float deathDelayTime;
    public float passedDeathTimer;
    public DeathType deathType;


    public void Setup(float _maxHealth, DeathType _deathType, float _deathDelayTime, Resistance[] _resistances = null, bool _isDead = false)
    {
        isDead = _isDead;

        deathDelayTime = _deathDelayTime;
        deathType = _deathType;

        currentHealth = _maxHealth;
        lastCurrentHealth = _maxHealth;

        maxHealth = _maxHealth;
        lastMaxHealth = _maxHealth;

        resistances = _resistances;
    }
}


