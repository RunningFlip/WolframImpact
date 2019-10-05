using UnityEngine;
using System.Collections.Generic;


public class DataManager : MonoBehaviour
{
    public static DataManager Instance;


    public List<UserObject> userObjects = new List<UserObject>();


    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }


    public List<UserObject> GetUsers()
    {
        return userObjects;
    }


    public void SetUsers(List<UserObject> _userObjects)
    {
        userObjects = _userObjects;
    }

}
