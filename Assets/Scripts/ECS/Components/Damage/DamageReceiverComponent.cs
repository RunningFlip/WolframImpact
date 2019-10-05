using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DamageReceiverComponent", menuName = "ScriptableObjects/Components Configs/DamageReceiverComponent")]
public class DamageReceiverComponent : EntityComponent
{
    public bool receivable;
    public List<DamageElement> damageElements = new List<DamageElement>();


    public void Setup(bool _receivable)
    {
        receivable = _receivable;
    }
}


