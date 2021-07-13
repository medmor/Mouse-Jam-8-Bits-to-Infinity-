using System.Collections.Generic;
using UnityEngine;
public class EnemiesGenerator : MonoBehaviour
{
    public GameObject EnemyPref;
    private List<GameObject> EnemiesPool { get; set; } = new List<GameObject>();

    public void Start()
    {
        SpawnThornsPool();
        GameManager.Instance.FloorExtended.AddListener(SpawnThorns);
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            foreach (var t in EnemiesPool)
                Destroy(t);
            Destroy(gameObject);
        });
    }
    public void SpawnThorns(Vector3 firstP, Vector3 secondP)
    {
        var dir = secondP - firstP;
        var thorn = GetThorn();
        thorn.transform.position = firstP + dir * Random.value;
        thorn.transform.position += Vector3.up + Vector3.up * Random.value;
    }
    GameObject GetThorn()
    {
        for (var i = 0; i < EnemiesPool.Count; i++)
        {
            if (!EnemiesPool[i].activeSelf)
            {
                EnemiesPool[i].SetActive(true);
                return EnemiesPool[i];
            }
        }
        foreach (var thorn in EnemiesPool)
        {
            var p = Camera.main.WorldToViewportPoint(thorn.transform.position);
            if (p.x <= float.Epsilon)
            {
                thorn.SetActive(false);
            }
        }
        return GetThorn();
    }
    void SpawnThornsPool()
    {
        for (var i = 0; i < 10; i++)
        {
            GameObject thorn = Instantiate(EnemyPref);
            thorn.SetActive(false);
            EnemiesPool.Add(thorn);
        }
    }
}
