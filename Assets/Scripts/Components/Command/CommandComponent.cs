using UnityEngine;
using System;


[CreateAssetMenu(fileName = "CommandComponent", menuName = "ScriptableObjects/Components Configs/CommandComponent")]
public class CommandComponent : EntityComponent
{
    //Flags
    [NonSerialized] public bool giveCommand;
    [NonSerialized] public bool continueCommanding;

    //RayCast
    [NonSerialized] public float maxRayCastLength;
    [NonSerialized] public LayerMask mouseClickLayerMask;

    //Vector
    [NonSerialized] public Vector3 nullVec = Vector3.zero;
}
