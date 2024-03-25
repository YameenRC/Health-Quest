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
    public GameObject showCoins;
    public GameObject settingsPanel;
    public GameObject settingsBTN;

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

    void pauseGame() // switches visibility of oxygen bar and health bar, makes pause menu visible
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        showPauseMenu.SetActive(true);
        showHealthBar.SetActive(false);
        Time.timeScale = 0f; // freezes time
        isGamePaused = true;
    }

    public void SetImageEnabled(Image image, bool isEnabled)
    {
        image.enabled = isEnabled;
    }

    public void loadMainMenu() // loads main menu if main menu button pressed
    {
        resumeGame();
        SceneManager.LoadScene("Start");
    }

    public void QuitGame() // exits game
    {
        Application.Quit();

    }
}
