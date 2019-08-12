using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour
{
    [SerializeField] GameObject Robot;

    Vector3 offset = new Vector3(2.33f, 1.75f, 0f);

    // Update is called once per frame
    void Update()
    {
        transform.position = Robot.transform.position + offset;
    }
}
