using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    WeaponPickup weapon;

    void Awake()
    {
        weapon = GetComponentInParent<WeaponPickup>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPickup picker =
            other.GetComponentInParent<PlayerPickup>();

        if (picker)
        {
            picker.SetNearbyWeapon(weapon);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerPickup picker =
            other.GetComponentInParent<PlayerPickup>();

        if (picker)
        {
            picker.ClearNearbyWeapon(weapon);
        }
    }

}
