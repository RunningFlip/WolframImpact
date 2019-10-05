using UnityEngine;


[CreateAssetMenu(fileName = "StorageComponent", menuName = "ScriptableObjects/Components Configs/StorageComponent")]
public class StorageComponent : EntityComponent
{
    [Header("Storage values")]
    public int storageAmount;
    public StorageType storageType;
}
