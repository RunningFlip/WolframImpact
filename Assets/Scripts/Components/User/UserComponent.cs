using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UserComponent", menuName = "ScriptableObjects/Components Configs/UserComponent")]
public class UserComponent : EntityComponent
{
    public bool alive = true;
    public UserObject userObject;


    [Header("Faction Entity List")]
    public List<int> factionEntities = new List<int>(); //Object list of units and buildings
}
