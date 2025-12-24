using UnityEngine;

public static class IgnoreWeaponOwnerCollision
{
    public static void Apply(
        Collider2D weapon,
        Collider2D[] owner
    )
    {
        foreach (var c in owner)
        {
            Physics2D.IgnoreCollision(c, weapon, true);
        }
    }
}
