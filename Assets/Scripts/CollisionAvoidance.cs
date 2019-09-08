using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    bool enter = true;
    bool stay = true;
    bool exit = true;
    [SerializeField] public bool inCollision = false;
    public MeshRenderer uiPrompt;
    public Collider collisionCollider;

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
        }
    }

    private void OnTriggerStay(Collider other)
    {

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
