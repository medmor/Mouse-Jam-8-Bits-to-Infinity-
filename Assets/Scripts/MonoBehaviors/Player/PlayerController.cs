using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public PlayerDefinition PlayerDefinition;

    public LayerMask Ground;
    Rigidbody2D rg;

    Animator animator;

    public bool IsGrounded { get; private set; }

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (rg.velocity.x < PlayerDefinition.MinXVelocity)
            rg.velocity = new Vector2(PlayerDefinition.MinXVelocity, rg.velocity.y);
        if (rg.velocity.x > PlayerDefinition.MaxXVelocity)
            rg.velocity = new Vector2(PlayerDefinition.MaxXVelocity, rg.velocity.y);
        if (Physics2D.OverlapCircle(transform.position, .6f, Ground))
        {
            IsGrounded = true;
            animator.SetBool("Jump", false);
            rg.velocity -= Vector2.right * .01f;
            if (rg.rotation > 20)
            {
                rg.rotation = 20;
                rg.rotation -= 5;
            }
            else if (rg.rotation < -15)
            {
                rg.rotation = -15;
                rg.rotation += 5;
            }
        }
        else
        {
            IsGrounded = false;
            animator.SetBool("Jump", true);
            rg.rotation = 0;
        }
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (Input.GetMouseButtonDown(0) && IsGrounded)
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Slide();
        }
    }
    void Jump()
    {
        rg.velocity += Vector2.up * PlayerDefinition.JumpVelocity;
        SoundManager.Instance.PlayEffects("Jump");
    }
    void Slide()
    {
        //animator.SetTrigger("Slide");
        rg.velocity = new Vector2(rg.velocity.x - 10, rg.velocity.y - 20);
        SoundManager.Instance.PlayEffects("Laser");
    }
}

