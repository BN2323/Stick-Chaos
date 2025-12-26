using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    enum AIState { Idle, Chase, Attack, Cooldown }
    AIState state;

    Transform target;

    [Header("References")]
    public Transform bodyCenter;

    [Header("Ranges")]
    public float detectRange = 6f;
    public float attackRange = 2.2f;

    [Header("Attack")]
    public float attackCooldown = 0.8f;

    AttackController attack;
    EnemyMovement movement;

    float cooldown;

    void Awake()
    {
        attack = GetComponent<AttackController>();
        movement = GetComponent<EnemyMovement>();
  
        Health myHealth = GetComponent<Health>();
        if (attack.weapon && myHealth)
        {
            attack.weapon.SetOwner(myHealth);
            Debug.Log("Jok jey hz bro");
        }
    }

    void Start()
    {
        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        if (player)
            target = player.transform;
        // else
            // Debug.LogError("EnemyAI: No Player found");
    }

    void Update()
    {
        if (!target || !bodyCenter)
        {
            SetState(AIState.Idle);
            return;
        }

        float dist = Vector2.Distance(
            bodyCenter.position,
            target.position
        );

        cooldown -= Time.deltaTime;

        // ---- DECISION ----
        if (dist > detectRange)
            SetState(AIState.Idle);
        else if (dist > attackRange)
            SetState(AIState.Chase);
        else if (cooldown <= 0f)
            SetState(AIState.Attack);
        else
            SetState(AIState.Cooldown);

        // ---- ACTION ----
        switch (state)
        {
            case AIState.Idle:
                movement.SetMove(false);
                break;

            case AIState.Chase:
                movement.SetMove(true);
                movement.SetTarget(target.position);
                break;

            case AIState.Attack:
                movement.SetMove(false);
                attack.StartAttack(target.position);
                cooldown = attackCooldown;
                break;

            case AIState.Cooldown:
                movement.SetMove(false);
                break;
        }

        // DEBUG DISTANCE
        // Debug.Log($"[EnemyAI] dist={dist:F2} state={state}");
    }

    void SetState(AIState newState)
    {
        if (state == newState) return;
        state = newState;
    }

    void OnDrawGizmosSelected()
    {
        if (!bodyCenter) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(bodyCenter.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bodyCenter.position, attackRange);
    }
}
