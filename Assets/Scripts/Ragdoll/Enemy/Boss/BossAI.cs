using UnityEngine;

public class BossAI : MonoBehaviour
{
    enum AIState { Idle, Chase, Attack, Cooldown }
    AIState state;

    [Header("References")]
    public Transform bodyCenter;
    public BossTrigger bossTrigger; // Reference to your BossTrigger
    public float attackRange = 2.5f;
    public float attackCooldown = 1f;

    AttackController attack;
    EnemyMovement movement;

    Transform target;
    float cooldown;
    bool playerInArena;

    void Awake()
    {
        attack = GetComponent<AttackController>();
        movement = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) target = player.transform;
    }

    void Update()
    {
        if (!target || !bodyCenter)
        {
            SetState(AIState.Idle);
            return;
        }

        if (!playerInArena)
        {
            SetState(AIState.Idle);
            return; // Do nothing if player is outside the boss area
        }

        float dist = Vector2.Distance(bodyCenter.position, target.position);
        cooldown -= Time.deltaTime;

        // ---- DECISION ----
        if (dist > attackRange)
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
    }

    void SetState(AIState newState)
    {
        if (state == newState) return;
        state = newState;
    }

    // Called by the BossTrigger
    public void SetPlayerInArena(bool inArena)
    {
        playerInArena = inArena;
    }

    void OnDrawGizmosSelected()
    {
        if (!bodyCenter) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bodyCenter.position, attackRange);
    }
}
