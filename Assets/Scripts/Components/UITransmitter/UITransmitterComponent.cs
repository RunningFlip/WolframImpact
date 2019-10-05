using UnityEngine;
using System;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "UITransmitterComponent", menuName = "ScriptableObjects/Components Configs/UITransmitterComponent")]
public class UITransmitterComponent : EntityComponent
{
    public bool multipleInfos;


    //Lists
    [NonSerialized] public List<InformationPackage> sortedSelections = new List<InformationPackage>();
}
