using UnityEngine;
using System;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "MiniMapComponent", menuName = "ScriptableObjects/Components Configs/MiniMapComponent")]
public class MiniMapComponent : EntityComponent
{
    //Minimap Values
    [NonSerialized] public MiniMapLink miniMapLink;

    //Camera
    [NonSerialized] public Camera gameplayCamera;
    [NonSerialized] public Transform gameplayCamTrans;

    //Vectors

    [NonSerialized] public Vector3 mousePosition;
    [NonSerialized] public Vector3 minimapOrigin;
    [NonSerialized] public Vector3 screenSize;
    [NonSerialized] public Vector3 camPosOnLevelBackground;

    [NonSerialized] public LayerMask minimapMask;

    //Ray length
    [NonSerialized] public float rayCastLength;

    //Rays
    [NonSerialized] public Ray topLeftCorner;
    [NonSerialized] public Ray topRightCorner;
    [NonSerialized] public Ray bottomLeftCorner;
    [NonSerialized] public Ray bottomRightCorner;

    //Hits
    [NonSerialized] public RaycastHit[] hits = new RaycastHit[4];

    //Other components
    [NonSerialized] public Button minimapButton;

    //MiniMap renderer link
    [NonSerialized] public MiniMapRendererLink rendererLink;
}
