using UnityEngine;

[System.Serializable]
public class ArmRig
{
    public HingeJoint2D shoulder;
    public HingeJoint2D elbow;
    public Rigidbody2D forearmRb;
}
