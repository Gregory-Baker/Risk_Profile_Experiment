using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRound : MonoBehaviour
{
    [SerializeField] UIController UI;
    [SerializeField] RobotController robot;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject scoreText;


    void Update()
    {
        if(UI.gateCounter > 9)
        {
            endScreen.SetActive(true);
            scoreText.SetActive(true);
        }
        else
        {
            endScreen.SetActive(false);
        }
    }
}
