using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.5f;

    public Rigidbody2D rb;
    Vector2 moveTarget;
    bool moving;

    // void Awake()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    // }

    public void SetMove(bool value)
    {
        moving = value;
    }

    public void SetTarget(Vector2 target)
    {
        moveTarget = target;
    }

    void FixedUpdate()
    {
        if (!moving)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        float dir = Mathf.Sign(moveTarget.x - rb.position.x);
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
    }
}
