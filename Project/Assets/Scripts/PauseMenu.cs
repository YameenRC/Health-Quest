using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject showPauseMenu;
    public GameObject showHealthBar;
    public GameObject settingsPanel;
    public GameObject settingsBTN;

    public AudioClip clickSound;
    public float delay = 0.5f; // Delay in seconds, adjust as needed
    private AudioSource audioSource;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // Get the audio source from this game object or its children
        // Make sure there's an AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // trigger to show pause menu , checks if settings menu already pressed
        {
            if (isGamePaused)
            {
                if (settingsPanel.activeSelf == true)
                {
                    settingsPanel.SetActive(false);
                }
                resumeGame();
            }
            else
            {
                settingsBTN.SetActive(true);
                pauseGame();
            }
        }

    }

    public void resumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        showPauseMenu.SetActive(false);
        showHealthBar.SetActive(true);
        Time.timeScale = 1f; // makes time restart in the game again
        isGamePaused = false;
    }

    void pauseGame() // makes pause menu visible and freeze time
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        showPauseMenu.SetActive(true);
        showHealthBar.SetActive(true);
        Time.timeScale = 0f; // freezes time
        isGamePaused = true;
    }

    public void SetImageEnabled(Image image, bool isEnabled)
    {
        image.enabled = isEnabled;
    }

    public void loadMainMenu() // loads main menu if main menu button pressed
    {
        // Ensure the cursor is visible and unlocked before going to the main menu.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        resumeGame();
        SceneManager.LoadScene("Start");
    }

    public void QuitGame() // exits game
    {
        Application.Quit();

    }

    public void Restart()
    {
        // Play the click sound
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        resumeGame();

        // Start the coroutine to restart the scene after a delay
        StartCoroutine(RestartAfterDelay(SceneManager.GetActiveScene().name));

    }

    IEnumerator RestartAfterDelay(string sceneName)
    {
        // Wait for the specified delay, during which the click sound can play
        yield return new WaitForSeconds(delay);

        // Now the sound has had time to play, resume the game

        // Load the scene
        SceneManager.LoadScene(sceneName);
    }
}
