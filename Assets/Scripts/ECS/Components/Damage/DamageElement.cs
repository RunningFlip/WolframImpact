using System;


[Serializable]
public class DamageElement
{
    public float damage;
    public DamageType damageType;


    public DamageElement(DamageType _damageType, float _damage)
    {
        damageType = _damageType;
        damage = _damage;
    }
}
