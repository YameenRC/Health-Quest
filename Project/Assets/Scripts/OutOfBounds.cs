using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] private float minHeight = -10f; // Minimum Y coordinate to consider the player out of bounds
    [SerializeField] private PlayerLife playerLife; // Reference to the PlayerLife script attached to the player GameObject
    [SerializeField] private float restartDelay = 2f; // Delay before restarting the level

    private bool isRestarting = false; // Flag to prevent multiple restarts

    void Update()
    {
        // Check if the player's Y position falls below the minHeight
        if (transform.position.y < minHeight && !isRestarting)
        {
            // Notify PlayerLife script to handle player death
            playerLife.PlayerDeath();
            // Start the restart coroutine
            StartCoroutine(RestartLevelAfterDelay());
        }
    }

    IEnumerator RestartLevelAfterDelay()
    {
        // Set the flag to prevent multiple restarts
        isRestarting = true;

        // Wait for the specified delay
        yield return new WaitForSeconds(restartDelay);

        // Restart the level after the delay
        playerLife.Restart();
    }
}