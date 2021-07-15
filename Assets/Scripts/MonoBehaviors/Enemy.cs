using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyDefinition Definition;
    public ParticleSystem ExplosionEffect;

    private float speed;
    private float health;
    private Transform player;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        ResetEnemy();
        ExplosionEffect = Instantiate(ExplosionEffect.gameObject).GetComponent<ParticleSystem>();
    }
    public void FollowPlayer()
    {
        StartCoroutine(followPlayer());
    }
    IEnumerator followPlayer()
    {
        while (gameObject.activeSelf && player)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, player.position, speed));
            yield return new WaitForFixedUpdate();
        }
    }
    public void Explode()
    {
        SoundManager.Instance.PlayEffects("Explosion");
        ExplosionEffect.transform.position = transform.position;
        ExplosionEffect.Play();
        gameObject.SetActive(false);
        ResetEnemy();
    }
    public bool DecrementHealth(float amount)
    {
        health -= amount;
        return health <= 0;
    }
    void ResetEnemy()
    {
        health = Definition.Health;
        speed = Random.Range(Definition.MinSpeedLimit, Definition.MaxSpeedLimit);
    }
}
