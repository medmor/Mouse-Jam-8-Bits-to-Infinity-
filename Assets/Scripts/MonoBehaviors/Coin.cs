using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
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
            transform.position = Vector3.MoveTowards(transform.position, player.position, .2f);
            yield return new WaitForFixedUpdate();
        }
    }
}
