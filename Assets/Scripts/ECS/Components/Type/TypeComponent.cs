using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TypeComponent", menuName = "ScriptableObjects/Components Configs/TypeComponent")]
public class TypeComponent : EntityComponent
{
    [Header("Tag")]
    public TagType tagType;

    [Header("Object Type")]
    public ObjectType objectType;
    public List<ObjectType> targetObjectTypes;


    public void Setup(TagType _tagType, ObjectType _objectType, List<ObjectType> _targetObjectTypes)
    {
        tagType = _tagType;

        objectType = _objectType;
        targetObjectTypes = _targetObjectTypes;
    }
}
