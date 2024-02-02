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
        if (xDir > 0)
        {
            animator.SetBool("Running", true);
            sprite.flipX = false;
        }
        else if (xDir < 0)
        {
            animator.SetBool("Running", true);
            sprite.flipX = true;
        }
        else
        {
            animator.SetBool("Running", false);
        }

    }
}
