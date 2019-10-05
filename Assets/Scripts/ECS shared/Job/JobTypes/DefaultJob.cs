
/// <summary>
/// A job that gets executed every frame, until it got canceled.
/// </summary>
public class DefaultJob : Job
{
    public delegate void MultiDelegate();
    private MultiDelegate myMultiDelegate;


    /// <summary>
    /// Creates a job that gets executed every frame, until it got canceled.
    /// </summary>
    /// <param name="_jobCall">Functionality that will be executed.</param>
    /// <param name="_jobUpdateType">JobUpdateType of the job.</param>
    /// <param name="_destroyOnLoad">If true, the job will be destroyed if the scene changed.</param>
    public DefaultJob(MultiDelegate _jobCall, bool _destroyOnLoad = true) : base(_destroyOnLoad)
    {     
        myMultiDelegate = _jobCall;
        ResetJob();
    }


    public override void ResetJob()
    {
        base.ResetJob();
    }


    public override void UpdateJob()
    {
        if (myMultiDelegate != null) myMultiDelegate();
    }

}
