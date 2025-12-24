using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform handSocket;

    WeaponPickup nearbyWeapon;
    WeaponPickup heldWeapon;

    public void SetNearbyWeapon(WeaponPickup weapon)
    {
        if (weapon.IsHeld) return;
        nearbyWeapon = weapon;
        weapon.ShowText(true);
        Debug.Log("True");

    }

    public void ClearNearbyWeapon(WeaponPickup weapon)
    {
        if (nearbyWeapon != weapon) return;
        weapon.ShowText(false);
        Debug.Log("True");
        nearbyWeapon = null;
    }

    public void TryPickOrDrop()
    {
        if (heldWeapon)
        {
            heldWeapon.Drop();
            heldWeapon = null;
        }
        else if (nearbyWeapon)
        {
            nearbyWeapon.PickUp(handSocket);
            heldWeapon = nearbyWeapon;
        }
    }
}
