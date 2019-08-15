using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem
{
    //---------------------------
    public class ConfirmTarget : MonoBehaviour
    {
        public SteamVR_Action_Boolean confirmTargetAction;

        public Hand hand;

        public GameObject objectToMove;
        public GameObject objectToTrack;

        public float delayTime;

        public float verticalOffset = 0f;

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
                confirmTargetAction.RemoveOnChangeListener(OnConfirmActionChange, hand.handType);
        }

        private void OnConfirmActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                StartCoroutine(MoveTargetCoroutine(delayTime));
            }
        }

        IEnumerator MoveTargetCoroutine(float delay)
        {
            yield return new WaitForSeconds(delayTime);
            Vector3 targetPosition = objectToTrack.transform.position;
            targetPosition.y += verticalOffset;
            MoveTarget(objectToMove, targetPosition);
        }

        public void MoveTarget(GameObject objectToMove, Vector3 position)
        {
            objectToMove.transform.position = position;
        }
    }
}