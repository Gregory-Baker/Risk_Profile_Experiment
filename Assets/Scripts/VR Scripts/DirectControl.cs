﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using UnityEngine.AI;

public class DirectControl : MonoBehaviour
{
    public SteamVR_Action_Boolean forwardAction;
    public SteamVR_Action_Boolean reverseAction;
    public SteamVR_Action_Boolean leftAction;
    public SteamVR_Action_Boolean rightAction;

    public Hand hand;

    public float linearSpeed = 0.5f;
    public float angularSpeed = 1f;

    public Status status;

    public CollisionAvoidance collisionSensor;

    public SpriteRenderer uparrow;
    public SpriteRenderer downarrow;
    public SpriteRenderer leftarrow;
    public SpriteRenderer rightarrow;
    public SpriteRenderer uprightarrow;
    public SpriteRenderer upleftarrow;
    public SpriteRenderer downrightarrow;
    public SpriteRenderer downleftarrow;
    public SpriteRenderer[] directionArrows;

    public float turnTime;


    [HideInInspector] public bool showHint;

    void OnEnable()
    {
        if (hand == null)
            Debug.LogError("<b>[SteamVR Interaction]</b> No hand assigned");

        if (forwardAction == null || reverseAction == null || leftAction == null || rightAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No action assigned");
            return;
        }

        collisionSensor.enabled = true;

        //communicationDelay = status.communicationDelay;
    }

    private void OnDisable()
    {
        collisionSensor.enabled = false;
    }

    private void OnConfirmActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            print("action identified");
        }
    }

    private bool IsActionButtonDown(Hand hand, SteamVR_Action_Boolean action)
    {
        return action.GetState(hand.handType);
    }

    void Update()
    {
        foreach (SpriteRenderer arrow in directionArrows)
        {
            arrow.enabled = false;
        }

        if (IsActionButtonDown(hand, forwardAction))
        {
            StartCoroutine(MoveRobotCoroutine(linearSpeed, status.communicationDelay));

            if (IsActionButtonDown(hand, rightAction))
            {
                StartCoroutine(TurnRobotCoroutine(angularSpeed, status.communicationDelay));
                uprightarrow.enabled = true;
            }

            else if (IsActionButtonDown(hand, leftAction))
            {
                StartCoroutine(TurnRobotCoroutine(-angularSpeed, status.communicationDelay));
                upleftarrow.enabled = true;
            }

            else
            {
                uparrow.enabled = true;
            }
        }

        else if (IsActionButtonDown(hand, reverseAction))
        {
            StartCoroutine(MoveRobotCoroutine(-linearSpeed, status.communicationDelay));

            if (IsActionButtonDown(hand, rightAction))
            {
                StartCoroutine(TurnRobotCoroutine(-angularSpeed, status.communicationDelay));
                downrightarrow.enabled = true;
            }

            else if (IsActionButtonDown(hand, leftAction))
            {
                StartCoroutine(TurnRobotCoroutine(angularSpeed, status.communicationDelay));
                downleftarrow.enabled = true;
            }

            else
            {
                downarrow.enabled = true;
            }
        }

        else
        {
            if (IsActionButtonDown(hand, rightAction))
            {
                StartCoroutine(TurnRobotCoroutine(1.5f * angularSpeed, status.communicationDelay));
                rightarrow.enabled = true;
                turnTime += Time.deltaTime;
            }

            if (IsActionButtonDown(hand, leftAction))
            {
                StartCoroutine(TurnRobotCoroutine(-1.5f * angularSpeed, status.communicationDelay));
                leftarrow.enabled = true;
                turnTime += Time.deltaTime;
            }
        }

        if (showHint)
        {
            ShowHint();
        }
            

    }

    IEnumerator MoveRobotCoroutine(float linearVelocity, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (linearVelocity > 0)
        {
            if (!collisionSensor.inCollision)
            {
                transform.Translate(Vector3.forward * linearVelocity * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * linearVelocity * Time.deltaTime);
        }

    }

    IEnumerator TurnRobotCoroutine(float angularVelocity, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        transform.Rotate(transform.up * angularVelocity * Time.deltaTime);
    }

    public void ShowHint()
    {
        ControllerButtonHints.ShowTextHint(hand, forwardAction, "Move Robot");
    }

    private void OnApplicationQuit()
    {
        if (!status.tutorial)
        {
            //LogTurnTime();
        }
    }

    private void LogTurnTime()
    {
        string path = status.trialDataFile;
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
        {
            file.WriteLine("Turn Time, " + turnTime);
        }
    }
}