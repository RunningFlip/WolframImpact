using UnityEngine;
using System;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "SelectionComponent", menuName = "ScriptableObjects/Components Configs/SelectionComponent")]
public class SelectionComponent : EntityComponent
{
    public bool enemySelected;
    public bool friendSelected;


    //Flags
    [NonSerialized] public bool selectByClick;
    [NonSerialized] public bool mouseDown;
    [NonSerialized] public bool continueSelection;
    [NonSerialized] public bool resetSelection;
    [NonSerialized] public bool destroySelection;

    //Selection
    [NonSerialized] public LayerMask selectionLayerMask;
    [NonSerialized] public LayerMask groundLayerMask;
    [NonSerialized] public List<int> selectedEntityIds = new List<int>();
    [NonSerialized] public Dictionary<int, InformationPackage> selectionTable = new Dictionary<int, InformationPackage>();
    [NonSerialized] public GameObject[] dragSelectionObjects;
    [NonSerialized] public float magnitudeToSelect;
    [NonSerialized] public int maxSelectableObjects;

    //Number of the current selection number where the current selection has to be saved to.
    [NonSerialized] public int selectionSetNum = -1;
    [NonSerialized] public SelectionSetArray selectionSetArray; //Saved selections

    //Selection drag
    [NonSerialized] public bool dragStarted;
    [NonSerialized] public Vector3 currentMousePosition;
    [NonSerialized] public Vector3 mouseStartPosition;
    [NonSerialized] public Color dragSelectionBackgroundColor;
    [NonSerialized] public Color dragSelectionStrokeColor;
    [NonSerialized] public int dragSelectionStrokeThickness;

    //NullVec
    [NonSerialized] public Vector3 nullVec = Vector3.zero;

    //Raycast
    [NonSerialized] public float maxRayCastLength;

    //Events
    [NonSerialized] public SimpleEvent onSelectionChanged = new SimpleEvent();
}
