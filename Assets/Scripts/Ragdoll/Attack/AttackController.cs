using UnityEngine;

public class AttackController : MonoBehaviour
{
    [System.Serializable]
    public class JointMotorSettings
    {
        public float strength = 10f;
        public float damping = 5f;
        public float maxTorque = 800f;
    }

    [Header("Joints")]
    public HingeJoint2D shoulder;
    public HingeJoint2D elbow;

    [Header("Motor Settings")]
    public JointMotorSettings shoulderMotor;
    public JointMotorSettings elbowMotor;

    [Header("Attack")]
    public float attackDuration = 0.18f;
    public float elbowExtendAngle = -25f;
    public float elbowDelay = 0.03f;

    bool attacking;
    float timer;

    float shoulderTarget;
    float elbowTarget;

    // ================================
    // PUBLIC ENTRY POINT (Player / AI)
    // ================================
    public void StartAttack(Vector2 targetWorld)
    {
        if (attacking) return;

        attacking = true;
        timer = attackDuration;

        // -------- SHOULDER AIM --------
        Vector2 dir =
            targetWorld - (Vector2)shoulder.transform.position;

        float worldAngle =
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float baseAngle = shoulder.connectedBody
            ? shoulder.connectedBody.rotation
            : 0f;

        shoulderTarget =
            Mathf.DeltaAngle(baseAngle, worldAngle);

        shoulderTarget =
            ClampToLimits(shoulder, shoulderTarget);

        // -------- ELBOW EXTEND --------
        elbowTarget = elbowExtendAngle;
    }

    void FixedUpdate()
    {
        if (!attacking)
        {
            DisableMotor(shoulder);
            DisableMotor(elbow);
            return;
        }

        timer -= Time.fixedDeltaTime;

        if (timer <= 0f)
        {
            attacking = false;
            return;
        }

        float elapsed = attackDuration - timer;

        // Shoulder always leads
        DriveJoint(
            shoulder,
            shoulderTarget,
            shoulderMotor
        );

        // Elbow follows slightly later
        if (elapsed > elbowDelay)
        {
            DriveJoint(
                elbow,
                elbowTarget,
                elbowMotor
            );
        }
    }

    // ================================
    // MOTOR DRIVE (PD CONTROLLER)
    // ================================
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

    float ClampToLimits(
        HingeJoint2D joint,
        float angle
    )
    {
        JointAngleLimits2D limits = joint.limits;
        return Mathf.Clamp(angle, limits.min, limits.max);
    }
}
