using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    public Status status;

    public CollisionAvoidance collisionAvoidance;
    public Timer timer;
    public TrackRobotPosition tracker;
    public SecondaryTaskHandler secondaryTaskHandler;

    // Start is called before the first frame update
    void Start()
    {
        if (status == null)
        {
            status = GetComponent<Status>();
        }
    }

    private void OnApplicationQuit()
    {
        if (!status.tutorial)
        {
            string path = status.trialDataFile;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                file.WriteLine("Trial Time, " + timer.timer);
                file.WriteLine("Distance Travelled, " + tracker.distanceTravelled);
                file.WriteLine("Task Errors, " + secondaryTaskHandler.errors);
                file.WriteLine("Collisions, " + collisionAvoidance.collisions);
            }
        }
    }

}
