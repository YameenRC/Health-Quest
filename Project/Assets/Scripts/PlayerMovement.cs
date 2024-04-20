using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D component for physics calculations.
    private Animator animator; // Animator component to control animations.
    private float xDir = 0f; // Horizontal direction, determined by player input.
    private SpriteRenderer sprite; // SpriteRenderer to modify the player's sprite properties like flipping.
    private BoxCollider2D col; // Collider component used to check if the player is grounded.

    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 10f;
    private enum Movement { still,running,jump,fall } // Enum to track different movement states.

    [SerializeField] private LayerMask JumpFromGround;  // LayerMask to determine what objects the player can jump from.
    [SerializeField] private AudioSource Jumpingsfx;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame.
    // Handles input and movement logic.
    void Update()
    {
        xDir = Input.GetAxisRaw("Horizontal"); // Get horizontal input from the player.
        rb.velocity = new Vector2(xDir * speed, rb.velocity.y); // Apply movement based on input.

        if (Input.GetButtonDown("Jump") && CheckIfGrounded()) // Handle jump input if the player is grounded.
        {
            Jumpingsfx.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnim(); // Update the player's animation state.

    }

    private void UpdateAnim()
    {
        Movement mov_state;

        if (xDir > 0) // Determine running direction and set sprite flip.
        {
            mov_state = Movement.running;
            sprite.flipX = false;
        }
        else if (xDir < 0)
        {
            mov_state = Movement.running;
            sprite.flipX = true;
        }
        else
        {
            mov_state = Movement.still;
        }

        if (rb.velocity.y > .1f) // Check for jumping or falling to update vertical animation.
        {
            mov_state = Movement.jump;
        }
        else if(rb.velocity.y < -.1f) 
        {
            mov_state = Movement.fall;
        }

        animator.SetInteger("State",(int) mov_state); // Set the animation state.
    }

    private bool CheckIfGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, JumpFromGround); // Checks if the player is grounded using a BoxCast below the player.
    }

    public void SetPowerUp(bool isPoweredUp) // Sets or resets power-up effects on the player.
    {
        if (isPoweredUp)
        {
            speed *= 1.5f; // Double the speed
            jumpForce *= 1.5f; // Increase jump force
        }
        else
        {
            // Reset the speed and jump force to their original values
            speed /= 1.5f;
            jumpForce /= 1.5f;
        }
    }
}
