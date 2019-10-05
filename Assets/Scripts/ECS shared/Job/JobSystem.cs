using System.Collections.Generic;
using UnityEngine.SceneManagement;


/// <summary>
/// The system that handles all job actions.
/// </summary>
public class JobSystem
{
    //Updateable object hashSet   
    private static List<Job> updateJobList = new List<Job>();
    private static List<Job> jobsToAdd = new List<Job>();


    public JobSystem()
    {
        //Callback for scene changes
        SceneManager.activeSceneChanged += CheckDestroyOnLoadJobs;
    }


    /// <summary>
    /// Will be exeuted every frame.
    /// </summary>
    public void OnUpadate()
    {
        CheckNewJobs(); //Checks if there are new jobs to add.

        for (int i = 0; i < updateJobList.Count; i++)
        {
            updateJobList[i].UpdateJob();
        }
    }


    /// <summary>
    /// Registers the given object to the updateTable.
    /// </summary>
    /// <param name="_job"></param>
    public static void RegisterJob(Job _job)
    {
        if (_job == null) throw new System.ArgumentNullException();
        jobsToAdd.Add(_job);
    }


    /// <summary>
    /// Unregisters the given object from the updateTable.
    /// </summary>
    /// <param name="_job"></param>
    public static void UnregisterJob(Job _job)
    {
        if (_job == null) throw new System.ArgumentNullException();
        updateJobList.Remove(_job);
    }


    /// <summary>
    /// Adds the jobs from the "jobsToAdd"-List to the "jobList", where they will be executed.
    /// </summary>
    private void CheckNewJobs()
    {
        if (jobsToAdd.Count > 0)
        {
            var item = jobsToAdd.GetEnumerator(); //avoid gc by calling GetEnumerator and iterating manually
            while (item.MoveNext())
            {
                updateJobList.Add(item.Current);
            }
            jobsToAdd.Clear();
        }
    }


    /// <summary>
    /// Cancels every job that is tagged with "destroyOnLoad".
    /// </summary>
    /// <param name="_arg0"></param>
    /// <param name="_arg1"></param>
    private void CheckDestroyOnLoadJobs(Scene _arg0, Scene _arg1) {
        for (int i = 0; i < updateJobList.Count; i++)                      
        {
            if (updateJobList[i].destroyOnLoad) updateJobList[i].CancelJob();
        }
    }
}
