using UnityEngine;

public class PlayerGrounding : MonoBehaviour
{
    public LayerMask groundLayer;
    public Vector2 groundCheckOffset = new Vector2(0, -0.8f);
    public float rayLength = 1.5f;

    public bool IsGrounded { get; private set; }
    public float GroundDistance { get; private set; }
    public Vector2 GroundNormal { get; private set; }

    void FixedUpdate()
    {
        CheckGround();
    }

    void CheckGround()
    {
        Vector2 origin = (Vector2)transform.position + groundCheckOffset;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            rayLength,
            groundLayer
        );

        if (hit.collider)
        {
            IsGrounded = true;
            GroundDistance = hit.distance;
            GroundNormal = hit.normal;
        }
        else
        {
            IsGrounded = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector2 origin = (Vector2)transform.position + groundCheckOffset;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + Vector2.down * rayLength);
    }
}
