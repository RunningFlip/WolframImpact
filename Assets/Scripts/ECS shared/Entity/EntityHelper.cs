using UnityEngine;


public static class EntityHelper
{
    /// <summary>
    /// Returns the gameobject of the entity holder that is attached to the gameobject hirachie.
    /// </summary>
    /// <param name="_object"></param>
    /// <returns></returns>
    public static GameObject GetEntityObject(GameObject _object)
    {
        GameObject entityObject = null;
        GameObjectEntityHolder entityHolder = _object.GetComponent<GameObjectEntityHolder>();

        if (entityHolder == null)
        {
            EntityHolderLink link = _object.GetComponent<EntityHolderLink>();
            entityObject = link.entityHolder.gameObject;
        }
        else
        {
            entityObject = entityHolder.gameObject;
        }

        return entityObject;
    }
}
