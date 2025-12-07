using UnityEngine;

public class HandWeaponHold : MonoBehaviour
{
    public Transform handTip;
    public float grabRadius = 0.2f;
    public LayerMask weaponLayer, grabbableLayer;

    Rigidbody2D handRb;
    FixedJoint2D weaponJoint;
    FixedJoint2D grabJoint;

    Rigidbody2D grabbedRb;
    Transform weaponHandle;

    void Awake()
    {
        handRb = GetComponent<Rigidbody2D>();

        if (handTip == null)
        {
            GameObject tip = new GameObject("HandTip");
            tip.transform.SetParent(transform);
            tip.transform.localPosition = Vector3.right * 0.4f;
            handTip = tip.transform;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (weaponJoint == null && grabJoint == null)
            {
                if (!TryGrabWeapon())
                    TryGrabNormal();
            }
        }
        else
        {
            ReleaseAll();
        }
    }

    bool TryGrabWeapon()
    {
        Collider2D hit = Physics2D.OverlapCircle(handTip.position, grabRadius, weaponLayer);

        if (hit == null) return false;

        Rigidbody2D rb = hit.attachedRigidbody;
        if (rb == null) return false;

        weaponHandle = hit.transform.Find("Handle");

        weaponJoint = handRb.gameObject.AddComponent<FixedJoint2D>();
        weaponJoint.connectedBody = rb;
        weaponJoint.enableCollision = false;

        if (weaponHandle != null)
        {
            rb.position += (Vector2)(handTip.position - weaponHandle.position);
            weaponJoint.autoConfigureConnectedAnchor = false;
            weaponJoint.connectedAnchor = weaponHandle.localPosition;
        }
        else
        {
            weaponJoint.autoConfigureConnectedAnchor = true;
        }

        grabbedRb = rb;
        return true;
    }
    void TryGrabNormal()
    {
        Collider2D hit = Physics2D.OverlapCircle(handTip.position, grabRadius, grabbableLayer);

        if (hit == null) return;

        Rigidbody2D rb = hit.attachedRigidbody;
        if (rb == null) return;

        grabJoint = handRb.gameObject.AddComponent<FixedJoint2D>();
        grabJoint.connectedBody = rb;
        grabJoint.enableCollision = false;

        grabbedRb = rb;
    }

    void ReleaseAll()
    {
        if (weaponJoint != null) Destroy(weaponJoint);
        if (grabJoint != null) Destroy(grabJoint);

        grabbedRb = null;
        weaponHandle = null;
        weaponJoint = null;
        grabJoint = null;
    }

    void OnDrawGizmosSelected()
    {
        if (handTip != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(handTip.position, grabRadius);
        }
    }
}
