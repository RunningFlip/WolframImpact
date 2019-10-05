using UnityEngine;


[CreateAssetMenu(fileName = "PoolHolderComponent", menuName = "ScriptableObjects/Components Configs/PoolHolderComponent")]
public class PoolHolderComponent : EntityComponent
{
    public GameObject[] poolingObjects;
}
