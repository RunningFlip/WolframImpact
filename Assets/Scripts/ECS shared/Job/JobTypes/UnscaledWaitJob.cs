using UnityEngine;

/// <summary>
/// A job that will wait a specific unscaled realtime, before it will be executed.
/// </summary>
public class UnscaledWaitJob : Job
{
    public delegate void MultiDelegate();
    private MultiDelegate myMultiDelegate;


    private float timeToWait;
    private float startTime;


    /// <summary>
    /// Creates a UnscaledWaitJob that will be executed after a specific unscaled amount of realtime.
    /// </summary>
    /// <param name="_jobCall">Functionality that will be executed.</param>
    /// <param name="_timeToWait">Time that has to be waited, before executing the job.</param>
    /// <param name="_jobUpdateType">JobUpdateType of the job.</param>
    /// <param name="_destroyOnLoad">If true, the job will be destroyed if the scene changed.</param>
    public UnscaledWaitJob(MultiDelegate _jobCall, float _timeToWait, bool _destroyOnLoad = true) : base(_destroyOnLoad)
    {
        myMultiDelegate = _jobCall;
        timeToWait = _timeToWait;

        ResetJob();
    }


    public override void ResetJob()
    {
        startTime = Time.unscaledTime;
        base.ResetJob();
    }


    public override void UpdateJob()
    {
        if (myMultiDelegate != null)
        {
            if (startTime + timeToWait <= Time.unscaledTime)
            {
                myMultiDelegate();
                CancelJob();
            }
        }
    }
}
