using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    [Header("General Buttons")]
    public Button lobbyButton;
    public Button settingsButton;
    public Button quitButton;


	private void Awake ()
    {
        lobbyButton.onClick.AddListener(delegate { LoadLobby(); });
        settingsButton.onClick.AddListener(delegate { OpenSettings(); });
        quitButton.onClick.AddListener(delegate { QuitGame(); });
    }


    private void LoadLobby()
    {
        LoadSceneWithIndex(1);
    }


    private void OpenSettings()
    {

    }


    private void QuitGame()
    {
        Application.Quit();
    }


    private void LoadSceneWithIndex(int _index)
    {
        SceneManager.LoadScene(_index);
    }

}
