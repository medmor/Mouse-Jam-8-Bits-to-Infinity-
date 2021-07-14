using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private float speed = .6f;

    public void Fire(Transform player)
    {
        SoundManager.Instance.PlayEffects("Fire");
        StartCoroutine(fire(player));
    }
    IEnumerator fire(Transform player)
    {
        while (player && Vector2.Distance(player.position, transform.position) < 10)
        {
            transform.position += Quaternion.Euler(0, 0, player.eulerAngles.z) * Vector3.right * speed;
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
            SoundManager.Instance.PlayEffects("Explosion");
            collision.gameObject.GetComponent<EnemyController>().Explode();
        }
    }

}
