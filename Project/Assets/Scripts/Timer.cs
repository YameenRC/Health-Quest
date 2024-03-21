using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText; // Assign this in the inspector
    [SerializeField] private PlayerLife playerLife; // Assign the player's PlayerLife script in the inspector
    [SerializeField] private float timeDuration = 120f; // Duration of the timer in seconds
    [SerializeField] private float restartDelay = 2f; // Delay before restarting the level, editable in the Inspector

    private float timeRemaining;
    private bool timerRunning = false;

    private void Start()
    {
        SetTime(timeDuration); // Initialize the timer with the specified duration
        StartTimer(); // Start the timer immediately
    }

    public void SetTime(float time)
    {
        timeRemaining = time;
        UpdateTimerDisplay(); // Update display to show the set time initially
    }

    public void StartTimer()
    {
        if (!timerRunning)
        {
            timerRunning = true;
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
        UpdateTimerDisplay(); // This will ensure the display is correct after the timer stops
    }

    private void Update()
    {
        if (timerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else if (timerRunning && timeRemaining <= 0)
        {
            StopTimer();
            TimeExpired();
        }
    }

    private void UpdateTimerDisplay()
    {
        timerText.text = "Time: " + (timeRemaining > 0 ? FormatTime(timeRemaining) : "00:00");
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimeExpired()
    {
        if (playerLife != null)
        {
            playerLife.PlayerDeath();
            StartCoroutine(RestartLevelAfterDelay());
        }
    }

    private IEnumerator RestartLevelAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
