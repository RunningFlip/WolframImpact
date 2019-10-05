using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "Map_Config", menuName = "ScriptableObjects/Map Configs/Map Config")]
public class MapConfig : ScriptableObject {

    [Header("Scene")]
    public string sceneName;

    [Header("Specifications")]
    public string mapName;
    public string mapDiscription;
    [Space]
    [Range(2, 8)]
    public int minPlayers = 2;
    [Range(2, 8)]
    public int maxPlayers = 2;
    [Space]
    public Sprite picture;

    

}
