using System.Collections.Generic;

using UnityEngine;

public class CollectablesGenerator : MonoBehaviour
{
    public GameObject CoinPref;
    public List<GameObject> EquipementsPrefs = new List<GameObject>();
    private List<GameObject> CoinsPool = new List<GameObject>();


    public void Start()
    {
        SpawnCoinsPool();
        GameManager.Instance.FloorExtended.AddListener(SpawnCoinsAndCollectables);
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            foreach (var t in CoinsPool)
                Destroy(t);
            Destroy(gameObject);
        });
    }
    GameObject GetCoin()
    {
        for (var i = 0; i < CoinsPool.Count; i++)
        {
            if (!CoinsPool[i].activeSelf)
            {
                CoinsPool[i].SetActive(true);
                return CoinsPool[i];
            }
        }
        foreach (var coin in CoinsPool)
        {
            var p = Camera.main.WorldToViewportPoint(coin.transform.position);
            if (p.x <= float.Epsilon)
            {
                coin.SetActive(false);
            }
        }
        return GetCoin();
    }
    void SpawnCoinsPool()
    {
        for (var i = 0; i < 50; i++)
        {
            GameObject coin = Instantiate(CoinPref);
            coin.SetActive(false);
            CoinsPool.Add(coin);
        }
    }
    void SpawnCoinsAndCollectables(Vector3 firstPos, Vector3 secondPos)
    {
        var dir = secondPos - firstPos;

        SpawnCoins(firstPos, dir);
        SpawnRandomEquipement(firstPos, dir);
    }
    void SpawnCoins(Vector3 firstPos, Vector3 dir)
    {
        for (int j = -1; j <= 1; j++)
        {
            var coin = GetCoin();
            coin.transform.position = firstPos + Vector3.up + Vector3.Cross(dir.normalized, Vector3.up) + (Vector3.right + dir.normalized) * j * .5f;
        }
    }
    void SpawnRandomEquipement(Vector3 firstPos, Vector3 dir)
    {
        if (Random.value < .1f)
        {
            var equip = Instantiate(EquipementsPrefs[Random.Range(0, EquipementsPrefs.Count)]);
            Vector3 target_position = firstPos + Random.value * dir;
            equip.transform.position = target_position;
            equip.transform.position += Vector3.up * Random.Range(.5f, 1.5f);
        }
    }
}
