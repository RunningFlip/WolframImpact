using UnityEngine;


public class Map : MonoBehaviour
{
    [Header("Map Ground")]
    public BoxCollider mapGroundCollider;
    public BoxCollider mapBackgroundCollider;

    [Header("Map Spawns")]
    public Transform[] factionSpawnPoints;
}
