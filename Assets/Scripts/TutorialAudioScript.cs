using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TutorialAudioScript : MonoBehaviour
{
    public Hand leftHand;
    public Hand rightHand;
    public SteamVR_Action_Boolean nextTutorialAction;
    public SteamVR_Action_Boolean repeatTutorialAction;

    public SteamVR_Action_Boolean movementActionDC;
    public SteamVR_Action_Boolean movementActionIC;
    public SteamVR_Action_Boolean confirmAction;
    public SteamVR_Action_Boolean stopAction;
    public SteamVR_Action_Boolean secondaryTaskAction;

    public SteamVR_Action_Boolean changeControlAction;


    public AudioClip[] dcAudioClips;
    public AudioClip[] icAudioClips;
    public AudioClip[] bonusAudioClips;

    AudioClip[] audioClips;

    public AudioSource audioSource;

    public GameObject robot;
    public Status status;

    public SecondaryTaskHandler secondaryTask;

    public MeshRenderer videoScreen;

    private int counter = -1;
    private bool startedTutorial = false;

    public Transform target;

    private void OnEnable()
    {
        if (leftHand == null)
            leftHand = this.GetComponent<Hand>();

        if (nextTutorialAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No action assigned");
            return;
        }

        nextTutorialAction.AddOnChangeListener(NextTutorialActionChange, leftHand.handType);
        repeatTutorialAction.AddOnChangeListener(RepeatTutorialActionChange, leftHand.handType);

    }

    // Start is called before the first frame update
    void Start()
    {
        secondaryTask.enabled = false;
        if (status.changeControlPermitted)
        {
            audioClips = bonusAudioClips;
        }
        else if (status.directControl)
        {
            audioClips = dcAudioClips;
        }
        else
        {
            audioClips = icAudioClips;
            videoScreen.enabled = true;
        }
        StartCoroutine(PlayTutorialHint(leftHand, nextTutorialAction, "Play Tutorial", true, 5f));
    }



    private void NextTutorialActionChange(SteamVR_Action_Boolean nextTutorialAction, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            if (counter < 4)
            {
                counter += 1;
            }
            ControllerButtonHints.HideAllButtonHints(leftHand);
            ControllerButtonHints.HideAllButtonHints(rightHand);
            ControllerButtonHints.HideAllTextHints(leftHand);
            ControllerButtonHints.HideAllTextHints(rightHand);
            StopAllCoroutines();
            PlayAudio(counter);
            HighlightButton(ref counter);
            ShowTutorialHint(ref counter);
        }
    }

    private void HighlightButton(ref int counter)
    {
        if (counter == 0)
        {
            if (status.changeControlPermitted)
            {
                StartCoroutine(HighlightButton(rightHand, changeControlAction, "Change Control", 10f, 10f));
                counter = 3;
            }
            else if (status.directControl)
            {
                StartCoroutine(HighlightButton(rightHand, movementActionDC, "Direct Control", 0f, 20f));
            }
            else
            {
                StartCoroutine(HighlightButton(rightHand, movementActionIC, "Select Waypoint", 20f, 20f));
                StartCoroutine(HighlightButton(rightHand, confirmAction, "Confirm Waypoint", 40f, 10f));
            }
        }
        else if (counter == 1)
        {
            if (!status.directControl)
            {
                StartCoroutine(HighlightButton(rightHand, stopAction, "Stop Robot"));
            }

        }
        else if (counter == 2)
        {
            StartCoroutine(HighlightButton(leftHand, secondaryTaskAction, "Press Direction of Arrow", 5f, 20f));
            secondaryTask.enabled = true;
            secondaryTask.startSecondaryTask = true;
        }
        else if (counter == 3)
        {
            if (secondaryTask.activeArrow != null)
            {
                secondaryTask.activeArrow.enabled = false;
            }
            ControllerButtonHints.HideTextHint(leftHand, secondaryTaskAction);
            secondaryTask.startSecondaryTask = false;
            secondaryTask.enabled = false;
            status.communicationDelayOn = true;
        }
    }

    private void RepeatTutorialActionChange(SteamVR_Action_Boolean repeatTutorialAction, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            PlayAudio(counter);
        }
    }

    private void ShowTutorialHint(ref int counter)
    {
        if (counter < 3)
        {
            ControllerButtonHints.HideTextHint(leftHand, nextTutorialAction);
            ControllerButtonHints.HideTextHint(leftHand, repeatTutorialAction);
            StartCoroutine(PlayTutorialHint(leftHand, nextTutorialAction, "Next Tutorial", false, audioClips[counter].length));
            StartCoroutine(PlayTutorialHint(leftHand, repeatTutorialAction, "Repeat Tutorial", false, audioClips[counter].length));
        }
        else if (counter == 3)
        {   
            ControllerButtonHints.HideTextHint(leftHand, nextTutorialAction);
            ShowTarget();
        }
        else if (counter == 4)
        {
            ControllerButtonHints.HideTextHint(leftHand, repeatTutorialAction);
        }
    }

    private void ShowTarget()
    {
        target.position = new Vector3(35f, 0.05f, 0f);
    }

    private void PlayAudio(int counter)
    {
        if (audioClips.Length > 0 && counter >= 0 && counter < audioClips.Length)
        {
            audioSource.clip = audioClips[counter];
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.transform.position = robot.transform.position;

    }

    public IEnumerator PlayTutorialHint(Hand hand, SteamVR_Action_Boolean action, string display, bool highlight = false, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        ControllerButtonHints.ShowTextHint(hand, action, display, highlight);
    }

    public IEnumerator HighlightButton(Hand hand, SteamVR_Action_Boolean action, string display, float delay = 0f, float time = 15f)
    {
        yield return new WaitForSeconds(delay);
        ControllerButtonHints.ShowTextHint(hand, action, display, true);
        yield return new WaitForSeconds(time);
        ControllerButtonHints.HideTextHint(hand, action);
    }

}
