﻿using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StateComponent", menuName = "ScriptableObjects/Components Configs/StateComponent")]
public class StateComponent : EntityComponent
{
    public int collidingState;
    public int movingState;
    public int attackingState;
    public int damageState;
    public int selectionState;

    [HideInInspector]
    public List<StateElement> waitingStates = new List<StateElement>();
}
