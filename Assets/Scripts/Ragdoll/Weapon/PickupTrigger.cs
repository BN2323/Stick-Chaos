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
            Debug.Log("Player entered pickup range");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerPickup picker =
            other.GetComponentInParent<PlayerPickup>();

        if (picker)
        {
            picker.ClearNearbyWeapon(weapon);
            Debug.Log("Player exited pickup range");
        }
    }

}
