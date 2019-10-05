using UnityEngine;


[CreateAssetMenu(fileName = "DeathComponent", menuName = "ScriptableObjects/Components Configs/DeathComponent")]
public class DeathComponent : EntityComponent
{
    public float delayTime;
    public DeathType deathType;
}
