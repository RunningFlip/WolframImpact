using System;
using UnityEngine;


[CreateAssetMenu(fileName = "EnergyComponent", menuName = "ScriptableObjects/Components Configs/EnergyComponent")]
public class EnergyComponent : EntityComponent
{
    [Header("Energy")]
    public float currentEnergy;   
    public float maxEnergy;


    //Last energy values
    [NonSerialized] public float lastCurrentEnergy;
    [NonSerialized] public float lastMaxEnergy;

    //Events
    [NonSerialized] public SimpleEvent onCurrentEnergyChanged = new SimpleEvent();
    [NonSerialized] public SimpleEvent onMaxEnergyChanged = new SimpleEvent();
}
