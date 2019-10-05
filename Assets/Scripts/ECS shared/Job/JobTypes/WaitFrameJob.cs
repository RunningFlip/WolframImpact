
/// <summary>
/// A job that will wait a specific amount of frames, before it will be executed.
/// </summary>
public class WaitFrameJob : Job
{
    public delegate void MultiDelegate();
    private MultiDelegate myMultiDelegate;

    private int framesToWait;
    private int framesLeft;


    /// <summary>
    /// Creates a WaitFrameJob that will be executed after a specific amount of time.
    /// </summary>
    /// <param name="_jobCall">Functionality that will be executed.</param>
    /// <param name="_frames">Frames that has to be awaited, before executing the job.</param>
    /// <param name="_jobUpdateType">JobUpdateType of the job.</param>
    /// <param name="_destroyOnLoad">If true, the job will be destroyed if the scene changed.</param>
    public WaitFrameJob(MultiDelegate _jobCall, int _frames = 1, bool _destroyOnLoad = true) : base(_destroyOnLoad)
    {
        framesToWait = _frames + 1;
        myMultiDelegate = _jobCall;

        ResetJob();
    }


    public override void ResetJob()
    {
        framesLeft = framesToWait;
        base.ResetJob();
    }


    public override void UpdateJob()
    {
        if (myMultiDelegate != null)
        {
            framesLeft--;

            if (framesLeft == 0)
            {
                myMultiDelegate();
                CancelJob();
            }
        }
    }

}
