using UnityEngine;

public class IgnoreSelfCollision : MonoBehaviour
{
    void Start()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

        for (int i = 0; i < colliders.Length; i++)
        {
            for (int j = i + 1; j < colliders.Length; j++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[j]);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D thisCollider = GetComponent<Collider2D>();
            if (thisCollider != null)
            {
                Collider2D otherCollider = collision.collider;
                Physics2D.IgnoreCollision(thisCollider, otherCollider);
            }
        }
    }
}
