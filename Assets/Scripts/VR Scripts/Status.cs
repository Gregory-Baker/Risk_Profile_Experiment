using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Status : MonoBehaviour
{
    public float communicationDelay = 0f;
    public bool directControl;

    public bool changeControlPermitted;

    public enum EnvironmentOrientation { A, B, C, D}
    public EnvironmentOrientation environment;

    public GameObject gates;

    void Start()
    {

        if (environment == EnvironmentOrientation.B)
        {
            gates.transform.Rotate(Vector3.up, 90f);
        }
        else if (environment == EnvironmentOrientation.C)
        {
            gates.transform.localScale = new Vector3(1, 1, -1);
        }
        else if (environment == EnvironmentOrientation.D)
        {
            gates.transform.localScale = new Vector3(1, 1, -1);
            gates.transform.Rotate(Vector3.up, 180f);
        }

        gates.isStatic = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
