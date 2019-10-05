using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Lobby_Parameter_Config", menuName = "ScriptableObjects/Parameter Configs/Lobby Parameters Config")]
public class LobbyParameter : ScriptableObject {

    [Header("Player Colors")]
    public FactionColorObject[] userColors;

    [Header("Maps")]
    public List<MapConfig> mapConfigs;

}
