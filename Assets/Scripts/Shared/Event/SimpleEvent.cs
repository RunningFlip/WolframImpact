
/// <summary>
/// Simple event class that will invoke all the listeners if it gets invoked.
/// </summary>
public class SimpleEvent
{
    public delegate void MultiDelegate();
    private MultiDelegate myMultiDelegate;


    /// <summary>
    /// Adds a listener to the event.
    /// </summary>
    /// <param name="_eventCall"></param>
    public void AddListener(MultiDelegate _eventCall)
    {
        myMultiDelegate -= _eventCall; //Prevents the delegate list from having duplicates.
        myMultiDelegate += _eventCall;
    }

    /// <summary>
    /// Removes a listener to the event.
    /// </summary>
    public void RemoveListener(MultiDelegate _eventCall)
    {
        myMultiDelegate -= _eventCall;
    }

    /// <summary>
    /// Invokes all listeners of the event.
    /// </summary>
    public void Invoke()
    {
        if (myMultiDelegate == null) return;
        myMultiDelegate();
    }
}
