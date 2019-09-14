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
    public float communicationDelay = 0f;
    public bool directControl;
    public bool changeControlPermitted;
    public bool tutorial;

    [Header("Left/Right Handed")]
    public bool leftHanded = false;
    public Hand rightHand;
    public Hand leftHand;
    public GameObject rightHandModel;
    public GameObject leftHandModel;

    [HideInInspector] public string folderLocalPath;
    [HideInInspector] public string trialName;
    [HideInInspector] public string trialDataFile;

    void Awake()
    {
        if (communicationDelayOn)
            communicationDelay = 1f;
        else
            communicationDelay = 0;

        trialName = MakeTag();

        folderLocalPath = FindUniqueDirectoryName(String.Format("Assets/ParticipantData/{0}/{1}", participantID, trialName));

        if (!tutorial)
        {
            var folder = Directory.CreateDirectory(folderLocalPath);

            trialDataFile = folderLocalPath + "/" + trialName + "_DataFile";
            trialDataFile = FindUniquePathName(trialDataFile, ".txt");

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(trialDataFile, true))
            {
                string env = "Environment, " + environment;
                file.WriteLine(env);
            }
        }

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

        string tag = participantID + "_" + dcTag + commDelayTag;

        return tag;
    }

    public string FindUniquePathName(string pathNoExt, string fileType)
    {
        string path = pathNoExt + fileType;
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

    public string FindUniqueDirectoryName(string path)
    {
        int counter = 1;
        string pathNew = path;
        if (Directory.Exists(path))
        {
            do
            {
                pathNew = path + "_" + counter;
                counter += 1;

            } while (Directory.Exists(pathNew));
        }

        return pathNew;
    }
}
