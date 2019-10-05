using UnityEngine;


[CreateAssetMenu(fileName = "DeathSimpleComponent", menuName = "ScriptableObjects/Components Configs/DeathSimpleComponent")]
public class DeathComponent : EntityComponent
{
    public float delayTime;
    public DeathType deathType;


    public DeathComponent(DeathType _deathType, float _delayTime = 0f)
    {
        delayTime = _delayTime;
        deathType = _deathType;
    }
}
