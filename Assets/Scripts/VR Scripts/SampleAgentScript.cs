using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleAgentScript : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    LineRenderer line;
    public Transform endReticle;
    float pathLength = 0f;
    bool pathLengthRqd = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        line = this.GetComponent<LineRenderer>();
        agent.SetDestination(endReticle.position);
    }

    void Update()
    {
        if (pathLengthRqd)
        {
            float pathLength = calculatePathDistance(agent.path);
            if (pathLength != 0)
            {
                print("Optimum path length: " + pathLength);
                pathLengthRqd = false;
            }
        }
        else
        {
            agent.SetDestination(target.position);
            OnDrawGizmosSelected();
        }
    }

    void OnDrawGizmosSelected()
    {

        if (agent == null || agent.path == null)
            return;

        if (line == null)
        {
            line.startWidth = 0.5f;
            line.endWidth = 0.5f;
            line.startColor = Color.yellow;
        }

        var path = agent.path;

        line.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }

    private float calculatePathDistance(NavMeshPath path)
    {
        float distance = .0f;
        for (var i = 0; i < path.corners.Length - 1; i++)
        {
            distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }
        return distance;
    }

    private float FindOptimalPathLength()
    {
        agent.SetDestination(endReticle.position);
        float pathLength = agent.remainingDistance;
        agent.SetDestination(agent.GetComponentInParent<Transform>().position);
        return pathLength;
    }
}
