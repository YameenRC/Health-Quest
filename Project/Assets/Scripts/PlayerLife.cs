using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D component for physics operations.
    private Animator animator; // Animator component to control animations.
    [SerializeField] private AudioSource DeathSound;
    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component from the same GameObject.
        rb = GetComponent<Rigidbody2D>();
        // Get the Animator component from the same GameObject.
        animator = GetComponent<Animator>();
    }
    // OnCollisionEnter2D is called when this collider/rigidbody has begun touching another rigidbody/collider.
    // In this method, it checks for collisions with traps.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps")) // Check if the collider we collided with has the tag "Traps".
        {
            PlayerDeath(); // If it is a trap, trigger the player's death process.
        }

    }

    public void PlayerDeath()
    {
        DeathSound.Play();  // Play the death sound effect.
        rb.bodyType = RigidbodyType2D.Static; // Change the Rigidbody's body type to Static, stopping all physical movement.
        animator.SetTrigger("Death");  // Trigger the "Death" animation.
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load the current scene again by using its name to refresh the game state.
    }
}
