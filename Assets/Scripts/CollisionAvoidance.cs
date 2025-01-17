﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    bool enter = true;
    bool exit = true;
    public bool inCollision = false;
    public MeshRenderer uiPrompt;
    public Collider collisionCollider;
    public Status status;
    public int collisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        collisionCollider.enabled = true;
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
            if (uiPrompt != null)
            {
                uiPrompt.enabled = true;
            }
            collisions += 1;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (exit)
        {
            inCollision = false;
            if (uiPrompt != null)
            {
                uiPrompt.enabled = false;
            }
        }
    }
}
