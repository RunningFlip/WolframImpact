using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public static GameController Instance;


    [Header("Current users")]
    public List<int> userEntityIds = new List<int>();
    private Dictionary<int, int> userTable = new Dictionary<int, int>();
    public int userCount = 0;
    public int deadUsers = 0;

    [Header("User Materials")]
    public Dictionary<int, Material> userMaterials = new Dictionary<int, Material>();

    [Header("Game parameter")]
    public GameParameter GameParameter;

    [Header("UI")]
    public UIController uiController;

    [Header("Components")]
    public Map map;


    //Time
    private float lastTimeScale = -1;

	
	private void Awake ()
    {
		if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Map
        map = FindObjectOfType<Map>();

        //Game loop - routine
        StartCoroutine("GameLoopRoutine");
    }


    /// <summary>
    /// Starts the game loop
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameLoopRoutine()
    {
        yield return StartCoroutine("InitUsers");
        yield return StartCoroutine("SpawnBase");
        yield return StartCoroutine("PlayGame");
        yield return StartCoroutine("EndGame");
    }


    private IEnumerator InitUsers()
    {
        List<UserObject> userObjects = DataManager.Instance.GetUsers();

        for (int i = 0; i < userObjects.Count; i++)
        {
            yield return AddUser(userObjects[i]);
        }
        Debug.Log("Users initialized.");
    }


    private IEnumerator SpawnBase()
    {
        for (int i = 0; i < userEntityIds.Count; i++)
        {
            int entityID = userEntityIds[i];

            UserComponent user = ComponentManager.GetComponent<UserComponent>(entityID);
            ResourceComponent resource = ComponentManager.GetComponent<ResourceComponent>(entityID);

            yield return StartCoroutine(UserHelper.SpawnBase(map, user, resource, user.userObject.userIndex));
        }
        yield return null;
        Debug.Log("Base spawned.");
    }


    private IEnumerator PlayGame()
    {
        ReadInput(true);
        Debug.Log("Game has started!");
        yield return new WaitUntil(() => deadUsers == userCount - 1);
    }


    private IEnumerator EndGame()
    {
        ReadInput(false);
        Debug.Log("Game has ended!");
        yield return null;
    }


    /// <summary>
    /// Adds a user to the game
    /// </summary>
    public IEnumerator AddUser(UserObject _userObject)
    {
        //Instantiate
        GameObject userControllerObject = null;

        switch (_userObject.userType)
        {
            case UserType.AI:
                userControllerObject = Instantiate(GameParameter.userControllerPrefab_AI);
                break;

            case UserType.Player:
                userControllerObject = Instantiate(GameParameter.userControllerPrefab_Player);
                break;
        }
        yield return null; //Waits a frame

        //ID
        GameObjectEntityHolder entityHolder = userControllerObject.GetComponent<GameObjectEntityHolder>();
        userEntityIds.Add(entityHolder.GetEntityID());
        userTable.Add(_userObject.userIndex, entityHolder.GetEntityID());

        //UserComponent
        UserComponent userComponent = ComponentManager.GetComponent<UserComponent>(entityHolder.GetEntityID());
        userComponent.userObject = _userObject;

        //User Materials
        userMaterials.Add(_userObject.userIndex, _userObject.factionMaterial);

        //Couting
        userCount++;
    }


    /// <summary>
    /// Kill a user.
    /// </summary>
    public void KillUser()
    {
        deadUsers++;
    }


    /// <summary>
    /// Returns the entityId that is linked to the user entity of the given userIndex.
    /// </summary>
    /// <param name="_userIndex"></param>
    /// <returns></returns>
    public int GetUserEntitId(int _userIndex)
    {
        return userTable[_userIndex];
    }


    /// <summary>
    /// Enables or disables time.
    /// </summary>
    /// <param name="_status"></param>
    public void PauseGame(bool _status)
    {
        if (_status && lastTimeScale == -1)
        {
            lastTimeScale = Time.timeScale;
            Time.timeScale = 0;

        }
        else if (!_status && lastTimeScale != -1)
        {

            Time.timeScale = lastTimeScale;
            lastTimeScale = -1;
        }
    }


    /// <summary>
    /// Disables the inputcomponent of the players and the functionality of the AI's.
    /// </summary>
    private void ReadInput(bool _read)
    {
        List<Entity> userEntities = EntityManager.Entities.ForEach(typeof(UserComponent), typeof(InputComponent));

        for (int i = 0; i < userEntities.Count; i++)
        {
            int entityID = userEntities[i].GetEntityID();

            InputComponent inputComponent = ComponentManager.GetComponent<InputComponent>(entityID);
            inputComponent.readInput = _read;
        }
    }

}
