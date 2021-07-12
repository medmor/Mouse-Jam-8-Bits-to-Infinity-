using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerDefinition PlayerDefinition;

    public LayerMask ThornsGround;

    private bool canHit = true;
    private Animator animator;

    private PlayerController pController;

    private float currentThornX = float.NegativeInfinity;
    private int cummulativeScore = 0;
    private int score = 0;

    private int coins = 0;
    private float health = 100;
    private void Start()
    {
        pController = GetComponent<PlayerController>();

        animator = GetComponent<Animator>();

        PlayerDefinition.Shield.Unequipe();
        PlayerDefinition.Magnet.Unequipe();
    }
    private void Update()
    {
        if (!pController.IsGrounded)
        {
            var hit = Physics2D.Raycast(transform.position, -Vector2.up, 20f, ThornsGround);
            if (hit && hit.collider.gameObject.CompareTag("Thorns"))
            {
                if (currentThornX == float.NegativeInfinity)
                    currentThornX = hit.collider.transform.position.x;
            }
            else
            {
                if (currentThornX != float.NegativeInfinity)
                {
                    cummulativeScore++;
                    SetScore(score + 1);
                    currentThornX = float.NegativeInfinity;
                }
            }
        }
        else
        {
            if (cummulativeScore > 1)
            {
                if (PlayerDefinition.MinXVelocity < 15)
                    PlayerDefinition.MinXVelocity += .1f;
                else
                    PlayerDefinition.MinXVelocity += .05f;
                SetScore(score + cummulativeScore * 10);
                UIManager.Instance.BonusScore.Show("+ " + cummulativeScore * 10);
                //if (health < 100)
                //{
                //    SetHealth(health + cummulativeScore);
                //    SoundManager.Instance.PlayEffects("Power_up");
                //}
                if (cummulativeScore == 2)
                    SoundManager.Instance.PlayEffects("Double");
                else if (cummulativeScore == 3)
                    SoundManager.Instance.PlayEffects("Triple");
                else if (cummulativeScore == 4)
                    SoundManager.Instance.PlayEffects("Quad");
                else
                    SoundManager.Instance.PlayEffects("Extra");
            }
            cummulativeScore = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType() == typeof(CircleCollider2D))
        {

            if (collision.gameObject.CompareTag("Thorns"))
            {
                OnThornHit();
            }
            else if (collision.gameObject.CompareTag("Coin"))
            {
                OnCoinHit(collision.gameObject);
            }
        }
    }
    void OnThornHit()
    {
        if (canHit && !PlayerDefinition.Shield.IsActif)
        {
            canHit = false;
            currentThornX = float.NegativeInfinity;
            cummulativeScore = 0;
            transform.position += Vector3.up * 3;
            StartCoroutine(ResetCanHit(1));
            animator.SetTrigger("Flicker");
            SetHealth(health - 50);
            SoundManager.Instance.PlayEffects("Hit");
            if (health <= 0)
            {
                GameManager.Instance.PlayerKilled.Invoke();
                OnPlayerKiled();
            }
        }
    }
    void OnCoinHit(GameObject coinObject)
    {
        coinObject.SetActive(false);
        SetCoins(coins + 1);
    }
    void SetHealth(float h)
    {
        health = h;
        UIManager.Instance.Inventory.SetHealthBar(health / 100);
    }
    void SetCoins(int c)
    {
        coins = c;
        UIManager.Instance.Inventory.SetCoin(coins);
        SoundManager.Instance.PlayEffects("Coin");

    }
    void SetScore(int s)
    {
        score = s;
        UIManager.Instance.Inventory.SetScore(score);
    }
    public void ResetPlayer()
    {
        gameObject.SetActive(true);
        SetScore(0);
        SetHealth(100);
        SetCoins(0);
        canHit = true;
        PlayerDefinition.Shield.Unequipe();
        PlayerDefinition.Magnet.Unequipe();
        pController.PlayerDefinition.MinXVelocity = pController.PlayerDefinition.StartMinVelocity;
    }

    void OnPlayerKiled()
    {
        UIManager.Instance.TimerUI.Stop();
        SoundManager.Instance.TogglePauseMusic();
        GameManager.Instance.CalculatScore(coins, score);
        UIManager.Instance.EndUI.Show();
        UIManager.Instance.PauseButton.Hide();
        Destroy(gameObject);
        //gameObject.SetActive(false);
        //Stope();
        //GameManager.Instance.UpdateState(GameManager.GameStates.PAUSED);
        //UIManager.Instance.Inventory.Hide();
        //UIManager.Instance.TimerUI.Hide();
        //UIManager.Instance.BonusScore.Hide();
    }
    IEnumerator ResetCanHit(float s)
    {
        yield return new WaitForSeconds(s);
        canHit = true;
    }
}
