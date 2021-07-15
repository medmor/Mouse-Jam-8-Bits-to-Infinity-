using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerDefinition PlayerDefinition;
    public LayerMask EnemiesGround;
    public GameObject AmmoPref;

    private List<GameObject> AmmoPool = new List<GameObject>();

    private bool canHit = true;
    private Animator animator;

    private PlayerController pController;

    //private float currentEnemyX = float.NegativeInfinity;
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

        if (pController.IsGrounded)
        {
            if (cummulativeScore > 1)
            {
                if (pController.MinXVelocity < 10)
                    pController.MinXVelocity += .1f;
                else
                    pController.MinXVelocity += .05f;
                SetScore(score + cummulativeScore * 10);
                UIManager.Instance.BonusScore.Show("+ " + cummulativeScore * 10);

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
        if (Input.mouseScrollDelta.y > 0)
        //if (Input.GetMouseButtonDown(2))
        {
            var ammo = GetAmmo();
            ammo.transform.position = transform.position;
            ammo.GetComponent<Ammo>().Fire(transform);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType() == typeof(CircleCollider2D))
        {
            if (collision.gameObject.CompareTag("Coin"))
            {
                OnCoinHit(collision.gameObject);
            }
        }
        else if (collision.GetType() == typeof(BoxCollider2D))
        {
            if (collision.gameObject.CompareTag("Enemy")
                && Vector3.Dot(-transform.up,
                collision.transform.position - transform.position) < 0)
            {
                OnEnemyHit(collision.gameObject.GetComponent<Enemy>());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            cummulativeScore++;
            SetScore(score + 1);
            pController.rg.velocity = Vector2.up * PlayerDefinition.JumpVelocity;
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.DecrementHealth(10))
                enemy.Explode();

        }

    }
    void OnEnemyHit(Enemy enemy)
    {
        if (PlayerDefinition.Shield.IsActif)
        {
            if (enemy.DecrementHealth(10))
                enemy.Explode();
        }
        else if (canHit)
        {
            canHit = false;
            transform.position += Vector3.up * 3;
            StartCoroutine(ResetCanHit(2));
            animator.SetTrigger("Flicker");
            SetHealth(health - 10);
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
    void OnPlayerKiled()
    {
        UIManager.Instance.TimerUI.Stop();
        SoundManager.Instance.TogglePauseMusic();
        GameManager.Instance.CalculatScore(coins, score);
        UIManager.Instance.EndUI.Show();
        UIManager.Instance.PauseButton.Hide();
        Destroy(gameObject);
    }
    GameObject GetAmmo()
    {
        foreach (var a in AmmoPool)
        {
            if (!a.activeSelf)
            {
                a.SetActive(true);
                return a;
            }
        }
        FillAmmoPool();
        return GetAmmo();
    }
    void FillAmmoPool()
    {
        for (var i = 0; i < 10; i++)
        {
            AmmoPool.Add(Instantiate(AmmoPref));
            AmmoPool[i].SetActive(false);
        }
    }
    IEnumerator ResetCanHit(float s)
    {
        yield return new WaitForSeconds(s);
        canHit = true;
    }
}
//public void ResetPlayer()
//{
//    gameObject.SetActive(true);
//    SetScore(0);
//    SetHealth(100);
//    SetCoins(0);
//    canHit = true;
//    PlayerDefinition.Shield.Unequipe();
//    PlayerDefinition.Magnet.Unequipe();
//    pController.PlayerDefinition.MinXVelocity = pController.PlayerDefinition.StartMinVelocity;
//}

//if (!pController.IsGrounded)
//{
//    var hit = Physics2D.Raycast(transform.position, -Vector2.up, 20f, EnemiesGround);
//    if (hit && hit.collider.gameObject.CompareTag("Enemy"))
//    {
//        if (currentEnemyX == float.NegativeInfinity)
//            currentEnemyX = hit.collider.transform.position.x;
//    }
//    else
//    {
//        if (currentEnemyX != float.NegativeInfinity)
//        {
//            cummulativeScore++;
//            SetScore(score + 1);
//            currentEnemyX = float.NegativeInfinity;
//        }
//    }
//}
//else
//{
//    if (cummulativeScore > 1)
//    {
//        if (PlayerDefinition.MinXVelocity < 15)
//            PlayerDefinition.MinXVelocity += .1f;
//        else
//            PlayerDefinition.MinXVelocity += .05f;
//        SetScore(score + cummulativeScore * 10);
//        UIManager.Instance.BonusScore.Show("+ " + cummulativeScore * 10);

//        if (cummulativeScore == 2)
//            SoundManager.Instance.PlayEffects("Double");
//        else if (cummulativeScore == 3)
//            SoundManager.Instance.PlayEffects("Triple");
//        else if (cummulativeScore == 4)
//            SoundManager.Instance.PlayEffects("Quad");
//        else
//            SoundManager.Instance.PlayEffects("Extra");
//    }
//    cummulativeScore = 0;
//}