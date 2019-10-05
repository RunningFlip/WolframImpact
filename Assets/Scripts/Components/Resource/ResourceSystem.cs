using System.Collections.Generic;
using UnityEngine;


public class ResourceSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(ResourceComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            ResourceComponent resource = ComponentManager.GetComponent<ResourceComponent>(entityID);

            if (!resource.init)
            {
                resource.init = true;
                ResourceComponentSetup(resource);
            }


            resource.passedTime += Time.deltaTime;

            if (resource.passedTime >= resource.harvestTick)
            {
                resource.passedTime = 0f;
                TickResources(resource);
            }

            //Generator
            CheckGeneratorToAdd(resource);
            CheckGeneratorToRemove(resource);

            //Storage
            CheckStorageToAdd(resource);
            CheckStorageToRemove(resource);

            //Checks values that changes
            CheckValues(resource);
        }
    }

    #region Setup

    /// <summary>
    /// Setups all neccessary properties of the given resource component.
    /// </summary>
    /// <param name="_resource"></param>
    private void ResourceComponentSetup(ResourceComponent _resource)
    {
        GameParameter parameter = GameController.Instance.GameParameter;

        //Events - UI
        _resource.onEnergyCapChanged.AddListener(delegate { GameController.Instance.uiController.UpdateEnergyStorageUI(_resource.energyCap); });
        _resource.onCurrentEnergyChanged.AddListener(delegate { GameController.Instance.uiController.UpdateEnergyValueUI(_resource.currentEnergy); });

        _resource.onMassCapChanged.AddListener(delegate { GameController.Instance.uiController.UpdateMassStorageUI(_resource.massCap); });
        _resource.onCurrentMassChanged.AddListener(delegate { GameController.Instance.uiController.UpdateMassValueUI(_resource.currentMass); });

        _resource.onObjectCapChanged.AddListener(delegate { GameController.Instance.uiController.UpdateObjectCapUI(_resource.objectCap, _resource.currentObjectSpace); });


        //Inits
        _resource.harvestTick = parameter.resourceTickStart;

        _resource.energyCap = parameter.energyCapStart;
        _resource.massCap = parameter.massCapStart;
        _resource.objectCap = parameter.objectCapStart;
        _resource.currentEnergy = parameter.energyStart;
        _resource.currentMass = parameter.massStart;


        //Last values
        _resource.lastEnergyCap = _resource.energyCap;
        _resource.lastCurrentEnergy = _resource.currentEnergy;

        _resource.lastMassCap = _resource.massCap;
        _resource.lastCurrentMass = _resource.currentMass;

        _resource.lastObjectCap = _resource.objectCap;


        //Inits resource UI
        _resource.onEnergyCapChanged.Invoke();
        _resource.onCurrentEnergyChanged.Invoke();
        _resource.onMassCapChanged.Invoke();
        _resource.onCurrentMassChanged.Invoke();
        _resource.onObjectCapChanged.Invoke();
    }

    #endregion Setup


    /// <summary>
    /// 
    /// </summary>
    private void TickResources(ResourceComponent _resource)
    {
        //Energy
        for (int i = 0; i < _resource.energyGenerators.Count; i++)
        {
            _resource.currentEnergy += _resource.energyGenerators[i].amountPerTick;
        }

        //Mass
        for (int i = 0; i < _resource.massGenerators.Count; i++)
        {
            _resource.currentMass += _resource.massGenerators[i].amountPerTick;
        }

        //Clamp
        _resource.currentEnergy = Mathf.Clamp(_resource.currentEnergy, 0, _resource.energyCap);
        _resource.currentMass = Mathf.Clamp(_resource.currentMass, 0, _resource.massCap);
    }


    /// <summary>
    /// 
    /// </summary>
    private void CheckGeneratorToAdd(ResourceComponent _resource)
    {
        //Energy
        if (_resource.energyGeneratorsToAdd.Count > 0)
        {
            for (int i = 0; i < _resource.energyGeneratorsToAdd.Count; i++)
            {
                _resource.energyGenerators.Add(_resource.energyGeneratorsToAdd[i]);
            }
            _resource.energyGeneratorsToAdd.Clear();
        }

        //Mass
        if (_resource.massGeneratorsToAdd.Count > 0)
        {
            for (int i = 0; i < _resource.massGeneratorsToAdd.Count; i++)
            {
                _resource.massGenerators.Add(_resource.massGeneratorsToAdd[i]);
            }
            _resource.massGeneratorsToAdd.Clear();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void CheckGeneratorToRemove(ResourceComponent _resource)
    {
        //Energy
        if (_resource.energyGeneratorsToRemove.Count > 0)
        {
            for (int i = 0; i < _resource.energyGeneratorsToRemove.Count; i++)
            {
                _resource.energyGenerators.Remove(_resource.energyGeneratorsToRemove[i]);
            }
            _resource.energyGeneratorsToRemove.Clear();
        }

        //Mass
        if (_resource.massGeneratorsToRemove.Count > 0)
        {
            for (int i = 0; i < _resource.massGeneratorsToRemove.Count; i++)
            {
                _resource.massGenerators.Add(_resource.massGeneratorsToRemove[i]);
            }
            _resource.massGeneratorsToRemove.Clear();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void CheckStorageToAdd(ResourceComponent _resource)
    {
        //Energy
        if (_resource.energyStoragesToAdd.Count > 0)
        {
            for (int i = 0; i < _resource.energyStoragesToAdd.Count; i++)
            {
                _resource.energyStorages.Add(_resource.energyStoragesToAdd[i]);
                _resource.energyCap += _resource.energyStorages[i].storageAmount;
            }
            _resource.energyStoragesToAdd.Clear();
        }

        //Mass
        if (_resource.massStoragesToAdd.Count > 0)
        {
            for (int i = 0; i < _resource.massStoragesToAdd.Count; i++)
            {
                _resource.massStorages.Add(_resource.massStoragesToAdd[i]);
                _resource.massCap += _resource.massStorages[i].storageAmount;
            }
            _resource.massStoragesToAdd.Clear();
        }

        //Objects
        if (_resource.objectStoragesToAdd.Count > 0)
        {
            for (int i = 0; i < _resource.objectStoragesToAdd.Count; i++)
            {
                _resource.objectStorages.Add(_resource.objectStoragesToAdd[i]);
                _resource.objectCap += _resource.objectStorages[i].storageAmount;
            }
            _resource.objectStoragesToAdd.Clear();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void CheckStorageToRemove(ResourceComponent _resource)
    {
        //Energy
        if (_resource.energyStoragesToRemove.Count > 0)
        {
            for (int i = 0; i < _resource.energyStoragesToRemove.Count; i++)
            {
                _resource.energyStorages.Add(_resource.energyStoragesToRemove[i]);
                _resource.energyCap -= _resource.energyStorages[i].storageAmount;
            }
            _resource.energyStoragesToRemove.Clear();

            if (_resource.energyCap < 0) _resource.energyCap = 0;
        }

        //Mass
        if (_resource.massStoragesToRemove.Count > 0)
        {
            for (int i = 0; i < _resource.massStoragesToRemove.Count; i++)
            {
                _resource.massStorages.Add(_resource.massStoragesToRemove[i]);
                _resource.massCap -= _resource.massStorages[i].storageAmount;
            }
            _resource.massStoragesToRemove.Clear();

            if (_resource.massCap < 0) _resource.massCap = 0;
        }

        //Objects
        if (_resource.objectStoragesToRemove.Count > 0)
        {
            for (int i = 0; i < _resource.objectStoragesToRemove.Count; i++)
            {
                _resource.objectStorages.Add(_resource.objectStoragesToRemove[i]);
                _resource.objectCap -= _resource.objectStorages[i].storageAmount;
            }
            _resource.objectStoragesToRemove.Clear();

            if (_resource.objectCap < 0) _resource.objectCap = 0;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void CheckValues(ResourceComponent _resource)
    {
        //Cap - Energy
        if (_resource.energyCap != _resource.lastEnergyCap)
        {
            _resource.lastEnergyCap = _resource.energyCap;

            if (_resource.energyCap < _resource.currentEnergy)
            {
                _resource.currentEnergy = _resource.energyCap;
            }
            _resource.onEnergyCapChanged.Invoke();
        }

        //Cap - Mass
        if (_resource.massCap != _resource.lastMassCap)
        {
            _resource.lastMassCap = _resource.massCap;

            if (_resource.massCap < _resource.currentMass)
            {
                _resource.currentMass = _resource.massCap;
            }
            _resource.onMassCapChanged.Invoke();
        }

        //Cap - Object space
        if (_resource.objectCap != _resource.lastObjectCap)
        {
            _resource.lastObjectCap = _resource.objectCap;

            if (_resource.objectCap < _resource.currentObjectSpace)
            {
                _resource.currentObjectSpace = _resource.objectCap;
            }
            _resource.onObjectCapChanged.Invoke();
        }

        //Current energy
        if (_resource.currentEnergy != _resource.lastCurrentEnergy)
        {
            _resource.lastCurrentEnergy = _resource.currentEnergy;
            _resource.onCurrentEnergyChanged.Invoke();
        }

        //Current mass
        if (_resource.currentMass != _resource.lastCurrentMass)
        {
            _resource.lastCurrentMass = _resource.currentMass;
            _resource.onCurrentMassChanged.Invoke();
        }

        //Current object space
        if (_resource.currentObjectSpace != _resource.lastCurrentObjects)
        {
            _resource.lastCurrentObjects = _resource.currentObjectSpace;
            _resource.onObjectCapChanged.Invoke();
        }
    }
}
