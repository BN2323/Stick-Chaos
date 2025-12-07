using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    public Animator anim;

    [Header("Movement")]
    public float moveForce = 15f;
    public float maxSpeed = 2f;
    public float minActionTime = 1f;
    public float maxActionTime = 4f;

    Rigidbody2D rb;
    bool movingRight = true;
    bool isWalking = false;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseNewAction();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
            ChooseNewAction();
    }

    void FixedUpdate()
    {
        if(isWalking)
        {
            float dir = movingRight ? 1f : -1f;

            rb.AddForce(new Vector2(dir * moveForce, 0));

            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
        }
    }

    void ChooseNewAction()
    {
        timer = Random.Range(minActionTime, maxActionTime);

        int choice = Random.Range(0, 3); 

        switch(choice)
        {
            case 0:
                isWalking = false;
                anim.Play("Idle");
                break;

            case 1:
                isWalking = true;
                movingRight = true;
                PlayWalk();
                break;

            case 2:
                isWalking = true;
                movingRight = false;
                PlayWalk();
                break;
        }

        FlipIfNeeded();
    }

    void PlayWalk()
    {
        anim.Play(movingRight ? "walk" : "walkBack");
    }

    void FlipIfNeeded()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (movingRight ? 1 : -1);
        transform.localScale = scale;
    }
}
