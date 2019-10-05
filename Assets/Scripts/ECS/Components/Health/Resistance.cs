using UnityEngine;
using System;


[Serializable]
public class Resistance
{
    public DamageType damageType;
    public int resistanceValue;


    public Resistance(DamageType _damageType, int _resistanceValue)
    {
        damageType = _damageType;
        resistanceValue = Mathf.Clamp(_resistanceValue, 0, 100);
    }
}
