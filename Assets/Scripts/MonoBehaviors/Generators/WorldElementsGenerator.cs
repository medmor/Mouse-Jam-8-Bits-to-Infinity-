using System.Collections.Generic;
using UnityEngine;

public class WorldElementsGenerator : MonoBehaviour
{
    public GameObject[] FloatingElementsPrefs;

    private List<GameObject> FloatingElementsPool = new List<GameObject>();

    private void Start()
    {
        GameManager.Instance.FloorExtended.AddListener((Vector3 v1, Vector3 v2) =>
        {
            SpawnWoldElement(v1, v2);
        });
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            foreach (var e in FloatingElementsPool)
                Destroy(e);
            Destroy(gameObject);
        });

        SpawnFloatingElementsPool();
    }
    private void Update()
    {
        if (Camera.main.transform.position.x - FloatingElementsPool[0].transform.position.x > 50)
            FloatingElementsPool[0].SetActive(false);
    }
    void SpawnWoldElement(Vector3 firstPos, Vector3 secondPos)
    {
        var element = GetFloatingElement();
        if (element)
            element.transform.position = firstPos + Vector3.up * 4 + Random.value * Vector3.up * 3 + Random.value * (secondPos - firstPos);
    }

    GameObject GetFloatingElement()
    {
        foreach (var e in FloatingElementsPool)
        {
            if (!e.activeSelf)
            {
                e.SetActive(true);
                return e;
            }
        }
        return null;
    }
    void SpawnFloatingElementsPool()
    {
        foreach (var element in FloatingElementsPrefs)
        {
            var e = Instantiate(element);
            e.SetActive(false);
            FloatingElementsPool.Add(e);
        }
    }

}
