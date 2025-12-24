using UnityEngine;

public class SelfCollisionIgnore : MonoBehaviour
{
    void Awake()
    {
        IgnoreSelfCollisions();
    }

    void IgnoreSelfCollisions()
    {
        Collider2D[] colliders =
            GetComponentsInChildren<Collider2D>();

        for (int i = 0; i < colliders.Length; i++)
        {
            for (int j = i + 1; j < colliders.Length; j++)
            {
                Physics2D.IgnoreCollision(
                    colliders[i],
                    colliders[j],
                    true
                );
            }
        }
    }
}
