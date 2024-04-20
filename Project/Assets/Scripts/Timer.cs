using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour // This class manages a countdown timer for a game level, handling time display and level restart on timeout.
{
    [SerializeField] private Text timerText; // Assign this in the inspector
    [SerializeField] private PlayerLife playerLife;  // Reference to the PlayerLife script to call when time expires.
    [SerializeField] private float timeDuration = 120f; // Duration of the timer in seconds
    [SerializeField] private float restartDelay = 2f; // Delay before restarting the level, editable in the Inspector

    private float timeRemaining; // Variable to track the remaining time.
    private bool timerRunning = false; // Flag to control the timer's operation.

    private void Start()
    {
        SetTime(timeDuration); // Initialize the timer with the specified duration
        StartTimer(); // Start the timer immediately
    }

    public void SetTime(float time) // Set the remaining time and update the timer display.
    {
        timeRemaining = time;
        UpdateTimerDisplay(); // Update display to show the set time initially
    }

    public void StartTimer() // Start the timer by setting the running flag to true.
    {
        if (!timerRunning)
        {
            timerRunning = true;
        }
    }

    public void StopTimer() // Stop the timer and update the display to show the stopped time.
    {
        timerRunning = false;
        UpdateTimerDisplay(); // This will ensure the display is correct after the timer stops
    }

    private void Update()
    {
        if (timerRunning && timeRemaining > 0) // Update the countdown each frame if the timer is running and time is left.
        {
            timeRemaining -= Time.deltaTime; // Decrease time remaining.
            UpdateTimerDisplay(); // Update the time display each frame.
        }
        else if (timerRunning && timeRemaining <= 0) // When no time is left, stop the timer and handle the expiration (e.g., player death).
        {
            StopTimer();
            TimeExpired(); // Call when the timer runs out.
        }
    }

    private void UpdateTimerDisplay() // Update the timer text on the UI.
    {
        timerText.text = "Time: " + (timeRemaining > 0 ? FormatTime(timeRemaining) : "00:00"); // Format and display the remaining time. Show "00:00" when no time remains.
    }

    private string FormatTime(float time) // Helper method to format time in minutes and seconds.
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimeExpired() // Actions to perform when the timer expires.
    {
        if (playerLife != null)
        {
            playerLife.PlayerDeath(); // Trigger player death sequence.
            StartCoroutine(RestartLevelAfterDelay()); // Begin the delay before restarting the level.
        }
    }

    private IEnumerator RestartLevelAfterDelay() // Coroutine to wait a specified delay before restarting the level.
    {
        yield return new WaitForSeconds(restartDelay); // Wait for the delay period.
        RestartLevel(); // Restart the level.
    }

    private void RestartLevel() // Reload the current scene to restart the level.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load the current scene.
    }
}
