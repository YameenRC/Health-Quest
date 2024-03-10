using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoHealth : MonoBehaviour
{
    [SerializeField] private CollectableItems collectableItems; // Reference to the CollectableItems script attached to the player GameObject
    [SerializeField] private float restartDelay = 2f; // Delay before restarting the level

    private bool isRestarting = false; // Flag to prevent multiple restarts

    private void Update()
    {
        // Check if player's health has reached zero
        if (collectableItems != null && collectableItems.GetHealth() <= 0 && !isRestarting)
        {
            // Start the restart coroutine
            StartCoroutine(RestartLevelAfterDelay());
            // Trigger the death animation
            PlayerLife playerLife = GetComponent<PlayerLife>(); // Get reference to the PlayerLife script
            if (playerLife != null)
            {
                playerLife.PlayerDeath();
            }
        }
    }

    IEnumerator RestartLevelAfterDelay()
    {
        // Set the flag to prevent multiple restarts
        isRestarting = true;

        // Wait for the specified delay
        yield return new WaitForSeconds(restartDelay);

        // Restart the level after the delay
        RestartLevel();
    }

    private void RestartLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


