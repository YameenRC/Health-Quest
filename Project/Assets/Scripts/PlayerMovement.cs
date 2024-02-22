using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float xDir = 0f;
    private SpriteRenderer sprite;
    private BoxCollider2D col;

    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 10f;
    private enum Movement { still,running,jump,fall}

    [SerializeField] private LayerMask JumpFromGround;
    [SerializeField] private AudioSource Jumpingsfx;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        xDir = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xDir * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && CheckIfGrounded())
        {
            Jumpingsfx.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnim();

    }

    private void UpdateAnim()
    {
        Movement mov_state;

        if (xDir > 0)
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

        if (rb.velocity.y > .1f) 
        {
            mov_state = Movement.jump;
        }
        else if(rb.velocity.y < -.1f) 
        {
            mov_state = Movement.fall;
        }

        animator.SetInteger("State",(int) mov_state);
    }

    private bool CheckIfGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, JumpFromGround);
    }

    public void SetPowerUp(bool isPoweredUp)
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
