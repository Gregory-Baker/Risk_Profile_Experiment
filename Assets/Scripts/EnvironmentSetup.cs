using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnvironmentSetup : MonoBehaviour
{

    //public enum EnvironmentOrientation { A, B, C, D }
    public Status robotStatus;
    Status.EnvironmentOrientation environment;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        environment = robotStatus.environment;
        if (environment == Status.EnvironmentOrientation.A)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (environment == Status.EnvironmentOrientation.B)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (environment == Status.EnvironmentOrientation.C)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(1, 1, -1);
        }
        else if (environment == Status.EnvironmentOrientation.D)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.localScale = new Vector3(1, 1, -1);
        }
    }
}
