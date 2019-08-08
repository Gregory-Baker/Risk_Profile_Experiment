using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RobotController : MonoBehaviour
{

    //defines the robot profile:
        // 0 = risk averse
        // 1 = risk seeking
        // 2 = expected value
        // 3 = human approach
    public int RobotProfile = 0;

    [SerializeField] Transform goal;
    [SerializeField] AudioSource engineSound;
    [SerializeField] UIController UI;

    Vector3[,] fullWaypointsList = new Vector3[10, 3];
    public Vector3 currentWaypoint = new Vector3();

    public Camera cam;
    public NavMeshAgent agent;
    public Path_Loader waypointList;

    public Renderer trackLeft;
    public Renderer trackRight;

    private Vector3 previousPosition;
    public float curSpeed;
    public float prevSpeed;

    int gateCounter = 0;

    //1 = certain route
    //0 = uncertain route

    public int[,] robotGateDecisions = new int[10, 4] {
       { 1, 0, 0, 1 },
       { 1, 0, 1, 0 },
       { 1, 0, 0, 1 },
       { 1, 0, 1, 0 },
       { 1, 0, 2, 0 },
       { 1, 0, 2, 1 },
       { 1, 0, 0, 1 },
       { 1, 0, 1, 0 },
       { 1, 0, 0, 1 },
       { 1, 0, 1, 0 },
       };



    void Awake()
    {
      // rand is to decided between even chances when the engine runs
        int rnd1 = UnityEngine.Random.Range(0, 2);
        int rnd2 = UnityEngine.Random.Range(0, 2);

        robotGateDecisions[4, 3] = rnd1; 
        robotGateDecisions[5, 3] = rnd2;

    }


    void Start()
    {
        fullWaypointsList = waypointList.fullWaypointsList;
    }


    void Update()
    {
        curSpeed = GetRobotSpeed();
        PlayEngineNoise(curSpeed);
        AnimateTracks();

        if (curSpeed == 0)
        {
            GetDecision();
            RotateForwards();
        }

        if (gateCounter >= 1) // if we're not at the beginning of the level
        {
            MoveToWaypoint();
        }

    }

    void ReceivedMessageAlter()
    {
        int decision = robotGateDecisions[gateCounter, RobotProfile];
        print("Alter Clicked");

        decision = AlterDecision(decision);

        currentWaypoint = fullWaypointsList[gateCounter, decision];
        gateCounter++;
        UI.gateCounter++;
    }

    private int AlterDecision(int decision)
    {
        if (decision == 1)
        {
            decision = 0;
        }
        else if (decision == 0)
        {
            decision = 1;
        }

        return decision;
    }

    void ReceivedMessageAccept()
    {
        int decision = robotGateDecisions[gateCounter, RobotProfile];
        print("Accept Clicked");
        currentWaypoint = fullWaypointsList[gateCounter, decision];

        gateCounter++;
        UI.gateCounter++;
    }


    void RotateForwards()
    {
        float rotSpeed = 75f;

        // The step size is equal to speed times frame time.
        var step = rotSpeed * Time.deltaTime;

        // Rotate our transform a step closer to the target's.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goal.rotation, step);

        float angleDiff = transform.rotation.y - goal.rotation.y;
        float angleDiffDeg = angleDiff * 180 / Mathf.PI;

        if(angleDiffDeg < 90 && angleDiffDeg != 0)
        {
            float offset = Time.time * rotSpeed/25;

            trackLeft.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
            trackRight.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));

            engineSound.volume = 0.1f;
            engineSound.pitch = 1.5f;
        }
        else if(angleDiffDeg > 90 && angleDiffDeg != 0)
        {
            float offset = Time.time * rotSpeed/25;

            trackLeft.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
            trackRight.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

            engineSound.volume = 0.1f;
            engineSound.pitch = 1.5f;
        }

    }

    private void AnimateTracks()
    {
        float offset = Time.time * curSpeed;

        trackLeft.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        trackRight.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }

    private float GetRobotSpeed()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        return curSpeed;
    }

    void PlayEngineNoise(float curSpeed)
    {
        engineSound.volume = 0.1f;
        engineSound.pitch = 1f * curSpeed / 3;

        if (curSpeed == 0)
        {
            engineSound.volume = 0.05f;
            engineSound.pitch = 1f;
        }
    }

    private void MoveToWaypoint()
    {
        agent.SetDestination(currentWaypoint);


        float dist = Vector3.Distance(currentWaypoint, transform.position);

        if (dist < 1)
        {
            currentWaypoint = fullWaypointsList[gateCounter-1, 2];
        }
    }

    void GetDecision()
    {
        if (Input.GetKeyDown("u"))
        {
            currentWaypoint = fullWaypointsList[gateCounter, 0];

            gateCounter++;
        }
        else if (Input.GetKeyDown("c"))
        {
            currentWaypoint = fullWaypointsList[gateCounter, 1];

            gateCounter++;
        }
    }

    


    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }*/
}
