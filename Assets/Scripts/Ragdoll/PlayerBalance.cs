using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBalance : MonoBehaviour
{
    public PlayerGrounding grounding;

    public float targetHeight = 1f;
    public float stiffness = 50f;
    public float damping = 8f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!grounding.IsGrounded) return;

        float error = targetHeight - grounding.GroundDistance;
        float spring = error * stiffness;
        float damper = -rb.velocity.y * damping;

        rb.AddForce(Vector2.up * (spring + damper));
    }
}
