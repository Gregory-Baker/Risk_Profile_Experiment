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

        public Status status;

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

        void FixedUpdate()
        {
            if (IsActionButtonDown(hand, forwardAction))
            {
                StartCoroutine(MoveRobotCoroutine(linearSpeed, status.communicationDelay));

                if (IsActionButtonDown(hand, rightAction))
                {
                    StartCoroutine(TurnRobotCoroutine(angularSpeed, status.communicationDelay));
                }

                if (IsActionButtonDown(hand, leftAction))
                {
                    StartCoroutine(TurnRobotCoroutine(-angularSpeed, status.communicationDelay));
                }
            }

            else if (IsActionButtonDown(hand, reverseAction))
            {
                StartCoroutine(MoveRobotCoroutine(-linearSpeed, status.communicationDelay));

                if (IsActionButtonDown(hand, rightAction))
                {
                    StartCoroutine(TurnRobotCoroutine(-angularSpeed, status.communicationDelay));
                }

                if (IsActionButtonDown(hand, leftAction))
                {
                    StartCoroutine(TurnRobotCoroutine(angularSpeed, status.communicationDelay));
                }
            }

            else
            {
                if (IsActionButtonDown(hand, rightAction))
                {
                    StartCoroutine(TurnRobotCoroutine(angularSpeed, status.communicationDelay));
                }

                if (IsActionButtonDown(hand, leftAction))
                {
                    StartCoroutine(TurnRobotCoroutine(-angularSpeed, status.communicationDelay));
                }
            }



        }

        IEnumerator MoveRobotCoroutine(float linearVelocity, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            if (linearVelocity > 0)
            {
                if (!collisionSensor.inCollision)
                {
                    transform.Translate(Vector3.forward * linearVelocity * Time.fixedDeltaTime);
                }
            }
            else
            {
                transform.Translate(Vector3.forward * linearVelocity * Time.fixedDeltaTime);
            }

        }

        IEnumerator TurnRobotCoroutine(float angularVelocity, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            transform.Rotate(transform.up * angularVelocity * Time.fixedDeltaTime);
        }
    }
}
