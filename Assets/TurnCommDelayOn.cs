using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCommDelayOn : MonoBehaviour
{
    public Status status;
    public Transform commDelayPos;
    public Transform robot;
    public MeshRenderer uiText;
    public bool commDelayOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!commDelayOn)
        {
            float distance = commDelayPos.position.x - robot.position.x;
            if (Mathf.Abs(distance) < 0.05)
            {
                StartCoroutine(CommDelayOnCoroutine(5f));
                commDelayOn = true;
            }
        }
    }

    public IEnumerator CommDelayOnCoroutine(float duration)
    {
        uiText.enabled = true;
        status.communicationDelayOn = true;
        yield return new WaitForSeconds(duration);
        uiText.enabled = false;
    }
}
