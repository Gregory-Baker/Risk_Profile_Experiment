﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCameraPosition : MonoBehaviour
{

    public Camera vrCamera;
    public GameObject robot;

    public float verticalHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 camPos = robot.transform.position - vrCamera.transform.position;
        vrCamera.transform.Translate(camPos);
    }
}
