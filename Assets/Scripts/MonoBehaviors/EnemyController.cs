using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LayerMask Ground;
    public ParticleSystem ExplosionEffect;
    private Transform player;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        ExplosionEffect = Instantiate(ExplosionEffect.gameObject).GetComponent<ParticleSystem>();
    }
    public void FollowPlayer()
    {
        StartCoroutine(followPlayer());
    }
    IEnumerator followPlayer()
    {
        while (gameObject.activeSelf)
        {

            rb.MovePosition(Vector3.MoveTowards(transform.position, player.position, .03f));

            yield return new WaitForFixedUpdate();

        }
    }

    public void Explode()
    {
        ExplosionEffect.transform.position = transform.position;
        ExplosionEffect.Play();
        gameObject.SetActive(false);
    }
}
