using UnityEngine;

/// <summary>
/// A job that will wait a specific time, before it will be executed.
/// </summary>
public class WaitJob : Job
{
    public delegate void MultiDelegate();
    private MultiDelegate myMultiDelegate;


    private float timeToWait;
    private float startTime;

    /// <summary>
    /// Creates a WaitJob that will be executed after a specific amount of time.
    /// </summary>
    /// <param name="_jobCall">Functionality that will be executed.</param>
    /// <param name="_timeToWait">Time that has to be waited, before executing the job.</param>
    /// <param name="_jobUpdateType">JobUpdateType of the job.</param>
    /// <param name="_destroyOnLoad">If true, the job will be destroyed if the scene changed.</param>
    public WaitJob(MultiDelegate _jobCall, float _timeToWait, bool _destroyOnLoad = true) : base(_destroyOnLoad)
    {
        myMultiDelegate = _jobCall;
        timeToWait = _timeToWait;

        ResetJob();
    }


    public override void ResetJob()
    {
        startTime = Time.time;
        base.ResetJob();
    }


    public override void UpdateJob()
    {
        if (myMultiDelegate != null)
        {
            if (startTime + timeToWait <= Time.time)
            {
                myMultiDelegate();
                CancelJob();
            }
        }
    }
}
