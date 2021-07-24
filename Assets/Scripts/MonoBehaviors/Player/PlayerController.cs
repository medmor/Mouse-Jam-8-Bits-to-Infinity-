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
        StartCoroutine(IncreaseSpeed());

        GameManager.Instance.MobileSwipEvent?.AddListener(HandleMobileSwip);
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
        }
        if (Physics2D.OverlapCircle(transform.position, .6f, Ground))
        {
            IsGrounded = true;
            rg.constraints = RigidbodyConstraints2D.None;
            if (animator.GetBool("Jump"))
                animator.SetBool("Jump", false);
            rg.velocity -= Vector2.right * .01f;
            if (rg.rotation > 20)
            {
                rg.rotation = 20;
                rg.rotation -= 5;
            }
            else if (rg.rotation < -15)
            {
                rg.rotation = -10;
                rg.rotation += 5;
            }

        }
        else
        {
            rg.constraints = RigidbodyConstraints2D.FreezeRotation;
            IsGrounded = false;
            animator.SetBool("Jump", true);
            if (Input.GetMouseButtonDown(1))
                Slide();
        }
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (Input.GetMouseButtonDown(0) && IsGrounded)
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(2))
        {
            Spin();
        }
        if (rg.velocity.y > PlayerDefinition.JumpVelocity)
            rg.velocity = new Vector2(rg.velocity.x, PlayerDefinition.JumpVelocity);
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
    void Spin()
    {
        if (!spinning && IsGrounded)
        {
            SoundManager.Instance.PlayEffects("Laser");
            spinning = true;
            StartCoroutine(ResetSpinning());
        }
    }
    IEnumerator ResetSpinning()
    {
        yield return new WaitForSeconds(.2f);
        spinning = false;
    }
    IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            if (MinXVelocity < 10)
                MinXVelocity += .1f;
            else
                MinXVelocity += .05f;
            yield return new WaitForSeconds(.5f);
        }
    }
    private void HandleMobileSwip(SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeDirection.Up:
                Jump();
                break;
            case SwipeDirection.Down:
                Slide();
                break;
            case SwipeDirection.Left:
                break;
            case SwipeDirection.Right:
                Spin();
                break;
        }
    }
}