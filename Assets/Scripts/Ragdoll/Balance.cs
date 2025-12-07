using UnityEngine;

public class Balance : MonoBehaviour
{
    [Header("Settings")]
    public float targetRotation = 0f;
    public float force;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        float currentAngle = rb.rotation;
        float newAngle = Mathf.LerpAngle(currentAngle, targetRotation, force * Time.fixedDeltaTime);
        rb.MoveRotation(newAngle);
    }
}
