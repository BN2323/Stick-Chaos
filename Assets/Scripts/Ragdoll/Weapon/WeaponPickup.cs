using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject pickupText;
    public Collider2D pickupTrigger;
    public WeaponDamage weapon;

    bool held;

    Rigidbody2D rb;
    Collider2D physicalCollider;

    void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        physicalCollider = GetComponent<Collider2D>();
        pickupText.SetActive(false);

         if (!weapon) 
            weapon = GetComponent<WeaponDamage>();
        if (!weapon)
            weapon = GetComponentInChildren<WeaponDamage>();

        if (!weapon)
            Debug.LogError($"[WeaponPickup] {name} NO WeaponDamage FOUND ANYWHERE");
        else
            Debug.Log($"[WeaponPickup] {name} WeaponDamage found: {weapon}");
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

        rb.bodyType = RigidbodyType2D.Kinematic;
        physicalCollider.isTrigger = true;
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
    }


    public void Drop()
    {
        held = false;

        if (weapon)
            weapon.SetOwner(null);
        
        rb.bodyType = RigidbodyType2D.Dynamic;
        
        pickupTrigger.enabled = true;
        transform.SetParent(null);
        physicalCollider.enabled = true;
        physicalCollider.isTrigger = false;
    }

    public WeaponDamage GetWeapon()
    {
        if (weapon == null)
            weapon = GetComponent<WeaponDamage>() ?? GetComponentInChildren<WeaponDamage>();
        return weapon;
    }


}
