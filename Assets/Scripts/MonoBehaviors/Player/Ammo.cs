using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private float speed = .5f;

    public void Fire(Transform player)
    {
        StartCoroutine(fire(player));
    }
    IEnumerator fire(Transform player)
    {
        while (Vector2.Distance(player.position, transform.position) < 10)
        {
            transform.position += Vector3.right * speed;
            yield return new WaitForFixedUpdate();
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }

}
