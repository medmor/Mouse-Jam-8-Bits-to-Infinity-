using System.Collections.Generic;

using UnityEngine;

public class CollectablesGenerator : MonoBehaviour
{
    public CollectablesGeneratorDefinition Definition;
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
        for (var i = 0; i < 100; i++)
        {
            GameObject coin = Instantiate(Definition.CoinPref);
            coin.SetActive(false);
            CoinsPool.Add(coin);
        }
    }
    void SpawnCoinsAndCollectables(Vector3 firstPos, Vector3 secondPos)
    {
        var dir = secondPos - firstPos;

        if (Random.value < Definition.CoinsFrequency)
            SpawnCoins(firstPos, dir);
        if (Random.value < Definition.EquipementFrequency)
            SpawnRandomEquipement(firstPos, dir);
    }
    void SpawnCoins(Vector3 firstPos, Vector3 dir)
    {
        var limits = Random.Range(1, 3);
        for (int j = -limits; j <= limits; j++)
        {
            var coin = GetCoin();
            coin.transform.position = firstPos + 5 * Vector3.up
                + Vector3.Cross(dir.normalized, Vector3.up)
                + .5f * j * (Vector3.right + dir.normalized);
            StartCoroutine(coin.GetComponent<Coin>().FallToGround());
        }
    }
    void SpawnRandomEquipement(Vector3 firstPos, Vector3 dir)
    {
        var equip = Instantiate(Definition.EquipementsPrefs[Random.Range(0, Definition.EquipementsPrefs.Count)]);
        Vector3 target_position = firstPos + Random.value * dir;
        equip.transform.position = target_position;
        equip.transform.position += Vector3.up + Vector3.up * Random.Range(.5f, 1.5f);
    }

}
