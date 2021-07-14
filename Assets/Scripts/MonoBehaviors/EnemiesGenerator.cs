using System.Collections.Generic;
using UnityEngine;
public class EnemiesGenerator : MonoBehaviour
{
    public GameObject EnemyPref;
    private List<EnemyController> EnemiesPool { get; set; } = new List<EnemyController>();

    public void Start()
    {
        SpawnEnemiesPool();
        GameManager.Instance.FloorExtended.AddListener(SpawnEnemy);
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            foreach (var t in EnemiesPool)
                Destroy(t);
            Destroy(gameObject);
        });
    }
    public void SpawnEnemy(Vector3 firstP, Vector3 secondP)
    {
        var dir = secondP - firstP;
        var enemy = GetEnemy();
        enemy.transform.position = firstP + dir * Random.value;
        enemy.transform.position += 2 * Vector3.up;
        enemy.FollowPlayer();
    }
    EnemyController GetEnemy()
    {
        for (var i = 0; i < EnemiesPool.Count; i++)
        {
            if (!EnemiesPool[i].gameObject.activeSelf)
            {
                EnemiesPool[i].gameObject.SetActive(true);
                return EnemiesPool[i];
            }
        }
        foreach (var enemy in EnemiesPool)
        {
            var p = Camera.main.WorldToViewportPoint(enemy.transform.position);
            if (p.x <= float.Epsilon)
            {
                enemy.gameObject.SetActive(false);
            }
        }
        return GetEnemy();
    }
    void SpawnEnemiesPool()
    {
        for (var i = 0; i < 10; i++)
        {
            GameObject enemy = Instantiate(EnemyPref);
            enemy.SetActive(false);
            EnemiesPool.Add(enemy.GetComponent<EnemyController>());
        }
    }
}
