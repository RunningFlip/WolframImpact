using System;
using UnityEngine;


[CreateAssetMenu(fileName = "SelectionTargetComponent", menuName = "ScriptableObjects/Components Configs/SelectionTargetComponent")]
public class SelectionTargetComponent : EntityComponent
{
    [Header("Selection UI")]
    public SelectionTypeUI selectionTypeUI;

    //Camera
    [NonSerialized] public Camera mainCam;
    [NonSerialized] public Transform mainCamTransform;

    //Flag
    [NonSerialized] public bool selected;

    //Colors
    [NonSerialized] public Gradient colorGradient;
    [NonSerialized] public Color deactiveColor;

    //Vectors
    [NonSerialized] public Vector3 vectorBack = Vector3.back;
    [NonSerialized] public Vector3 vectorUp = Vector3.up;

    //Events
    [NonSerialized] public SimpleEvent onSelectionStarts = new SimpleEvent();
    [NonSerialized] public SimpleEvent onSelectionEnds = new SimpleEvent();
}
