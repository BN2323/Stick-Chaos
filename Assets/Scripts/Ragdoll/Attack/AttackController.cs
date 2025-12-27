using UnityEngine;

public class AttackController : MonoBehaviour
{
    public enum AttackPhase
    {
        Idle,
        Windup,
        Strike,
        Recover
    }

    [System.Serializable]
    public class JointMotorSettings
    {
        public float strength = 8f;
        public float damping = 6f;
        public float maxTorque = 600f;
    }

    [Header("Weapon")]
    public WeaponDamage weapon;

    [Header("Joints")]
    public HingeJoint2D shoulder;
    public HingeJoint2D elbow;

    [Header("Motor Settings")]
    public JointMotorSettings shoulderMotor;
    public JointMotorSettings elbowMotor;

    [Header("Attack Timings")]
    public float windupTime = 0.08f;
    public float strikeTime = 0.12f;
    public float recoverTime = 0.18f;

    [Header("Angles")]
    public float elbowStrikeAngle = -35f;
    public float elbowRestAngle = 0f;

    AttackPhase phase = AttackPhase.Idle;
    float phaseTimer;

    float shoulderTarget;
    float elbowTarget;

    // ============================
    // PUBLIC ENTRY (Player / AI)
    // ============================
    public void StartAttack(Vector2 targetWorld)
    {
        if (phase != AttackPhase.Idle)
            return;

        // -------- Shoulder aim --------
        Vector2 dir =
            targetWorld - (Vector2)shoulder.transform.position;

        float worldAngle =
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float baseAngle =
            shoulder.connectedBody
            ? shoulder.connectedBody.rotation
            : 0f;

        shoulderTarget =
            Mathf.DeltaAngle(baseAngle, worldAngle);

        shoulderTarget =
            ClampToLimits(shoulder, shoulderTarget);

        // -------- Start windup --------
        elbowTarget = elbowRestAngle;
        phase = AttackPhase.Windup;
        phaseTimer = windupTime;
    }

    void FixedUpdate()
    {
        switch (phase)
        {
            case AttackPhase.Windup:
                DriveJoint(shoulder, shoulderTarget, shoulderMotor);
                DriveJoint(elbow, elbowRestAngle, elbowMotor);

                TickPhase(AttackPhase.Strike, strikeTime);
                break;

            case AttackPhase.Strike:
                if(weapon) Debug.Log("PLAYER ATTACK Strike");
                
                weapon?.BeginAttack();
                DriveJoint(shoulder, shoulderTarget, shoulderMotor);
                DriveJoint(elbow, elbowStrikeAngle, elbowMotor);

                TickPhase(AttackPhase.Recover, recoverTime);
                break;

            case AttackPhase.Recover:
                weapon?.EndAttack();
                DisableMotor(elbow);
                DisableMotor(shoulder);

                TickPhase(AttackPhase.Idle, 0f);
                break;
        }
    }

    // ============================
    // HELPERS
    // ============================
    void TickPhase(AttackPhase next, float nextTime)
    {
        phaseTimer -= Time.fixedDeltaTime;
        if (phaseTimer <= 0f)
        {
            phase = next;
            phaseTimer = nextTime;
        }
    }

    void DriveJoint(
        HingeJoint2D joint,
        float target,
        JointMotorSettings settings
    )
    {
        float current = joint.jointAngle;
        float error = Mathf.DeltaAngle(current, target);
        float angularVel =
            joint.attachedRigidbody.angularVelocity;

        float speed =
            error * settings.strength
            - angularVel * settings.damping;

        JointMotor2D motor = joint.motor;
        motor.motorSpeed = speed;
        motor.maxMotorTorque = settings.maxTorque;
        joint.motor = motor;
        joint.useMotor = true;
    }

    void DisableMotor(HingeJoint2D joint)
    {
        if (joint != null)
            joint.useMotor = false;
    }

    float ClampToLimits(HingeJoint2D joint, float angle)
    {
        JointAngleLimits2D limits = joint.limits;
        return Mathf.Clamp(angle, limits.min, limits.max);
    }

    public void EquipWeapon(WeaponDamage newWeapon)
    {
        if (!newWeapon)
        {
            Debug.LogError("EquipWeapon: newWeapon is null");
            return;
        }

        // Disable old weapon
        if (weapon != null)
            weapon.EndAttack();

        // Assign new weapon
        weapon = newWeapon;

        // Find owner health
        Health myHealth = GetComponentInParent<Health>();
        if (!myHealth)
        {
            Debug.LogError("EquipWeapon: Health not found on player");
            return;
        }

        weapon.SetOwner(myHealth);
    }


}
