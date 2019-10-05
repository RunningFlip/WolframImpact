using UnityEngine;


[CreateAssetMenu(fileName = "TypeComponent", menuName = "ScriptableObjects/Components Configs/TypeComponent")]
public class TypeComponent : EntityComponent
{
    [Header("Tag")]
    public TagType tagType;
    public bool isMilitaryObject;

    [Header("Object Type")]
    public ObjectType objectType;
    public ObjectType[] targetObjectTypes;
}
