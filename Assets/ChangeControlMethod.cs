using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;

public class ChangeControlMethod : MonoBehaviour
{
    public Status status;

    public Hand hand;
    public SteamVR_Action_Boolean changeControlAction;

    public NavMeshAgent navMeshAgent;
    public SampleAgentScript sampleAgentScript;
    //public Teleport teleportScript;
    public GameObject teleportObject;
    public StopRobot stopRobot;
    public LineRenderer lineRenderer;
    public MeshRenderer dcUiText;
    public MeshRenderer icUiText;

    public DirectControl directControlScript;

    public SteamVR_ActionSet dcActionSet;
    public SteamVR_ActionSet icActionSet;
    public SteamVR_Input_Sources forSources = SteamVR_Input_Sources.Any;

    public GameObject[] teleportTargetDisks;

    private void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();

        if (changeControlAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No action assigned");
            return;
        }

        changeControlAction.AddOnChangeListener(OnConfirmActionChange, hand.handType);

        SwitchControl();

    }


    private void OnConfirmActionChange(SteamVR_Action_Boolean changeControlAction, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue && status.changeControlPermitted)
        {
            status.directControl = !status.directControl;
            SwitchControl();
        }
    }

    public void SwitchControl()
    {
        if (status.directControl)
        {
            print("Direct Control Activated");
            dcActionSet.Activate(forSources, 1, false);
            directControlScript.enabled = true;
            navMeshAgent.enabled = false;
            sampleAgentScript.enabled = false;
            teleportObject.SetActive(false);
            stopRobot.enabled = false;
            lineRenderer.enabled = false;
            foreach (GameObject targetDisk in teleportTargetDisks)
            {
                targetDisk.SetActive(false);
            }
            if (status.changeControlPermitted)
            {
                icUiText.enabled = false;
                dcUiText.enabled = true;
            }
        }
        else
        {
            print("Indirect Control Activated");
            dcActionSet.Deactivate(forSources);
            directControlScript.enabled = false;
            navMeshAgent.enabled = true;
            sampleAgentScript.enabled = true;
            teleportObject.SetActive(true);
            stopRobot.enabled = true;
            lineRenderer.enabled = true;
            foreach (GameObject targetDisk in teleportTargetDisks)
            {
                targetDisk.SetActive(true);
                targetDisk.transform.position = transform.position;
            }
            if (status.changeControlPermitted)
            {
                icUiText.enabled = true;
                dcUiText.enabled = false;
            }
        }
    }
}

