using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestionFurtherButton : MonoBehaviour
{
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] UIController gateCount;
    [SerializeField] RobotController robot;
    [SerializeField] QuestionButton questionButton;

    int currentGate = 0;
    int currentGateRotation = 0;
    int gateCounter = 0;
    public string currentButtonText;

    string[,] gateResponses =
     { 
        {"When examining this choice I look at the worst case outcome, therefore I believe that the certainty in gaining 1,920 outweighs the chance of gaining nothing.",
            "When examining this choice I look at the best case outcome, therefore I believe that the chance to gain 2,000 outweighs the certainty in gaining 1,920.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. Doing this I determine the risk to be worth 1,953 and the certain choice to be worth 1,920. Therefore I chose the risk.",
                    "To assess this choice I look at both the chance to gain the value and the value itself. I alter both to create comparison values to help aid my decision. In this case I found the risk to be worth 667 and the certain value to be worth 775. Therefore I chose certainty." },
        {"When examining this choice I look at the worst case outcome. In this case that would be losing 2,400. Therefore I believe that the certainty in losing -2,304 outweighs the chance of losing 2,400.",
            "Looking at this choice I see the best case outcome as losing nothing. Therefore I would rather risk losing 2,400 with the chance of losing 0 than certainly lose 2,304.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. Doing this I determine the risk to be worth -2,344 and the certain choice to be worth -2,304. Therefore I chose certainty.",
                    "To assess this choice I look at both the chance to gain the value and the value itself. I alter both to create comparison values to help aid my decision. In this case I found the risk to be worth -1,896 and the certain value to be worth -2,047. Therefore I chose the risk." },
        {"When examining this choice I look at the worst case outcome, therefore I believe that the certainty in gaining 2,400 outweighs the chance of gaining nothing.",
            "When examining this choice I look at the best case outcome, therefore I believe that the chance to gain 3,200 outweighs the certainty in gaining 2,400.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. Doing this I determine the risk to be worth 2,560 and the certain choice to be worth 2,400. Therefore I chose the risk.",
                    "To assess this choice I look at both the chance to gain the value and the value itself. I alter both to create comparison values to help aid my decision. In this case I found the risk to be worth 737 and the certain value to be worth 943. Therefore I chose certainty." },
        {"When examining this choice I look at the worst case outcome. In this case that would be losing 2,560. Therefore I believe that the certainty in losing 1,920 outweighs the chance of losing 2,560.",
            "Looking at this choice I see the best case outcome as losing nothing. Therefore I would rather risk losing 2,560 with the chance of losing 0 than certainly lose 1,920.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. Doing this I determine the risk to be worth -2,048 and the certain choice to be worth -1,920. Therefore I chose certainty.",
                    "To assess this choice I look at both the chance to gain the value and the value itself. I alter both to create comparison values to help aid my decision. In this case I found the risk to be worth -1,502 and the certain value to be worth -1,743. Therefore I chose the risk." },
        {"When examining this choice I look at the worst case outcome. As both options have the same worst case, I look at which is more likely to give me a gain. Therefore I chose the 0.2% chance to gain 2,500.",
            "When examining this choice I look at the best case outcome. In this case the best case will gain me 5,000. Therefore this is the option I chose.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. In this case both options come to a value of 500. Therefore I chose between these options by flipping a virtual coin.",
                    "To assess this choice I look at both the chance to gain the value and the value itself. I alter both to create comparison values to help aid my decision. In this case I found the 5,000 choice to have a value of 26, comparably I found the 2,500 choice to have a value of 21. Therefore I chose the option containing the 5,000." },
        {"When examining this choice I look at the worst case outcome. In this case that would be losing 5,500. Therefore I believe chose the option where the worst case is losing 2,750.",
            "When examining this choice I look at the best case outcome. As both options have the same best case, I look at which is less likely to give me a loss. Therefore I chose the 0.1% chance to gain to lose 5,500.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. In this case both options come to a value of -550. Therefore I chose between these options by flipping a virtual coin.",
                    "To assess this choice I look at both the chance to gain the value and the value itself. I alter both to create comparison values to help aid my decision. In this case I found the -5,500 choice to have a value of -37, comparably I found the -2,750 choice to have a value of -32. Therefore I chose the 0.2% chance to lose 2,750." },
        {"When examining this choice I look at the worst case outcome, therefore I believe that the certainty in gaining 540 outweighs the chance of gaining nothing.",
            "When examining this choice I look at the best case outcome, therefore I believe that the chance to gain 1,200 outweighs the certainty in gaining 540.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. Doing this I determine the risk to be worth 600 and the certain choice to be worth 540. Therefore I chose the risk.",
                    "To assess this choice I look at both the chance to gain the value and the value itself. I alter both to create comparison values to help aid my decision. In this case I found the risk to be worth 215 and the certain value to be worth 253. Therefore I chose certainty." },
        {"When examining this choice I look at the worst case outcome. In this case that would be losing 1,100. Therefore I believe that the certainty in losing 495 outweighs the chance of losing 1,100.",
            "Looking at this choice I see the best case outcome as losing nothing. Therefore I would rather risk losing 1,100 with the chance of losing 0 than certainly lose 495.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. Doing this I determine the risk to be worth -550 and the certain choice to be worth -495. Therefore I chose certainty.",
                    "To assess this choice I look at both the chance to gain the value and the value itself. I alter both to create comparison values to help aid my decision. In this case I found the risk to be worth -484 and the certain value to be worth -528. Therefore I chose the risk." },
        {"When examining this choice I look at the worst case outcome, therefore I believe that the certainty in gaining 1,500 outweighs the chance of gaining nothing.",
            "When examining this choice I look at the best case outcome, therefore I believe that the chance to gain 2,000 outweighs the certainty in gaining 1,500.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. Doing this I determine the risk to be worth 1,600 and the certain choice to be worth 1,500. Therefore I chose the risk.",
                    "To assess this choice I look at both the chance to gain the value and the value itself. I alter both to create comparison values to help aid my decision. In this case I found the risk to be worth 487 and the certain value to be worth 623. Therefore I chose certainty." },
        {"When examining this choice I look at the worst case outcome. In this case that would be losing 2,200. Therefore I believe that the certainty in losing 1,650 outweighs the chance of losing 2,200.",
            "Looking at this choice I see the best case outcome as losing nothing. Therefore I would rather risk losing 2,200 with the chance of losing 0 than certainly lose 1,650.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. Doing this I determine the risk to be worth -1,760 and the certain choice to be worth -1,650. Therefore I chose certainty.",
                    "To assess this choice I look at both the chance to gain the value and the value itself. I alter both to create comparison values to help aid my decision. In this case I found the risk to be worth –1,314 and the certain value to be worth -1,526. Therefore I chose the risk." },
    };

public void ProcessButtonPush()
    {
        if (!gateCount.isTutorial && questionButton.isPushed)
        {
            gateCounter = gateCount.gateCounter;
            currentGate = loadedGates.GateOrder[gateCounter];
            SetText(gateResponses[currentGate, robot.RobotProfile]);
        }
        else if(questionButton.isPushed)
        {
            SetText("Questioning the robot further will allow further insight into how the robot has come to it's decision. After having questioned it further you may accept or alter its decision");
        }
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
