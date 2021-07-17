using System.Collections.Generic;
using UnityEngine;

public class WorldElementsGenerator : MonoBehaviour
{
    public GameObject StaticElementPref;
    public GameObject MovingElementPref;
    public GameObject GroundedElementPref;
    public Sprite[] StaticElementSprites;
    public Sprite[] MovingElementSprites;
    public Sprite[] GroundedElementSprites;

    private List<WorldElementBase> ElementsPool = new List<WorldElementBase>();

    private void Start()
    {
        GameManager.Instance.FloorExtended.AddListener((Vector3 v1, Vector3 v2) =>
        {
            SpawnWoldElement(v1, v2);
        });
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            foreach (var e in ElementsPool)
                Destroy(e.gameObject);
            Destroy(gameObject);
        });

        SpawnElementsPool();
    }

    void SpawnWoldElement(Vector3 firstPos, Vector3 secondPos)
    {
        for (var i = 0; i < 10; i++)
        {
            var element = GetRandomElement();
            if (element)
                element.Activate(firstPos, secondPos);
        }
    }
    WorldElementBase GetRandomElement()
    {
        var e = ElementsPool[Random.Range(0, ElementsPool.Count - 1)];
        if (!e.IsActif()) return e;
        else return null;
    }
    WorldElementBase GetWorldElement()
    {
        foreach (var e in ElementsPool)
        {
            if (!e.IsActif())
            {
                return e;
            }
        }
        return null;
    }
    void SpawnElementsPool()
    {
        FillPool(StaticElementSprites, StaticElementPref);
        FillPool(MovingElementSprites, MovingElementPref);
        FillPool(GroundedElementSprites, GroundedElementPref);

    }
    void FillPool(Sprite[] sprites, GameObject pref)
    {
        foreach (var s in sprites)
        {
            var go = Instantiate(pref);
            go.GetComponent<SpriteRenderer>().sprite = s;
            var e = go.GetComponent<WorldElementBase>();
            e.Desactivate();
            ElementsPool.Add(e);
        }
    }

}
