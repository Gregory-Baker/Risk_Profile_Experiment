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

    [Header("Left/Right Handed")]
    public bool leftHanded = false;
    public Hand rightHand;
    public Hand leftHand;
    public GameObject rightHandModel;
    public GameObject leftHandModel;

    void Start()
    {
        if (leftHanded)
        {
            rightHand.renderModelPrefab = leftHandModel;
            leftHand.renderModelPrefab = rightHandModel;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
