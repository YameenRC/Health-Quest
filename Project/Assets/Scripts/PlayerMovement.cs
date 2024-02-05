using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float xDir = 0f;
    private SpriteRenderer sprite;

    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 10f;

    private enum Movement { still,running,jump,fall}


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        xDir = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xDir * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
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
}
