using System;


/// <summary>
/// The Job base class.
/// </summary>
public abstract class Job
{
    /// <summary>
    /// If true, the job is activated and will be updated from the jobsystem.
    /// </summary>
    [NonSerialized]
    private bool isActive;

    /// <summary>
    /// If true, the job gets destroied by changing the scene.
    /// </summary>
    [NonSerialized]
    public bool destroyOnLoad;


    /// <summary>
    /// Creates a job.
    /// </summary>
    /// <param name="_jobUpdateType">JobUpdateType of the job.</param>
    /// <param name="_destroyOnLoad">If true, the job will be destroyed if the scene changed.</param>
    public Job(bool _destroyOnLoad = true)
    {
        destroyOnLoad = _destroyOnLoad;
    }


    public abstract void UpdateJob();


    /// <summary>
    /// Resets the job and it's variables and starts it.
    /// </summary>
    public virtual void ResetJob()
    {
        if (isActive) CancelJob(); 

        isActive = true;
        JobSystem.RegisterJob(this);
    }

    /// <summary>
    /// Cancels the job.
    /// </summary>
    public virtual void CancelJob()
    {
        isActive = false;
        JobSystem.UnregisterJob(this);
    }
}
