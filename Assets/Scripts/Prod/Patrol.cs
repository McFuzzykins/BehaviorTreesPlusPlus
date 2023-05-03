using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;

    private int currentWaypointIndex;
    public float speed;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool isWaiting = false;
    
    private void Update()
    {
        if (isWaiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter < waitTime)
            {
                return;
            }
            isWaiting = false;
        }
        Transform wp = waypoints[currentWaypointIndex];
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            transform.position = wp.position;
            waitCounter = 0f;
            isWaiting = true;


            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, speed * Time.deltaTime);
            transform.LookAt(wp.position);
        }
    }
}
