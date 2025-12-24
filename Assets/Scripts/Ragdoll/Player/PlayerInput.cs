using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Move { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool PickUpPressed { get; private set; }
    public bool AttackPressed { get; private set; }

    public Vector2 MouseWorld { get; private set; }

    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
        PickUpPressed = Input.GetKeyDown(KeyCode.E);
        AttackPressed = Input.GetMouseButtonDown(0);

        MouseWorld =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void ConsumeJump() => JumpPressed = false;
    public void ConsumePick() => PickUpPressed = false;
    public void ConsumeAttack() => AttackPressed = false;
}
