using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SecondaryTaskHandler : MonoBehaviour
{
    public int numArrowTypes = 2;
    [SerializeField] SpriteRenderer[] rightArrows;
    [SerializeField] SpriteRenderer[] leftArrows;
    public int errors = 0;
    public SteamVR_Action_Boolean a_leftArrow;
    public SteamVR_Action_Boolean a_rightArrow;
    public Hand hand;
    public SpriteRenderer activeArrow = null;
    public float timeDelay = 10f;
    public Timer timer;
    [SerializeField] bool secondaryTaskActive = false;
    int arrowLR; //  0 for L arrow, 1 for R arrow
    float startTime;
    float responseTime;
    public Status status;
    string folder;
    string logFile;
    public bool startSecondaryTask = false;


    // Start is called before the first frame update
    void Start()
    {
        folder = status.folderLocalPath;
        string filename = status.trialName + "_ResponseTimes";
        string fileType = ".txt";
        string path = folder + "/" + filename + fileType;

        logFile = status.FindUniquePathName(folder + "/" + filename, fileType);


        AssignVRInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        if ((!secondaryTaskActive && timer.timerOn) || startSecondaryTask)
        {
            secondaryTaskActive = true;
            StartCoroutine(ArrowActivation(timeDelay));
            startSecondaryTask = false;
        }
    }

    private void AssignVRInputActions()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();

        if (a_leftArrow == null)
        {
            Debug.LogError("No left action assigned");
            return;
        }

        a_leftArrow.AddOnChangeListener(OnLeftArrowActionChange, hand.handType);
        a_rightArrow.AddOnChangeListener(OnRightArrowActionChange, hand.handType);
    }


    IEnumerator ArrowActivation(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        if (!timer.trialFinished)
        {
            ActivateArrow();
        }
    }

    private void ActivateArrow()
    {
        arrowLR = UnityEngine.Random.Range(0, numArrowTypes);
        startTime = Time.time;
        if (arrowLR % 2 == 0)
        {
            int arrowIndex = UnityEngine.Random.Range(0, 2);
            leftArrows[arrowIndex].enabled = true;
            activeArrow = leftArrows[arrowIndex];
        }
        else
        {
            int arrowIndex = UnityEngine.Random.Range(0, 2);
            rightArrows[arrowIndex].enabled = true;
            activeArrow = rightArrows[arrowIndex];
        }
    }

    private void OnLeftArrowActionChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        if (activeArrow != null)
        {
            if (arrowLR == 0)
            {
                ArrowDeactivate();
            }
            else
            {
                errors += Convert.ToInt32(newState);
            }
        }
    }

    private void OnRightArrowActionChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        if (activeArrow != null)
        {
            if (arrowLR == 1)
            {
                ArrowDeactivate();
            }
            else
            {
                errors += Convert.ToInt32(newState);
            }  
        }
    }

    private void ArrowDeactivate()
    {
        responseTime = Time.time - startTime;
        if (!status.tutorial)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(logFile, true))
            {
                file.WriteLine(responseTime);
            }
        }

        activeArrow.enabled = false;
        activeArrow = null;
        StartCoroutine(ArrowActivation(timeDelay));
    }

    private void OnDisable()
    {
        a_leftArrow.RemoveOnChangeListener(OnLeftArrowActionChange, hand.handType);
        a_rightArrow.RemoveOnChangeListener(OnRightArrowActionChange, hand.handType);
    }

    private void OnApplicationQuit()
    {
        if (!status.tutorial)
        {
            LogErrors();
        }
    }

    private void LogErrors()
    {
        string path = status.trialDataFile;
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
        {
            file.WriteLine("Task Errors, " + errors);
        }
    }
}
