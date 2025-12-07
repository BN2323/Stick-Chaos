using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public Animator anim;
    public float moveForce = 15f;
    public float maxSpeed = 6f;

    [Header("Jump")]
    public float jumpForce = 8f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;

    [Header("Animation (PLACEHOLDERS)")]
    public bool isWalking;
    public bool isJumping;

    private Rigidbody2D rb;
    private float moveInput;
    private bool grounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        grounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }

        isWalking = Mathf.Abs(moveInput) > 0.1f;
        isJumping = !grounded;

        if (isWalking && moveInput > 0)
            anim.Play("walk");
        else if (isWalking && moveInput < 0)
            anim.Play("walkBack");
        else
            anim.Play("Idle");
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(moveInput * moveForce, 0));

        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed),
            rb.velocity.y
        );
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
