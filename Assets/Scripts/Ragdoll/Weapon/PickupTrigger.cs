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
         Debug.Log($"The Weapon is set:  {weapon.GetWeapon().name}");

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
