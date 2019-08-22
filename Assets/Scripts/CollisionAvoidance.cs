using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    public bool enter = true;
    public bool stay = true;
    public bool exit = true;
    [SerializeField] public bool inCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enter)
        {
            inCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (exit)
        {
            inCollision = false;
        }
    }
}
