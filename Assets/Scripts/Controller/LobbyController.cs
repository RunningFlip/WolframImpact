using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;


    [Header("Lobby Parameters")]
    public LobbyParameter LobbyParameters;

    [Header("Lobby UI Controller")]
    public LobbyUIController uiController;


    //Sorted map list
    [NonSerialized]
    public List<MapConfig> sortedMaps;

    //Players
    private int minPlayers = 2;
    private int maxPlayers = 8;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Maps
        sortedMaps = LobbyParameters.mapConfigs.OrderBy(x => x.mapName).ToList();
        uiController.UpdateMapPanelList(ref sortedMaps);

        //Player init
        SetMapConfig(sortedMaps[0]);
    }


    public void SetMapConfig(MapConfig _config)
    {
        minPlayers = _config.minPlayers;
        maxPlayers = _config.maxPlayers;

        uiController.UpdatePlayerPanels(minPlayers, maxPlayers);
    }

}
