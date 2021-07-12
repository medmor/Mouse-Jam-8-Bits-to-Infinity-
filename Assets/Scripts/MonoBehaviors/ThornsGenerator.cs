using System.Collections.Generic;
using UnityEngine;
public class ThornsGenerator : MonoBehaviour
{
    public GameObject ThornPref;
    private List<GameObject> ThornsPool { get; set; } = new List<GameObject>();

    public void Start()
    {
        SpawnThornsPool();
        GameManager.Instance.FloorExtended.AddListener(SpawnThorns);
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            foreach (var t in ThornsPool)
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
        for (var i = 0; i < ThornsPool.Count; i++)
        {
            if (!ThornsPool[i].activeSelf)
            {
                ThornsPool[i].SetActive(true);
                return ThornsPool[i];
            }
        }
        foreach (var thorn in ThornsPool)
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
            GameObject thorn = Instantiate(ThornPref);
            thorn.SetActive(false);
            ThornsPool.Add(thorn);
        }
    }
}
