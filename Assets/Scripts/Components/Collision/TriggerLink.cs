using UnityEngine;


public class TriggerLink : MonoBehaviour
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


    //Trigger Enter
    private void OnTriggerEnter(Collider _collider)
    {
        if (init && _collider != null)
        {
            collisionComponent.lastTriggerObject = _collider.gameObject;
            collisionComponent.trigger = _collider;
        }
    }

    //Trigger Exit
    private void OnTriggerExit(Collider _collider)
    {
        if (init && _collider != null)
        {
            collisionComponent.lastTriggerObject = null;
            collisionComponent.backupTriggerObject = null;
            collisionComponent.trigger = null;
        }
    }
}
