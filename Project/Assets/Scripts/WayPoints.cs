using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float platformSpeed = 2f;

    private int currentWaypointIndex = 0;
    private int direction = 1; // 1 for moving forward, -1 for moving backward

    // Update is called once per frame
    void Update()
    {
        // Move the platform towards the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * platformSpeed);

        // Check if the platform has reached the current waypoint
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < 0.01f)
        {
            // Move to the next waypoint based on the direction
            currentWaypointIndex += direction;

            // If we reach the end or start of the waypoints array, switch direction
            if (currentWaypointIndex >= waypoints.Length || currentWaypointIndex < 0)
            {
                direction *= -1;
                currentWaypointIndex += direction * 2; // Skip the current waypoint to avoid stopping at it
            }
        }
    }
}

