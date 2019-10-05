using UnityEngine;
using System;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "ResourceComponent", menuName = "ScriptableObjects/Components Configs/ResourceComponent")]
public class ResourceComponent : EntityComponent
{
    [Header("Resources")]
    public int energyCap;
    public int currentEnergy;
    [Space]
    public int massCap;
    public int currentMass;
    [Space]
    public int objectCap;
    public int currentObjectSpace;


    //Generator - Energy
    [NonSerialized] public List<GeneratorComponent> energyGenerators = new List<GeneratorComponent>();
    [NonSerialized] public List<GeneratorComponent> energyGeneratorsToAdd = new List<GeneratorComponent>();
    [NonSerialized] public List<GeneratorComponent> energyGeneratorsToRemove = new List<GeneratorComponent>();

    //Generator - Mass
    [NonSerialized] public List<GeneratorComponent> massGenerators = new List<GeneratorComponent>();
    [NonSerialized] public List<GeneratorComponent> massGeneratorsToAdd = new List<GeneratorComponent>();
    [NonSerialized] public List<GeneratorComponent> massGeneratorsToRemove = new List<GeneratorComponent>();

    //Storage - Energy
    [NonSerialized] public List<StorageComponent> energyStorages = new List<StorageComponent>();
    [NonSerialized] public List<StorageComponent> energyStoragesToAdd = new List<StorageComponent>();
    [NonSerialized] public List<StorageComponent> energyStoragesToRemove = new List<StorageComponent>();

    //Storage - Mass
    [NonSerialized] public List<StorageComponent> massStorages = new List<StorageComponent>();
    [NonSerialized] public List<StorageComponent> massStoragesToAdd = new List<StorageComponent>();
    [NonSerialized] public List<StorageComponent> massStoragesToRemove = new List<StorageComponent>();

    //Storage - Objects
    [NonSerialized] public List<StorageComponent> objectStorages = new List<StorageComponent>();
    [NonSerialized] public List<StorageComponent> objectStoragesToAdd = new List<StorageComponent>();
    [NonSerialized] public List<StorageComponent> objectStoragesToRemove = new List<StorageComponent>();

    //Harvest
    [NonSerialized] public float harvestTick;
    [NonSerialized] public float passedTime;

    //Last values
    [NonSerialized] public int lastEnergyCap;
    [NonSerialized] public int lastCurrentEnergy;
    [NonSerialized] public int lastMassCap;
    [NonSerialized] public int lastCurrentMass;
    [NonSerialized] public int lastObjectCap;
    [NonSerialized] public int lastCurrentObjects;

    //Energy    - Events
    [NonSerialized] public SimpleEvent onEnergyCapChanged = new SimpleEvent();
    [NonSerialized] public SimpleEvent onCurrentEnergyChanged = new SimpleEvent();

    //Mass      - Events
    [NonSerialized] public SimpleEvent onMassCapChanged = new SimpleEvent();
    [NonSerialized] public SimpleEvent onCurrentMassChanged = new SimpleEvent();

    //Objects   - Events
    [NonSerialized] public SimpleEvent onObjectCapChanged = new SimpleEvent();
}
