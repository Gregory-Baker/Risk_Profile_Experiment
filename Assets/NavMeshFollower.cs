using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshFollower : MonoBehaviour
{
    public Transform target;

    public float speed;
    public float angularSpeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 targetDir = target.position - transform.position;
        targetDir.y = 0;

        if (targetDir.magnitude > 0.05)
        {
            // The step size is equal to speed times frame time.
            float step = Mathf.Deg2Rad * angularSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
