using System.Collections.Generic;
using UnityEngine;

public class WorldElementsGenerator : MonoBehaviour
{
    public GameObject[] StaticElementsPrefs;
    //public GameObject[] MovingElementsPrefs;

    private List<WorldElementBase> ElementsPool = new List<WorldElementBase>();
    //private List<WorldElementBase> MovingelementsPool = new List<WorldElementBase>();

    private void Start()
    {
        GameManager.Instance.FloorExtended.AddListener((Vector3 v1, Vector3 v2) =>
        {
            SpawnWoldElement(v1, v2);
        });
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            foreach (var e in ElementsPool)
                Destroy(e);
            Destroy(gameObject);
        });

        SpawnElementsPool(StaticElementsPrefs, ElementsPool);
        //SpawnFloatingElementsPool(MovingElementsPrefs, MovingelementsPool);
    }

    void SpawnWoldElement(Vector3 firstPos, Vector3 secondPos)
    {
        for (var i = 0; i < ElementsPool.Count; i++)
        {
            var element = GetWorldElement();
            if (element)
                element.Activate(firstPos, secondPos);
        }
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
    void SpawnElementsPool(GameObject[] elementsPrefs, List<WorldElementBase> ElementsPool)
    {
        foreach (var element in elementsPrefs)
        {
            var e = Instantiate(element).GetComponent<WorldElementBase>();
            e.Desactivate();
            ElementsPool.Add(e);
        }
    }

}
