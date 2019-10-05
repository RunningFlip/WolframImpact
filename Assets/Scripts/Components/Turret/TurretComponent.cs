using UnityEngine;
using System;


[CreateAssetMenu(fileName = "TurretComponent", menuName = "ScriptableObjects/Components Configs/TurretComponent")]
public class TurretComponent : EntityComponent
{
    public TurretLink turretLink;


    //Flags
    [NonSerialized] public bool updateTurrets;
    [NonSerialized] public bool resetTurrets;

    //Vectors
    [NonSerialized] public Vector3 targetVector;
    [NonSerialized] public Vector3 nullVec = Vector3.zero;
}
