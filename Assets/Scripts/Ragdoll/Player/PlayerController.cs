using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(PlayerPickup))]
public class PlayerController : MonoBehaviour
{
    PlayerInput input;
    PlayerPickup pickup;
    AttackController attack;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        pickup = GetComponent<PlayerPickup>();
        attack = GetComponent<AttackController>();
    }

    void Update()
    {
        if (input.PickUpPressed)
        {
            pickup.TryPickOrDrop();
            input.ConsumePick();
        }

        if (input.AttackPressed)
        {
            attack.StartAttack(input.MouseWorld);
            // input.ConsumeAttack();
            Debug.Log("PLAYER ATTACK STARTED");
            
        }
    }
}


