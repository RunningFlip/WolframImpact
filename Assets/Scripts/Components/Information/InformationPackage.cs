using System;
using UnityEngine;


[Serializable]
public class InformationPackage
{
    //Faction
    public FactionComponent factionComponent;

    //Type
    public TypeComponent typeComponent;

    //Identifier
    public IdentifierComponent identifierComponent;

    //Health
    public HealthComponent healthComponent;

    //Energy
    public EnergyComponent energyComponent;

    //Command Target
    public CommandTargetComponent commandTargetComponent;

    [Header("Damage")]
    public float damage = -1;
    public DamageType damageType = DamageType.Default;
}
