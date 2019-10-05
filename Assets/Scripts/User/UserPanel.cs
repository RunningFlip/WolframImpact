using System;
using UnityEngine;
using UnityEngine.UI;


public class UserPanel : MonoBehaviour
{
    [Header("User")]
    public int userIndex = -1;
    public UserType userType = UserType.None;
    public Material userMaterialColor;

    [Header("Panel Elements")]
    public Text userIndexLabel;
    public Text userNameLabel;
    [Space]
    public Dropdown statusDropdown;
    public Dropdown colorDropdown;

    
    private string UNKOWN_PLAYER = "Unkown user";
    private string PLAYER_NAME = "Player";
    private string AI_NAME = "A.I.";


    private void Awake()
    {
        statusDropdown.onValueChanged.AddListener(delegate { ListenToStatusDropdown(); ListenToColorDropdown(); });
        colorDropdown.onValueChanged.AddListener(delegate { ListenToColorDropdown(); });
    }


    private void ListenToColorDropdown()
    {
        userMaterialColor = LobbyController.Instance.LobbyParameters.userColors[colorDropdown.value].material;
    }


    private void ListenToStatusDropdown()
    {
        switch (statusDropdown.value)
        {
            case 0:
                ResetPanel();
                break;
            case 1:
                userIndex = Convert.ToInt32(userIndexLabel.text) - 1;
                ActivatePlayer(UserType.Player, userIndex);
                break;
            case 2:
                userIndex = Convert.ToInt32(userIndexLabel.text) - 1;
                ActivatePlayer(UserType.AI, userIndex);
                break;
        }
    }


    public void ActivatePlayer(UserType _playerType, int _colorIndex)
    {
        userNameLabel.gameObject.SetActive(true);
        colorDropdown.gameObject.SetActive(true);

        userType = _playerType;

        switch (userType)
        {
            case UserType.Player:
                userNameLabel.text = PLAYER_NAME + " " + userIndex;
                statusDropdown.value = 1;
                break;

            case UserType.AI:
                userNameLabel.text = AI_NAME + " " + userIndex;
                statusDropdown.value = 2;
                break;
        }
        colorDropdown.value = _colorIndex;
    }


    public void ResetPanel()
    {
        userIndex = -1;
        userType = UserType.None;
        userMaterialColor = null;

        userNameLabel.text = UNKOWN_PLAYER;

        statusDropdown.value = 0;
        colorDropdown.value = 0;

        userNameLabel.gameObject.SetActive(false);
        colorDropdown.gameObject.SetActive(false);
    }

}
