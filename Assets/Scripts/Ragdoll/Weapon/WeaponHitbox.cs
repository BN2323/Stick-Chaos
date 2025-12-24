// using UnityEngine;

// public class WeaponHitbox : MonoBehaviour
// {
//     public int damage = 10;

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.GetComponentInParent<WeaponPickup>())
//             return;

//         Debug.Log("Hit: " + other.name);

//         // Later:
//         // other.GetComponent<Health>()?.TakeDamage(damage);
//     }
// }
