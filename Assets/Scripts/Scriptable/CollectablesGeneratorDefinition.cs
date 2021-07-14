using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectablesGeneratorDefinition", menuName = "CollectablesGeneratorDefinition")]
public class CollectablesGeneratorDefinition : ScriptableObject
{
    public GameObject CoinPref;
    public List<GameObject> EquipementsPrefs = new List<GameObject>();

    public float CoinsFrequency;
    public float EquipementFrequency;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
