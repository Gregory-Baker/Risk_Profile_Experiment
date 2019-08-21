using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public bool timerOn = false;
    public bool trialFinished = false;
    [SerializeField] float timer = 0f;
    TextMesh timerText;
    public float endDistance = 1f;
    public GameObject robot;
    public GameObject target;
    public MeshRenderer endTrialText;

    void Start()
    {
        timerText = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (calculateEuclidianDistance(robot.transform.position, target.transform.position) < endDistance)
        {
            timerOn = false;
            trialFinished = true;
            endTrialText.enabled = true;
        }

        if (timerOn)
        {
            timer += Time.deltaTime;
            float minutes = Mathf.Floor(timer / 60);
            float seconds = timer % 60;
            string text = string.Format("{0:0}:{1:00}", minutes, seconds);
            timerText.text = text;
        }
    }

    private float calculateEuclidianDistance(Vector3 position, Vector3 target)
    {
        Vector3 toTargetVec = target - position;

        float euclidDist = Mathf.Sqrt(Mathf.Pow(toTargetVec.x, 2) + Mathf.Pow(toTargetVec.z, 2));

        return euclidDist;
    }
}
