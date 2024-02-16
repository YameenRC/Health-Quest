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
        FinishLine = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            FinishLine.Play();
            levelCompleted = true;
            Invoke("LevelComplete", 1.5f);
        }   
    }

    private void LevelComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
