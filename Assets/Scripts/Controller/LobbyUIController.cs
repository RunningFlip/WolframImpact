using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;


public class LobbyUIController : MonoBehaviour
{
    [Header("General Buttons")]
    public Button backToMenuButton;

    [Header("Lobby Buttons")]
    public Button loadMapButton;

    [Header("User Panels")]
    public UserPanel[] userPanels;

    [Header("Map Panel")]
    public Dropdown mapDropdown;
    [Space]
    public Image mapPicture;
    public Text mapName;
    public Text mapSettings;
    public Text mapDiscription;

    [Header("Lobby Controller")]
    public LobbyController lobbyController;


    //Strings
    private readonly string PREFIX = "Players: ";
    private readonly string BETWEEN = " - ";

    //Color pallet
    private List<Sprite> colorPallet = new List<Sprite>();
    private List<Sprite> startColorPallet = new List<Sprite>();

    //Map
    private List<MapConfig> sortedMaps;
    private List<string> mapNames = new List<string>();


    private void Awake() {
        //List init
        for (int i = 0; i < lobbyController.LobbyParameters.userColors.Length; i++)
        {
            colorPallet.Add(lobbyController.LobbyParameters.userColors[i].colorSprite);
        }
        for (int i = 0; i < colorPallet.Count; i++)
        {
            startColorPallet.Add(colorPallet[i]);
        }
        SetPlayerPanelColors();

        //General
        backToMenuButton.onClick.AddListener(delegate { SceneManager.LoadScene(0); });

        //Lobby
        loadMapButton.onClick.AddListener(delegate { LoadScene(mapDropdown.value); });
        mapDropdown.onValueChanged.AddListener(delegate { ListenForMapChange(mapDropdown.value); });
    }


    private void LoadScene(int _index)
    {
        string mapName = mapDropdown.options[_index].text;
        MapConfig config = GetMapConfigByName(mapName);

        SetDataManager();

        SceneManager.LoadScene(config.sceneName, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }


    private void ListenForMapChange(int _index)
    {
        string mapName = mapDropdown.options[_index].text;

        UpdateMapPanelInfo(GetMapConfigByName(mapName));
    }


    private MapConfig GetMapConfigByName(string _mapName)
    {
        MapConfig config = null;

        for (int i = 0; i < sortedMaps.Count; i++)
        {
            if (sortedMaps[i].mapName == (_mapName))
            {
                config = sortedMaps[i];
                break;
            }
        }
        return config;
    }


    private void SetPlayerPanelColors()
    {
        for (int i = 0; i < userPanels.Length; i++)
        {
            userPanels[i].colorDropdown.AddOptions(colorPallet);
        }
    }


    public void UpdatePlayerPanels(int _minPlayers, int _maxPlayers)
    {
        for (int i = 0; i < userPanels.Length; i++)
        {
            if (i < _maxPlayers)
            {
                userPanels[i].gameObject.SetActive(true);
                userPanels[i].userIndexLabel.text = (i + 1).ToString();

                if (i == 0) userPanels[i].ActivatePlayer(UserType.Player, i);

                if (i > 0 && i < _minPlayers) userPanels[i].ActivatePlayer(UserType.AI, i);
            }
            else
            {
                userPanels[i].ResetPanel();
                userPanels[i].gameObject.SetActive(false);
            }
        }
    }


    public void UpdateMapPanelInfo(MapConfig _config)
    {
        if (_config.picture != null) mapPicture.sprite = _config.picture;

        mapName.text = _config.mapName;
        mapDiscription.text = _config.mapDiscription;

        if (_config.minPlayers == _config.maxPlayers)
        {
            mapSettings.text = PREFIX + _config.maxPlayers;
        }
        else
        {
            mapSettings.text = PREFIX + _config.minPlayers + BETWEEN + _config.maxPlayers;
        }         
        lobbyController.SetMapConfig(_config);
    }


    public void UpdateMapPanelList(ref List<MapConfig> _maps)
    {
        sortedMaps = _maps;

        mapNames.Clear();
        mapDropdown.ClearOptions();

        for (int i = 0; i < sortedMaps.Count; i++)
        {
            mapNames.Add(sortedMaps[i].mapName);
        }

        mapDropdown.AddOptions(mapNames);

        UpdateMapPanelInfo(sortedMaps[0]);

        lobbyController.SetMapConfig(sortedMaps[0]);
    }


    private void SetDataManager()
    {
        List<UserObject> userObjects = new List<UserObject>();

        for (int i = 0; i < userPanels.Length; i++)
        {
            if (userPanels[i].userType != UserType.None)
            {
                UserPanel panel = userPanels[i];
                userObjects.Add(new UserObject(panel.userIndex, panel.userType, panel.userMaterialColor));
            }
        }
        DataManager.Instance.SetUsers(userObjects);
    }
}
