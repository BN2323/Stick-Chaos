using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform handSocket;

    WeaponPickup nearbyWeapon;
    WeaponPickup heldWeapon;
    public AttackController attack;

    void Awake()
    {
        attack = GetComponent<AttackController>();
    }

    public void SetNearbyWeapon(WeaponPickup weapon)
    {
        if (weapon.IsHeld) return;
        nearbyWeapon = weapon;
        weapon.ShowText(true);

    }

    public void ClearNearbyWeapon(WeaponPickup weapon)
    {
        if (nearbyWeapon != weapon) return;
        weapon.ShowText(false);
        nearbyWeapon = null;
    }

    public void TryPickOrDrop()
    {
        if (heldWeapon)
        {
            heldWeapon.Drop();
            heldWeapon = null;
            attack.EquipWeapon(null);
            return;
        }

        if (!nearbyWeapon) return;
        Health myHealth = GetComponentInParent<Health>();
        nearbyWeapon.PickUp(handSocket, myHealth);
        heldWeapon = nearbyWeapon;
    }

}
