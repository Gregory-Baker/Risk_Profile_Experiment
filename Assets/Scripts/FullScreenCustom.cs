using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenCustom : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.ExclusiveFullScreen);
        Screen.fullScreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.ExclusiveFullScreen);
        //Screen.fullScreen = !Screen.fullScreen;
    }
}
