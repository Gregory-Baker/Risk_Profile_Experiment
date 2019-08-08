using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AcceptDecisionButton : MonoBehaviour
{
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] UIController gateCount;
    [SerializeField] GameObject robot;
    [SerializeField] ScoreKeeper scoreKeeper;

    bool isPushed = false;
    int currentGate = 0;
    int currentGateRotation = 0;
    int gateCounter = 0;
    public string currentButtonText;

    public void ProcessButtonPush()
    {
            robot.SendMessage("ReceivedMessageAccept");
            scoreKeeper.SendMessage("ReceivedMessageAccept");
            gateCounter = gateCount.gateCounter;
            currentGate = loadedGates.GateOrder[gateCounter];
            SetText("Accepted");
    }


    private void RefreshButton()
    {
        gateCounter = gateCount.gateCounter;
        currentGate = loadedGates.GateOrder[gateCounter];

        if (currentButtonText != currentGate.ToString())
        {
            SetText("Question the Robot Further");
        }
    }

    public void SetText(string text)
    {
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = text;

        currentButtonText = txt.text;
    }
}
