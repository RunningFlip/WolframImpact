using System;
using UnityEngine;


[CreateAssetMenu(fileName = "CameraComponent", menuName = "ScriptableObjects/Components Configs/CameraComponent")]
public class CameraComponent : EntityComponent
{
    [Header("Movement values")]
    public bool useCameraMovement = true;
    public float moveSpeed;
    
    [Header("Zoom values")]
    public float defaultZoom;
    public float minZoom;
    public float maxZoom;
    public float zoomSpeed;

    [Header("Rotation values")]
    public bool resetRotation;
    public float rotationSpeed;


    //Movement
    [NonSerialized] public Vector3 movement;
    [NonSerialized] public Vector3 nullVector = Vector3.zero;
    [NonSerialized] public Vector3 desireMove;
    [NonSerialized] public Vector3 dir;
    [NonSerialized] public int borderTriggerSize;
    [NonSerialized] public float triggerMovementStep;

    //Zoom
    [NonSerialized] public Vector3 zoomVec;
    [NonSerialized] public float zoom; //only 1 or -1
    [NonSerialized] public bool resetZoom;
    [NonSerialized] public Vector3 resetVector;

    //Rotation
    [NonSerialized] public float rotation; //only 1 or -1
    [NonSerialized] public Quaternion resetQuaternion;

    //Fokus
    [NonSerialized] public bool focus;

    //Camera components
    [NonSerialized] public Transform cameraTransform;
    [NonSerialized] public Camera cam;

    //Other components
    [NonSerialized] public int selectionsBefore;
    [NonSerialized] public int currentSelection;
}
