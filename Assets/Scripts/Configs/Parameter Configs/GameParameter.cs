using UnityEngine;

[CreateAssetMenu(fileName = "Game_Parameter_Config", menuName = "ScriptableObjects/Parameter Configs/Game Parameter Config")]
public class GameParameter : ScriptableObject {

    [Header("Player Controller Prefab")]
    public GameObject userControllerPrefab_Player;
    public GameObject userControllerPrefab_AI;

    [Header("Base Spawn Values")]
    public float coWorkerSpawnRadius;
    public int coWorkerAmount;
    public ConstructionConfig baseConfig;
    public DevelopmentConfig coWorkerConfig;

    [Header("Raycast")]
    public float maxMouseRayCastLength;
    public float minimapFrustumRayCastLength;
    public LayerMask mouseClickLayerMask;
    public LayerMask cameraBorderLayerMask;

    [Header("Selection")]
    public LayerMask groundLayerMask;
    public LayerMask selectionLayerMask;
    public int maxSelectableObjects;
    public int maxSavableSelections;

    [Header("Drag Selection Box")]
    public Color backgroundColor;
    public Color strokeColor;
    public int strokeThickness;
    public float magnitudeToSelect;

    [Header("Camera")]
    public float cameraSpeed;
    [Space]
    public float cameraRotationSpeed;
    [Space]
    public float cameraDefaultZoom;
    public float cameraMinZoom;
    public float cameraMaxZoom;
    public float cameraZoomSpeed;
    [Space]
    public int cameraTriggerSize;
    public float cameraTriggerMovementStep;
    [Space]
    public float cameraMapBorderWidth;

    [Header("Minimap")]
    public LayerMask minimapLayerMask;
    public Color viewportColor;

    [Header("Resources")]
    public float resourceTickStart;
    [Space]
    public int energyCapStart;
    public int massCapStart;
    public int objectCapStart;
    [Space]
    public int energyStart;
    public int massStart;

    [Header("Construction Values")]
    public float maxConstructionDistance;

    [Header("Detection")]
    public float detectionUpdateTime;
    public LayerMask detectionMask;
    public float maxTimeToFollow;
    public float maxDistanceToFollowedObject;

    [Header("Mouse")]
    public Texture2D cursorTexture;

    [Header("Development")]
    public int maxObjectsToBuild;
    [Space]
    public int developmentFailedVolume;
    public AudioClip developmentFailedClip;

    [Header("Command Button Popup")]
    public Color CommandPopupColor;
    public Color fontColor;
    public int fontSize;
    public int borderSize;
    public int headlineGap;
    public float popupTimer;

    [Header("Fonts")]
    public Font font;

    [Header("World Energy-/Helthbars")]
    public Gradient worldUIHealthGradient;
    public float defaultWoldBarAlpha = 100;

    [Header("Command Ground UI")]
    public GameObject UINodeMove;
    public GameObject UIStrokeMove;
    [Space(5)]
    public GameObject UINodePatrol;
    public GameObject UIStrokePatrol;
    [Space(5)]
    public GameObject UINodeAttack;
    public GameObject UIStrokeAttack;

}
