using System;
using System.Collections.Generic;


[Serializable]
public struct SelectionSetArray
{
    public List<int>[] sets;


    public SelectionSetArray(int _size)
    {
        sets = new List<int>[_size];

        for (int i = 0; i < sets.Length; i++)
        {
            sets[i] = new List<int>();
        }
    }


    public static bool IsIndexUsed(SelectionSetArray _array, int _arrayIndex)
    {
        if (_array.sets[_arrayIndex].Count > 0) return true;
        return false;
    }


    public static void AddElementToSet(SelectionSetArray _array, int _arrayIndex, int _entityId)
    {
        if (_array.sets.Length > _arrayIndex)
        {
            _array.sets[_arrayIndex].Add(_entityId);
            HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(_entityId);
            healthComponent.onDeath.AddListener(delegate { RemoveElementFromSet(_array, _arrayIndex, _entityId); });
        }
    }


    public static void RemoveElementFromSet(SelectionSetArray _array, int _arrayIndex, int _entityId)
    {
        if (_array.sets.Length > _arrayIndex)
        {
            HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(_entityId);
            healthComponent.onDeath.RemoveListener(delegate { RemoveElementFromSet(_array, _arrayIndex, _entityId); });
            _array.sets[_arrayIndex].Remove(_entityId);
        }
    }


    public static void ClearSet(SelectionSetArray _array, int _arrayIndex)
    {
        if (_array.sets.Length > _arrayIndex)
        {
            for (int i = 0; i < _array.sets[_arrayIndex].Count; i++)
            {
                int id = _array.sets[_arrayIndex][i];
                HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(id);
                healthComponent.onDeath.RemoveListener(delegate { RemoveElementFromSet(_array, _arrayIndex, id); });
            }
            _array.sets[_arrayIndex].Clear();
        }
    }


    public static void UpdateSet(SelectionSetArray _array, int _setIndex)
    {
        if (_array.sets.Length > _setIndex)
        {
            for (int i = _array.sets[_setIndex].Count - 1; i > -1; i--)
            {
                int id = _array.sets[_setIndex][i];

                bool entityExists = EntityManager.EntityExists(id);
                HealthComponent healthComponent = ComponentManager.GetComponent<HealthComponent>(id);

                if (!entityExists || healthComponent.isDead)
                {
                    _array.sets[_setIndex].Remove(id);
                }
            }
        }
    }

}
