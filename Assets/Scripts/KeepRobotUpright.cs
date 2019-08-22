using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRobotUpright : MonoBehaviour
{
    float robotHeight;

    // Start is called before the first frame update
    void Start()
    {
        robotHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilt = transform.eulerAngles;
        tilt.y = 0;
        transform.Rotate(-tilt);


        Vector3 deltaY = Vector3.up * (robotHeight - transform.position.y);
        transform.Translate(deltaY);
        

    }
}
