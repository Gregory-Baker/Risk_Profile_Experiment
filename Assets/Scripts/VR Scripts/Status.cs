using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Status : MonoBehaviour
{
    public string participantID;
    public enum EnvironmentOrientation { A, B, C, D }
    public EnvironmentOrientation environment;
    public bool communicationDelayOn = false;
    [HideInInspector] public float communicationDelay = 0f;
    public bool directControl;
    public bool changeControlPermitted;

    [Header("Left/Right Handed")]
    public bool leftHanded = false;
    public Hand rightHand;
    public Hand leftHand;
    public GameObject rightHandModel;
    public GameObject leftHandModel;

    [HideInInspector] public string folderLocalPath;
    [HideInInspector] public string folderGlobalPath;
    [HideInInspector] public string trialName;

    void Awake()
    {
        if (communicationDelayOn)
            communicationDelay = 1f;
        else
            communicationDelay = 0;

        folderLocalPath = String.Format("Assets/ParticipantData/{0}", participantID);
        var folder = Directory.CreateDirectory(folderLocalPath);
        folderGlobalPath = Application.dataPath + "/" + "ParticipantData" + "/" + participantID;

        trialName = MakeTag();

        if (leftHanded)
        {
            rightHand.renderModelPrefab = leftHandModel;
            leftHand.renderModelPrefab = rightHandModel;
        }
    }

    // Update is called once per frame
    private string MakeTag()
    {
        string dcTag;
        string commDelayTag = "";

        if (directControl)
        {
            dcTag = "DC";
        }
        else
        {
            dcTag = "IC";
        }

        if (communicationDelayOn)
        {
            commDelayTag = "D";
        }

        string tag = participantID + "_" + dcTag + commDelayTag + "_" + environment;

        return tag;
    }

    public string FindUniquePathName(string pathNoExt, string fileType)
    {
        string path = pathNoExt + fileType; ;
        int counter = 1;
        if (File.Exists(path))
        {
            do
            {
                var pathNoExtNew = pathNoExt + "_" + counter;
                path = pathNoExtNew + fileType;
                counter += 1;

            } while (File.Exists(path));
        }

        return path;
    }
}
