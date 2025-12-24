using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject pickupText;
    public Collider2D pickupTrigger;

    bool held;

    Rigidbody2D rb;
    Collider2D physicalCollider;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicalCollider = GetComponent<Collider2D>();
        pickupText.SetActive(false);
    }

    public bool IsHeld => held;

    public void ShowText(bool show)
    {
        if (held) return;
        pickupText.SetActive(show);
    }

    public void PickUp(Transform handSocket)
    {
        if (held) return;
        held = true;

        pickupText.SetActive(false);
        pickupTrigger.enabled = false;

        rb.simulated = false;
        physicalCollider.enabled = false;

        Vector3 worldScale = transform.lossyScale;

        transform.SetParent(handSocket);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // RESTORE SCALE RELATIVE TO PARENT
        Vector3 parentScale = handSocket.lossyScale;
        transform.localScale = new Vector3(
            worldScale.x / parentScale.x,
            worldScale.y / parentScale.y,
            worldScale.z / parentScale.z
        );
        Debug.Log("pick me!");
    }


    public void Drop()
    {
        if (!held) return;
        held = false;

        transform.SetParent(null);

        rb.simulated = true;
        physicalCollider.enabled = true;
        pickupTrigger.enabled = true;
    }
}
