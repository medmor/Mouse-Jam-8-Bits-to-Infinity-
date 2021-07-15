using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public PlayerDefinition PlayerDefinition;

    public float MinXVelocity { get; set; }
    public float MaxXVelocity { get; set; }

    public LayerMask Ground;
    internal Rigidbody2D rg;

    Animator animator;
    private bool spinning = false;

    public bool IsGrounded { get; private set; }

    void Start()
    {
        MinXVelocity = PlayerDefinition.MinXVelocity;
        MaxXVelocity = PlayerDefinition.MaxXVelocity;
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (rg.velocity.x < MinXVelocity)
            rg.velocity = new Vector2(MinXVelocity, rg.velocity.y);
        if (rg.velocity.x > MaxXVelocity)
            rg.velocity = new Vector2(MaxXVelocity, rg.velocity.y);

        if (spinning)
        {
            rg.velocity = new Vector2(MaxXVelocity * 2, rg.velocity.y);
            StartCoroutine(ResetSpinning());
        }
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
            if (Input.GetMouseButtonDown(2))
                spinning = true;
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
        if (rg.velocity.y > PlayerDefinition.JumpVelocity)
            rg.velocity = new Vector2(100, PlayerDefinition.JumpVelocity);
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
    IEnumerator ResetSpinning()
    {
        yield return new WaitForSeconds(.1f);
        spinning = false;
    }

}

