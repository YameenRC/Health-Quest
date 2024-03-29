using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackAScene : MonoBehaviour
{
    public AudioClip clickSound;
    public float delay = 0.5f; // Delay in seconds, adjust as needed
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // Get the audio source from this game object or its children
        // Make sure there's an AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Call this method when the button is clicked
    public void ReturnBtn()
    {
        // Play the click sound
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        // Start the coroutine to load the scene
        StartCoroutine(LoadEndScreenAfterDelay("FinishGameScreen")); // Replace "Sources" with your scene name
    }

    IEnumerator LoadEndScreenAfterDelay(string sceneName)
    {
        // Wait for the specified delay, during which the click sound can play
        yield return new WaitForSeconds(delay);

        // Load the scene
        SceneManager.LoadScene(sceneName);
    }
}
