using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


namespace Valve.VR.InteractionSystem
{
    public class FlipCamera : MonoBehaviour
    {
        public SteamVR_Action_Boolean confirmTargetAction;

        public Hand hand;

        public float rotationDegrees;

        public MeshRenderer uiIndicator;

        private void OnEnable()
        {
            if (hand == null)
                hand = GetComponent<Hand>();

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
                RotateCamera(rotationDegrees);

                if (uiIndicator != null)
                {
                    uiIndicator.enabled = !uiIndicator.enabled;
                }
            }
        }

        private void RotateCamera(float rotation)
        {
            transform.Rotate(Vector3.up, rotation);
        }


    }
}
