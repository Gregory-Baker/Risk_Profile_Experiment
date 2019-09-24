using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TrackRobotPosition : MonoBehaviour
{
    public GameObject robot;

    public bool egocentric = true;
    public bool trackRotation = true;

    public GameObject teleportPlayerObject;

    public float maxTurnAnglePerSecond = 10f;

    public float camZOffset;

    Vector3 previousRobotPosition;
    Vector3 heading;

    Vector3 playerTranslation;

    public float distanceTravelled = 0f;

    float currentPlayerAngle;
    float currentRobotAngle;
    float previousRobotAngle;
    float headRotationAngle;

    Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        previousRobotPosition = robot.transform.position;
        previousRobotAngle = robot.transform.eulerAngles.y;
        timer = GetComponentInChildren<Timer>();
        //if (egocentric)
        //{
        //    SetTeleportActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (egocentric)
        {
            //TranslatePlayerWithRobot();
            MovePlayerToRobotPos();

            if (trackRotation)
            {
                TurnPlayerWithRobot();
            }
        }
        else
        {

            if (trackRotation)
            {
                TurnPlayerTowardsRobot();
            }
        }
    }

    private void TranslatePlayerWithRobot()
    {
        playerTranslation = robot.transform.position - previousRobotPosition;
        playerTranslation.y = 0;

        transform.Translate(playerTranslation, Space.World);

        previousRobotPosition = robot.transform.position;
    }

    public void MovePlayerToRobotPos()
    {
        Vector3 heading = robot.transform.position - transform.position + camZOffset * robot.transform.forward;
        heading.y = 0;
        if (timer != null && timer.startedMoving && !timer.trialFinished && heading.magnitude > 0.002)
        {
            timer.timerOn = true;
        }
        transform.Translate(heading, Space.World);
        distanceTravelled += heading.magnitude;
    }

    private void TurnPlayerWithRobot()
    {
        currentRobotAngle = robot.transform.eulerAngles.y;
        float angleDiff = currentRobotAngle - previousRobotAngle;
        if (angleDiff > 180)
        {
            angleDiff -= 360;
        }
        else if (angleDiff < -180)
        {
            angleDiff += 360;
        }

        if (Mathf.Abs(angleDiff) < maxTurnAnglePerSecond * Time.deltaTime)
        {
            headRotationAngle = angleDiff;
        }
        else
        {
            headRotationAngle = Mathf.Sign(angleDiff) * maxTurnAnglePerSecond * Time.deltaTime;
        }

        transform.Rotate(transform.up, headRotationAngle);

        previousRobotAngle = currentRobotAngle;
    }

    public void TurnPlayerToRobotHeading()
    {
        currentRobotAngle = robot.transform.eulerAngles.y;
        currentPlayerAngle = transform.eulerAngles.y;
        float angleDiff = currentRobotAngle - currentPlayerAngle;

        transform.Rotate(transform.up, angleDiff);
    }

    private void TurnPlayerTowardsRobot()
    {
        if (Vector3.Distance(robot.transform.position, transform.position) > 0.5f)
        {
            transform.LookAt(robot.transform);
        }
    }

    public void SetTeleportActive(bool input)
    {
        teleportPlayerObject.SetActive(input);
    }
}
