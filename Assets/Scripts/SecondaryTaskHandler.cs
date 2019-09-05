using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class SecondaryTaskHandler : MonoBehaviour
{
    public Texture leftArrow;
    public Texture rightArrow;
    public int errors = 0;
    public SteamVR_Action_Boolean a_leftArrow;
    public SteamVR_Action_Boolean a_rightArrow;
    public Hand lHand;
    public Hand rHand;
    public GameObject activeArrow = null;
    public float timeDelay = 10f;
    Coroutine hintCoroutine;
    public Timer timer;
    [SerializeField] bool secondaryTaskActive = false;

    // Start is called before the first frame update
    void Start()
    {
        AssignVRInputActions();
        //Invoke("ArrowActivation", 2);
        //hintCoroutine = StartCoroutine(HintCoroutine());
    }

   

    private void ArrowActivation()
    {
        Debug.Log("time up");
        bool handSelect = (UnityEngine.Random.value > 0.5f);
        
        if(handSelect == true)
            activeArrow = transform.Find("LeftArrow").gameObject;
        else
            activeArrow = transform.Find("RightArrow").gameObject;
        ArrowActivate();
    }

    private void AssignVRInputActions()
    {
        if (lHand == null)
            lHand = this.GetComponent<Hand>();

        if (rHand == null)
            rHand = this.GetComponent<Hand>();

        if (a_leftArrow == null)
        {
            Debug.LogError("No left action assigned");
            return;
        }

        a_leftArrow.AddOnChangeListener(OnLeftArrowActionChange, lHand.handType);

        if (a_rightArrow == null)
        {
            Debug.LogError("No right action assigned");
            return;
        }

        a_rightArrow.AddOnChangeListener(OnRightArrowActionChange, rHand.handType);
    }

    private IEnumerator HintCoroutine()
    {
        while (true)
        {
            //ControllerButtonHints.ShowTextHint(lHand, a_leftArrow, "Left Arrow Response");
            //ControllerButtonHints.ShowTextHint(rHand, a_rightArrow, "Right Arrow Response");
            yield return null;
        }
    }

    private void OnLeftArrowActionChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        //Debug.Log("Left");
        if (activeArrow != null)
        {
            ResponseCheck(leftArrow);
            if (hintCoroutine != null)
            {
                StopCoroutine(hintCoroutine);
                //ControllerButtonHints.HideTextHint(lHand, a_leftArrow);                
                hintCoroutine = null;
            }
        }
    }

    private void OnRightArrowActionChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        //Debug.Log("Right");
        if(activeArrow != null)
        {
            ResponseCheck(rightArrow);
            if (hintCoroutine != null)
            {
                StopCoroutine(hintCoroutine);
                //ControllerButtonHints.HideTextHint(rHand, a_rightArrow);
                hintCoroutine = null;
            }
        }
    }       

    // Update is called once per frame
    void Update()
    {
        if (!secondaryTaskActive && timer.timerOn)
        {
            secondaryTaskActive = true;
            Invoke("ArrowActivation", timeDelay);
        }
        //at a random time set the arrow for one of the two children


        /*activeArrow = transform.Find("LeftArrow").gameObject;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {            
            ArrowActivate();            
        }
        */
    }

    private void ResponseCheck(Texture arrow)
    {
       // Debug.Log("check" + arrow.name);
        //Debug.Log("active" + activeArrow.name);
        if(arrow == activeArrow.GetComponent<RawImage>().texture)
        {
            ArrowDeactivate();
            errors = 0;
            Invoke("ArrowActivation", timeDelay);
        }
        else//else it is an error so increase the error count by 1
        {
            errors++;
        }
    }
        

    private void ArrowDeactivate()
    {
        //Debug.Log("ArrowDeactivate");
        ExecuteEvents.Execute(activeArrow, null, (ISecondaryTaskMsgHandler handler, BaseEventData data) => handler.HideArrow());
        activeArrow = null;
    }

    void ArrowActivate()
    {
        //Debug.Log("ArrowActivate" + activeArrow.name);
        bool arrowSelect = (UnityEngine.Random.value > 0.5f);
        SecondaryTaskMsgData secondaryTaskMsgData = null;
        if (arrowSelect == true)
            secondaryTaskMsgData = new SecondaryTaskMsgData { texture = leftArrow };
        else
            secondaryTaskMsgData = new SecondaryTaskMsgData { texture = rightArrow };
        ExecuteEvents.Execute(activeArrow, secondaryTaskMsgData, (ISecondaryTaskMsgHandler handler, BaseEventData data) => handler.DisplayArrow(secondaryTaskMsgData));
    }

    private void OnDisable()
    {
        CancelInvoke();
        if (activeArrow != null)
        {
            CancelInvoke();
            ExecuteEvents.Execute(activeArrow, null, (ISecondaryTaskMsgHandler handler, BaseEventData data) => handler.HideArrow());
            activeArrow = null;
        }
    }

    private void OnEnable()
    {
        
    }
}
