using System.Collections.Generic;
using UnityEngine;


public class EnergySystem : IComponentSystem
{
    //List of matching entities
    private List<IEntity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(EnergyComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            IEntity iEntity = entities[i];
            int entityID = iEntity.GetEntityID();

            EnergyComponent energyComponent = ComponentManager.GetComponent<EnergyComponent>(entityID);

            //Current energy
            if (energyComponent.currentEnergy != energyComponent.lastCurrentEnergy)
            {
                energyComponent.lastCurrentEnergy = energyComponent.currentEnergy;
                UpdateCurrentEnergy(energyComponent);
            }

            //Max energy
            if (energyComponent.maxEnergy != energyComponent.lastMaxEnergy)
            {
                energyComponent.lastMaxEnergy = energyComponent.maxEnergy;
                UpdateMaxEnergy(energyComponent);
            }
        }
    }


    /// <summary>
    /// Updates the current energy and triggers the redrawing of the energy GUI.
    /// </summary>
    /// <param name="_energyComponent"></param>
    private void UpdateCurrentEnergy(EnergyComponent _energyComponent)
    {
        _energyComponent.currentEnergy = Mathf.Clamp(_energyComponent.currentEnergy, 0, _energyComponent.maxEnergy);
        _energyComponent.lastCurrentEnergy = Mathf.Clamp(_energyComponent.lastCurrentEnergy, 0, _energyComponent.maxEnergy);

        TriggerUpdateGUI(_energyComponent);
    }


    /// <summary>
    /// Updates the max energy and triggers the redrawing of the energy GUI.
    /// </summary>
    /// <param name="_energyComponent"></param>
    private void UpdateMaxEnergy(EnergyComponent _energyComponent)
    {
        _energyComponent.currentEnergy = Mathf.Clamp(_energyComponent.currentEnergy, 0, _energyComponent.maxEnergy);

        TriggerUpdateGUI(_energyComponent);
    }


    /// <summary>
    /// Triggers to update the energy GUI.
    /// </summary>
    /// <param name="_healthComponent"></param>
    private void TriggerUpdateGUI(EnergyComponent _energyComponent)
    {
        //TODO
    }
}
