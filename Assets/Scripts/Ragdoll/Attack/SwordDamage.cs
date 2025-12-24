using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public float baseDamage = 10f;

    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = col.relativeVelocity.magnitude;

        if (impact < 2f) return;

        // Health hp = col.collider.GetComponentInParent<Health>();
        // if (!hp) return;

        // hp.TakeDamage(baseDamage * impact);
    }
}
