using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int firstWayPoint = 0;
    [SerializeField] private float platformSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[firstWayPoint].transform.position, transform.position) < .1f)
        {
            firstWayPoint++;
            if (firstWayPoint >= waypoints.Length)
            {
                firstWayPoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[firstWayPoint].transform.position, Time.deltaTime * platformSpeed);
    }
}
