using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowDisplay : MonoBehaviour, ISecondaryTaskMsgHandler
{    
    RawImage rawImage;    

    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }    

    public void DisplayArrow(SecondaryTaskMsgData msgData)
    {
        rawImage.texture = msgData.texture;
        rawImage.enabled = true;
    }

    public void HideArrow()
    {
        rawImage.enabled = false;
    }
        
}
