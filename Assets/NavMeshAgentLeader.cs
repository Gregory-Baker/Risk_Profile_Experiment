using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class NavMeshAgentLeader : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    NavMeshSurface surface;

    public float distanceAhead = 0.2f;

    public SteamVR_Action_Boolean confirmTargetAction;
    public Hand hand;

    private void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();

        if (confirmTargetAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No action assigned");
            return;
        }

        confirmTargetAction.AddOnChangeListener(OnConfirmActionChange, hand.handType);
    }

    private void OnDisable()
    {
        if (confirmTargetAction != null)
        {
            confirmTargetAction.RemoveOnChangeListener(OnConfirmActionChange, hand.handType);
        }
    }

    private void OnConfirmActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            //MoveTowardsFirstPoint(distanceAhead);
        }
    }

    public IEnumerator MoveTowardsFirstPoint(float distance)
    {
        yield return new WaitForSeconds(0.05f);
        var vector = agent.path.corners[1] - transform.position;
        var distanceMin = Mathf.Min(distance, vector.magnitude);
        print(distanceMin);
        transform.Translate(vector * distanceMin, Space.World);
    }

    void Awake()
    {
        if (surface != null)
        {
            surface.BuildNavMesh();
        }
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }
}
