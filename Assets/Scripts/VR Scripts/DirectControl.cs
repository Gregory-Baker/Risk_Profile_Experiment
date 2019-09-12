using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;

namespace Valve.VR.InteractionSystem
{
    public class DirectControl : MonoBehaviour
    {
        public SteamVR_Action_Boolean forwardAction;
        public SteamVR_Action_Boolean reverseAction;
        public SteamVR_Action_Boolean leftAction;
        public SteamVR_Action_Boolean rightAction;

        public Hand hand;

        public float linearSpeed = 0.5f;
        public float angularSpeed = 1f;

        public Status robotStatus;
        public float communicationDelay;

        public CollisionAvoidance collisionSensor;

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

            communicationDelay = robotStatus.communicationDelay;
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

        void FixedUpdate()
        {
            if (IsActionButtonDown(hand, forwardAction))
            {
                if (!collisionSensor.inCollision)
                {
                    StartCoroutine(MoveRobotCoroutine(linearSpeed, communicationDelay));
                }

                if (IsActionButtonDown(hand, rightAction))
                {
                    StartCoroutine(TurnRobotCoroutine(angularSpeed, communicationDelay));
                }

                if (IsActionButtonDown(hand, leftAction))
                {
                    StartCoroutine(TurnRobotCoroutine(-angularSpeed, communicationDelay));
                }
            }

            else if (IsActionButtonDown(hand, reverseAction))
            {
                StartCoroutine(MoveRobotCoroutine(-linearSpeed, communicationDelay));

                if (IsActionButtonDown(hand, rightAction))
                {
                    StartCoroutine(TurnRobotCoroutine(-angularSpeed, communicationDelay));
                }

                if (IsActionButtonDown(hand, leftAction))
                {
                    StartCoroutine(TurnRobotCoroutine(angularSpeed, communicationDelay));
                }
            }

            else
            {
                if (IsActionButtonDown(hand, rightAction))
                {
                    StartCoroutine(TurnRobotCoroutine(angularSpeed, communicationDelay));
                }

                if (IsActionButtonDown(hand, leftAction))
                {
                    StartCoroutine(TurnRobotCoroutine(-angularSpeed, communicationDelay));
                }
            }



        }

        IEnumerator MoveRobotCoroutine(float linearVelocity, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            transform.Translate(Vector3.forward * linearVelocity * Time.fixedDeltaTime);
        }

        IEnumerator TurnRobotCoroutine(float angularVelocity, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            transform.Rotate(transform.up * angularVelocity * Time.fixedDeltaTime);
        }
    }
}
