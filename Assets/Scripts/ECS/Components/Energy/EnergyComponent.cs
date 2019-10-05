using UnityEngine;


[CreateAssetMenu(fileName = "EnergyComponent", menuName = "ScriptableObjects/Components Configs/EnergyComponent")]
public class EnergyComponent : EntityComponent
{
    [Header("Energy")]
    public float currentEnergy;
    public float lastCurrentEnergy;   
    [Space]
    public float maxEnergy;
    public float lastMaxEnergy;


    public void Setup(float _maxEnergy)
    {
        currentEnergy = _maxEnergy;
        lastCurrentEnergy = _maxEnergy;

        maxEnergy = _maxEnergy;
        lastMaxEnergy = _maxEnergy;
    }
}
