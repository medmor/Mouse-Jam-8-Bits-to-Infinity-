using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    LayerMask ground;
    private void Start()
    {
        ground = LayerMask.GetMask("Ground");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player.PlayerDefinition.Magnet.IsActif && gameObject.activeSelf)
            {
                StartCoroutine(Follow(player.transform));
            }
        }
    }
    IEnumerator Follow(Transform player)
    {
        while (gameObject.activeSelf && player)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, .3f);
            yield return new WaitForFixedUpdate();
        }
    }
    public IEnumerator FallToGround()
    {
        while (!Physics2D.OverlapCircle(transform.position, .5f, ground))
        {
            transform.Translate(Vector2.down * .1f);
            yield return new WaitForEndOfFrame();
        }
        if (Physics2D.OverlapCircle(transform.position, .3f, ground))
        {
            gameObject.SetActive(false);
        }
    }
}
