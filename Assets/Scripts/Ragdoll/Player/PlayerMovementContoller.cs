using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementContoller : MonoBehaviour
{
    public PlayerInput input;
    public PlayerGrounding grounding;

    public float moveSpeed = 6f;
    public float acceleration = 20f;
    public float jumpVelocity = 8f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    float coyoteTimer;
    float jumpBufferTimer;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (grounding.IsGrounded)
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.deltaTime;

        if (input.JumpPressed)
            jumpBufferTimer = jumpBufferTime;
        else
            jumpBufferTimer -= Time.deltaTime;

        if (jumpBufferTimer > 0 && coyoteTimer > 0)
        {
            Jump();
            jumpBufferTimer = 0;
        }
    }


    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float targetSpeed = input.Move * moveSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;

        rb.AddForce(Vector2.right * speedDiff * acceleration);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
    }
}
