using UnityEngine;
using System;


[CreateAssetMenu(fileName = "ConstructionComponent", menuName = "ScriptableObjects/Components Configs/ConstructionComponent")]
public class ConstructionComponent : EntityComponent
{
    [Header("Construction values")]
    public bool inConstruction;
    public float constructionTime;


    //Flags
    [NonSerialized] public bool cancel;

    //Time
    [NonSerialized] public float passedTime;
}
