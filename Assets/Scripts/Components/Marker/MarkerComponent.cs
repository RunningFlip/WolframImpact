using UnityEngine;
using System;


[CreateAssetMenu(fileName = "MarkerComponent", menuName = "ScriptableObjects/Components Configs/MarkerComponent")]
public class MarkerComponent : EntityComponent
{
    [Header("Marker object")]
    public GameObject markerObject;


    //Flag
    [NonSerialized] public bool firstSetted;

    //Components
    [NonSerialized] public SelectionTargetComponent selectionTargetComponent;
    [NonSerialized] public Transform markerTransform;
}
