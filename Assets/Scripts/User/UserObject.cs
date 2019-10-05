using System;
using UnityEngine;

[Serializable]
public class UserObject
{
    public int userIndex;
    public UserType userType;
    public Material factionMaterial;
    

    public UserObject(int _userIndex, UserType _userType, Material _factionMaterial)
    {
        userIndex = _userIndex;  
        userType = _userType;
        factionMaterial = _factionMaterial;
    }
}
