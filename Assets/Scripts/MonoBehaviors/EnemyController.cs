using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LayerMask Ground;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 1f, Ground))
        {

        }
        rb.MovePosition(Vector3.MoveTowards(transform.position, player.position, .03f));
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 5f);
    }
}
