using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Target")]
    public Rigidbody2D targetRb;   // Rigidbody of player to follow

    [Header("Follow Settings")]
    public Vector3 offset = new Vector3(0, 0, -10); // Camera offset
    public float followSpeed = 5f; // Smoothness

    void LateUpdate()
    {
        if (targetRb == null) return;

        // Follow player’s Rigidbody position (world space)
        Vector3 targetPosition = new(targetRb.position.x, transform.position.y, 0);
        Vector3 desiredPos = targetPosition + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);
    }
}
