using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;

public class ChangeControlMethod : MonoBehaviour
{
    public Status robotStatus;

    public Hand hand;
    public SteamVR_Action_Boolean changeControlAction;

    public SampleAgentScript navMeshAgent;
    //public Teleport teleportScript;
    public GameObject teleportObject;
    public StopRobot stopRobot;
    public LineRenderer lineRenderer;

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
        if (newValue)
        {
            robotStatus.directControl = !robotStatus.directControl;
            SwitchControl();
        }
    }

    public void SwitchControl()
    {
        if (robotStatus.directControl)
        {
            print("Direct Control Activated");
            dcActionSet.Activate(forSources, 0, false);
            directControlScript.enabled = true;
            navMeshAgent.enabled = false;
            teleportObject.SetActive(false);
            stopRobot.enabled = false;
            lineRenderer.enabled = false;
            foreach (GameObject targetDisk in teleportTargetDisks)
            {
                targetDisk.SetActive(false);
            }
        }
        else
        {
            print("Indirect Control Activated");
            dcActionSet.Deactivate(forSources);
            //icActionSet.Activate(forSources, 0, true);
            directControlScript.enabled = false;
            navMeshAgent.enabled = true;
            teleportObject.SetActive(true);
            stopRobot.enabled = true;
            lineRenderer.enabled = true;
            foreach (GameObject targetDisk in teleportTargetDisks)
            {
                targetDisk.SetActive(true);
                targetDisk.transform.position = transform.position;
            }
        }
    }
}

