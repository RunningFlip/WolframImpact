using System.Collections.Generic;
using UnityEngine;


public class EnergySystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(EnergyComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            int entityID = entities[i].GetEntityID();

            EnergyComponent energy = ComponentManager.GetComponent<EnergyComponent>(entityID);

            if (!energy.init)
            {
                energy.init = true;
                energy.currentEnergy = energy.maxEnergy;
            }

            //Current energy
            if (energy.currentEnergy != energy.lastCurrentEnergy)
            {
                energy.lastCurrentEnergy = energy.currentEnergy;
                UpdateCurrentEnergy(energy);
                energy.onCurrentEnergyChanged.Invoke();
            }

            //Max energy
            if (energy.maxEnergy != energy.lastMaxEnergy)
            {
                energy.lastMaxEnergy = energy.maxEnergy;
                UpdateMaxEnergy(energy);
                energy.onMaxEnergyChanged.Invoke();
            }
        }
    }


    /// <summary>
    /// Updates the current energy and triggers the redrawing of the energy GUI.
    /// </summary>
    /// <param name="_energy"></param>
    private void UpdateCurrentEnergy(EnergyComponent _energy)
    {
        _energy.currentEnergy = Mathf.Clamp(_energy.currentEnergy, 0, _energy.maxEnergy);
        _energy.lastCurrentEnergy = Mathf.Clamp(_energy.lastCurrentEnergy, 0, _energy.maxEnergy);

        TriggerUpdateGUI(_energy);
    }


    /// <summary>
    /// Updates the max energy and triggers the redrawing of the energy GUI.
    /// </summary>
    /// <param name="_energy"></param>
    private void UpdateMaxEnergy(EnergyComponent _energy)
    {
        _energy.currentEnergy = Mathf.Clamp(_energy.currentEnergy, 0, _energy.maxEnergy);

        TriggerUpdateGUI(_energy);
    }


    /// <summary>
    /// Triggers to update the energy GUI.
    /// </summary>
    /// <param name="_healthComponent"></param>
    private void TriggerUpdateGUI(EnergyComponent _energy)
    {
        //TODO
    }
}
