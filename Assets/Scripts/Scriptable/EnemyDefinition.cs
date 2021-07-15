using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefinition", menuName = "EnemyDefinition")]
public class EnemyDefinition : ScriptableObject
{
    public float MinSpeedLimit;
    public float MaxSpeedLimit;

    public float Health;
}
