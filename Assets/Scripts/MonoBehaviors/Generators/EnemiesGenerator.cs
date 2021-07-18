using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemiesGenerator : MonoBehaviour
{
    public GameObject Enemy1Pref;
    public GameObject Enemy2Pref;
    public GameObject Enemy3Pref;
    public GameObject Enemy4Pref;
    public GameObject Enemy5Pref;
    private List<Enemy> enemiesPool { get; set; } = new List<Enemy>();
    private int numberOfEnemiesToSpawn = 1;

    public void Start()
    {
        SpawnEnemiesPool();
        StartCoroutine(IncreamentNumberOfEnemiesToSpawn());
        GameManager.Instance.FloorExtended.AddListener(SpawnEnemy);
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            foreach (var e in enemiesPool)
            {
                Destroy(e.ExplosionEffect.gameObject);
                Destroy(e.gameObject);
            }
            Destroy(gameObject);
        });

    }
    public void SpawnEnemy(Vector3 firstP, Vector3 secondP)
    {
        var j = Random.Range(1, numberOfEnemiesToSpawn);
        for (var i = 0; i < j; i++)
        {
            var enemy = GetEnemy();
            var dir = secondP - firstP;
            if (enemy)
            {
                enemy.transform.position = firstP + dir + 2 * Vector3.up;
                enemy.FollowPlayer();
            }
        }

    }
    Enemy GetEnemy()
    {
        foreach (var enemy in enemiesPool)
        {
            if (enemy.gameObject.activeInHierarchy && Camera.main.transform.position.x - enemy.transform.position.x > 15)
            {
                enemy.gameObject.SetActive(false);
            }
        }
        for (var i = 0; i < enemiesPool.Count; i++)
        {
            if (!enemiesPool[i].gameObject.activeSelf)
            {
                enemiesPool[i].gameObject.SetActive(true);
                return enemiesPool[i];
            }
        }

        return null;
    }
    void SpawnEnemiesPool()
    {
        FillEnemiesPool(Enemy1Pref, 5);
        FillEnemiesPool(Enemy2Pref, 3);
        FillEnemiesPool(Enemy3Pref, 2);
        FillEnemiesPool(Enemy4Pref, 2);
        FillEnemiesPool(Enemy5Pref, 2);
    }
    void FillEnemiesPool(GameObject enemyPref, int number)
    {
        for (var i = 0; i < number; i++)
        {
            GameObject enemy = Instantiate(enemyPref);
            enemy.SetActive(false);
            enemiesPool.Add(enemy.GetComponent<Enemy>());
        }
    }
    IEnumerator IncreamentNumberOfEnemiesToSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(30 * numberOfEnemiesToSpawn);
            numberOfEnemiesToSpawn++;
        }
    }
}
