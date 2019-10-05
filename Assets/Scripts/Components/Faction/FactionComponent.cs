using UnityEngine;
using System;


[CreateAssetMenu(fileName = "FactionComponent", menuName = "ScriptableObjects/Components Configs/FactionComponent")]
public class FactionComponent : EntityComponent
{
    public int userIndex = -1;
    [NonSerialized] public int lastUserIndex = -1;
}
