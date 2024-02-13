using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// if i want the spike to just rotate in place and kill the player remove the waypoint script on it in unity

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
