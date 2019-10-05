using UnityEngine;


public class CollisionLink : MonoBehaviour
{
    private bool init;
    private CollisionComponent collisionComponent;


    /// <summary>
    /// Initializes the collision link.
    /// </summary>
    /// <param name="_collisionComponent"></param>
    public void Initialize(CollisionComponent _collisionComponent)
    {
        init = true;
        collisionComponent = _collisionComponent;
    }


    //Collision Enter
    private void OnCollisionEnter(Collision _collision)
    {
        if (init && _collision != null)
        {
            collisionComponent.lastColisionlObject = _collision.gameObject;
            collisionComponent.collision = _collision;
        }
    }


    //Collision Exit
    private void OnCollisionExit(Collision _collision)
    {
        if (init && _collision != null)
        {
            collisionComponent.lastColisionlObject = null;
            collisionComponent.backupCollisionObject = null;
            collisionComponent.collision = null;
        }
    }
}
