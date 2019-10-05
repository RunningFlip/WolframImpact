using UnityEngine;
using System;


[CreateAssetMenu(fileName = "DetectionComponent", menuName = "ScriptableObjects/Components Configs/DetectionComponent")]
public class DetectionComponent : EntityComponent
{
    [Header("Detection values")]
    public bool masterDetection = true;
    public bool detects = true;
    public float detectionRange;
    [Range(0, 100)]
    public int maxHits = 10;


    //Flags
    [NonSerialized] public bool wasDetectingBeforeDeath;

    //Time
    [NonSerialized] public float updateTime;
    [NonSerialized] public float lastUpdate = -float.MaxValue;

    //Detection
    [NonSerialized] public LayerMask detectionMask;
    [NonSerialized] public Vector3 forwardVec = Vector3.forward;
    [NonSerialized] public Vector3 detectionOffset;

    //Hits
    [NonSerialized] public RaycastHit[] hits;

    //Components
    [NonSerialized] public Transform movementTransform;
}
