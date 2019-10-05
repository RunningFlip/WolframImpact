using System;


[Serializable]
public class StateElement
{  
    public StateType stateType;
    public bool enabled;


    public StateElement(StateType _stateType, bool _enabled)
    {
        stateType = _stateType;
        enabled = _enabled;
    }
}
