using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public AttackController attack;
    public Transform player;
    public float attackRange = 2.5f;

    // void Update()
    // {
    //     if (!attack.IsAttacking &&
    //         Vector2.Distance(transform.position, player.position) < attackRange)
    //     {
    //         attack.Attack(player.position);
    //     }
    // }
}
