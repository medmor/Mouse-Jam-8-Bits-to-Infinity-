using System.Collections.Generic;
using UnityEngine;
public class EnemiesGenerator : MonoBehaviour
{
    public GameObject Enemy1Pref;
    public GameObject Enemy2Pref;
    private List<Enemy> Enemies1Pool { get; set; } = new List<Enemy>();
    private List<Enemy> Enemies2Pool { get; set; } = new List<Enemy>();

    public void Start()
    {
        SpawnEnemiesPool();
        GameManager.Instance.FloorExtended.AddListener(SpawnEnemy);
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            foreach (var t in Enemies1Pool)
                Destroy(t);
            Destroy(gameObject);
        });
    }
    public void SpawnEnemy(Vector3 firstP, Vector3 secondP)
    {
        var dir = secondP - firstP;
        Enemy enemy;
        if (Random.value < .8)
            enemy = GetEnemy(Enemies1Pool);
        else
            enemy = GetEnemy(Enemies2Pool);
        enemy.transform.position = firstP + dir * Random.value;
        enemy.transform.position += 2 * Vector3.up;
        enemy.FollowPlayer();
    }
    Enemy GetEnemy(List<Enemy> enemiesPool)
    {
        for (var i = 0; i < enemiesPool.Count; i++)
        {
            if (!enemiesPool[i].gameObject.activeSelf)
            {
                enemiesPool[i].gameObject.SetActive(true);
                return enemiesPool[i];
            }
        }
        foreach (var enemy in enemiesPool)
        {
            var p = Camera.main.WorldToViewportPoint(enemy.transform.position);
            if (p.x <= float.Epsilon)
            {
                enemy.gameObject.SetActive(false);
            }
        }
        return GetEnemy(enemiesPool);
    }
    void SpawnEnemiesPool()
    {
        for (var i = 0; i < 10; i++)
        {
            //Enemy 1
            GameObject enemy = Instantiate(Enemy1Pref);
            enemy.SetActive(false);
            Enemies1Pool.Add(enemy.GetComponent<Enemy>());

            //Enemy 2
            enemy = Instantiate(Enemy2Pref);
            enemy.SetActive(false);
            Enemies2Pool.Add(enemy.GetComponent<Enemy>());
        }
    }
}
