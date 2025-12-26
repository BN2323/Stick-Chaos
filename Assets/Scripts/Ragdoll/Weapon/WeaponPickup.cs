using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject pickupText;
    public Collider2D pickupTrigger;
    public WeaponDamage weapon;

    bool held;

    Rigidbody2D rb;
    Collider2D physicalCollider;

    // void Awake()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     physicalCollider = GetComponent<Collider2D>();
    //     pickupText.SetActive(false);
    //     weapon = GetComponent<WeaponDamage>();
    // }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicalCollider = GetComponent<Collider2D>();
        pickupText.SetActive(false);

        weapon = GetComponent<WeaponDamage>();
        Debug.Log($"[WeaponPickup] {name} same object WeaponDamage = {weapon}");

        if (!weapon)
        {
            weapon = GetComponentInChildren<WeaponDamage>();
            Debug.Log($"[WeaponPickup] {name} child WeaponDamage = {weapon}");
        }

        if (!weapon)
        {
            Debug.LogError($"[WeaponPickup] {name} NO WeaponDamage FOUND ANYWHERE");
        }
    }


    public bool IsHeld => held;

    public void ShowText(bool show)
    {
        if (held) return;
        pickupText.SetActive(show);
    }

    public void PickUp(Transform handSocket, Health owner)
    {
        if (held) return;
        held = true;
        weapon.SetOwner(owner);

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
        held = false;

        if (weapon)
            weapon.SetOwner(null); // 🔒 important

        transform.SetParent(null);
        rb.simulated = true;
        physicalCollider.enabled = true;
    }

}
