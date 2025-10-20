using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // how fast the character moves
    public float jumpHeight; // how high the character jumps

    public KeyCode Spacebar; // Jump key (Space)
    public KeyCode L; // Move left key
    public KeyCode R; // Move right key

    public Transform groundCheck; // point used to detect if player is on the ground
    public float groundCheckRadius; // radius of ground check
    public LayerMask whatIsGround; // what is considered ground

    private bool grounded; // is player touching the ground
    private Animator anim; // reference to animator

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Update animation parameters
        anim.SetFloat("speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetFloat("height", GetComponent<Rigidbody2D>().velocity.y);
        anim.SetBool("Ground", grounded);

        // Jump
        if (Input.GetKeyDown(Spacebar) && grounded)
        {
            Jump();
        }

        // Move Left
        if (Input.GetKey(L))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (GetComponent<SpriteRenderer>() != null)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        // Move Right
        if (Input.GetKey(R))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (GetComponent<SpriteRenderer>() != null)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    void FixedUpdate()
    {
        // Check if the player is on the ground
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }
}
