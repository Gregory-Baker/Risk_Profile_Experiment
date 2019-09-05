using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;


namespace Valve.VR.InteractionSystem
{
    public class Timer : MonoBehaviour
    {
        public bool timerOn = false;
        public bool trialFinished = false;
        [SerializeField] float timer = 0f;
        TextMesh timerText;
        public float endDistance = 1f;
        public GameObject robot;
        public GameObject target;
        public MeshRenderer endTrialText;

        public Hand hand;
        public SteamVR_Action_Boolean moveActionDC;
        public SteamVR_Action_Boolean moveActionIC;
        public bool startedMoving = false;

        void Start()
        {
            timerText = GetComponent<TextMesh>();

            moveActionDC.AddOnChangeListener(OnConfirmActionChange, hand.handType);
            moveActionIC.AddOnChangeListener(OnConfirmActionChange, hand.handType);
        }

        private void OnConfirmActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue && !startedMoving)
            {
                startedMoving = true;
            }
            moveActionDC.RemoveOnChangeListener(OnConfirmActionChange, hand.handType);
            moveActionIC.RemoveOnChangeListener(OnConfirmActionChange, hand.handType);
        }


        // Update is called once per frame
        void Update()
        {
            if (calculateEuclidianDistance(robot.transform.position, target.transform.position) < endDistance)
            {
                timerOn = false;
                trialFinished = true;
                endTrialText.enabled = true;
            }

            if (timerOn)
            {
                timer += Time.deltaTime;
                float minutes = Mathf.Floor(timer / 60);
                float seconds = timer % 60;
                string text = string.Format("{0:0}:{1:00}", minutes, seconds);
                timerText.text = text;
            }
        }

        private float calculateEuclidianDistance(Vector3 position, Vector3 target)
        {
            Vector3 toTargetVec = target - position;

            float euclidDist = Mathf.Sqrt(Mathf.Pow(toTargetVec.x, 2) + Mathf.Pow(toTargetVec.z, 2));

            return euclidDist;
        }
    }
}
