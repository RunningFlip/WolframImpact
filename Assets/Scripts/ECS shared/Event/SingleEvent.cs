
/// <summary>
/// An event that always has only one listener.
/// </summary>
public class SingleEvent
{
    public delegate void MultiDelegate();
    private MultiDelegate myMultiDelegate;


    /// <summary>
    /// Adds a single listener to the event.
    /// </summary>
    /// <param name="_eventCall"></param>
    public void AddListener(MultiDelegate _eventCall)
    {
        myMultiDelegate = null;
        myMultiDelegate += _eventCall;
    }

    /// <summary>
    /// Removes the listener from the event.
    /// </summary>
    public void RemoveListener(MultiDelegate _eventCall)
    {
        myMultiDelegate = null;
    }

    /// <summary>
    /// Invokes the listener of the event.
    /// </summary>
    public void Invoke()
    {
        if (myMultiDelegate == null) return;
        myMultiDelegate();
    }
}
