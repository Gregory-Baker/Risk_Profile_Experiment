using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionButton : MonoBehaviour
{
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] UIController gateCount;
    [SerializeField] RobotController robot;
    int currentGate = 0;
    int currentGateRotation = 0;
    int gateCounter = 0;
    public string currentButtonText;
    public bool isPushed = false;


    string[,] gateResponses =
{
        {"I chose certainty over a risk, I do not believe the risk is worth the reward", "I believe that the opportunity to gain more justifies the risk",
            "I believe, in this case, the risk is worth the reward", "I do not believe that the risk is worth the reward"},
        {"I chose certainty over a risk, I do not believe the risk is worth the reward", "I believe that the opportunity to lose less justifies the risk",
            "I do not believe that the risk is worth the reward", "I believe, in this case, the risk is worth the reward"},
        {"I chose certainty over a risk, I do not believe the risk is worth the reward", "I believe that the opportunity to gain more justifies the risk",
            "I believe, in this case, the risk is worth the reward", "I do not believe that the risk is worth the reward"},
        {"I chose certainty over a risk, I do not believe the risk is worth the reward", "I believe that the opportunity to lose less justifies the risk",
            "I do not believe that the risk is worth the reward", "I believe, in this case, the risk is worth the reward"},
        {"I chose the more certain option over the more risky option", "I chose the option with the higher risk because I could gain more",
            "I believe that both outcomes are equal so it does not matter which I choose", "I value the opportunity to gain more"},
        {"I chose the more certain option over the more risky option", "I chose the option with more risk, as there was a higher chance not lose",
            "I believe that both outcomes are equal so it does not matter which I choose", "I value the more certain chance of losing less"},
        {"I chose certainty over a risk, I do not believe the risk is worth the reward", "I believe that the opportunity to gain more justifies the risk",
            "I believe, in this case, the risk is worth the reward", "I do not believe that the risk is worth the reward"},
        {"I chose certainty over a risk, I do not believe the risk is worth the reward", "I believe that the opportunity to lose less justifies the risk",
            "I do not believe that the risk is worth the reward", "I believe, in this case, the risk is worth the reward"},
        {"I chose certainty over a risk, I do not believe the risk is worth the reward", "I believe that the opportunity to gain more justifies the risk",
            "I believe, in this case, the risk is worth the reward", "I do not believe that the risk is worth the reward"},
        {"I chose certainty over a risk, I do not believe the risk is worth the reward", "I believe that the opportunity to lose less justifies the risk",
            "I do not believe that the risk is worth the reward", "I believe, in this case, the risk is worth the reward"},
    };

    public void ProcessButtonPush()
    {
        if (!gateCount.isTutorial)
        {
            gateCounter = gateCount.gateCounter;
            currentGate = loadedGates.GateOrder[gateCounter];
            SetText(gateResponses[currentGate, robot.RobotProfile]);
            isPushed = true;
        }
        else
        {
            isPushed = true;
            SetText("Questioning the robot gives a brief overview of why it made it's choice. Once you have questioned the robot you may question it further, at any point you can accept or alter the robot's current decision by pressing the button's to the right");
        }

    }


    private void RefreshButton()
    {
        gateCounter = gateCount.gateCounter;
        currentGate = loadedGates.GateOrder[gateCounter];

        if (currentButtonText != currentGate.ToString())
        {
            SetText("Question the Robot");
            isPushed = false;
        }
    }

    public void SetText(string text)
    {
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = text;

        currentButtonText = txt.text;
    }
}
