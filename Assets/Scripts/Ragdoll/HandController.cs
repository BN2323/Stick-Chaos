using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class HandFollowMouse : MonoBehaviour
{
    public float speed = 300f;
    private Camera cam;
    private Rigidbody2D rb;
    public KeyCode mouseButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = new(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 different = mousePos - transform.position;

        float rotationZ = Mathf.Atan2(different.x, -different.y) * Mathf.Rad2Deg;
        
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.fixedDeltaTime));
    }

}
