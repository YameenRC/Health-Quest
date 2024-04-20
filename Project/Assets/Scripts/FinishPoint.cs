using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    private AudioSource FinishLine;
    private bool levelCompleted;
    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to the same GameObject.
        FinishLine = GetComponent<AudioSource>();
    }

    // OnTriggerEnter2D is called when another object enters a trigger collider attached to this object.
    // In this context, it handles the logic for when the player reaches the finish point.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player and the level has not yet been marked as completed.
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            FinishLine.Play();
            levelCompleted = true; // Set the level completed flag to true to prevent repeated level completion actions.
            Invoke("LevelComplete", 1.5f); // Call the LevelComplete method after a delay of 1.5 seconds.
        }   
    }

    private void LevelComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene in the build order, effectively moving to the next level.
    }
}
