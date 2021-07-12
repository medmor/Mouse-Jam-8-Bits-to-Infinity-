using UnityEngine;
[CreateAssetMenu(fileName = "PlayerDefinition", menuName = "PlayerDefinition")]
public class PlayerDefinition : ScriptableObject
{
    public Equipement Shield;
    public Equipement Magnet;

    public float MaxXVelocity = 15;
    public float MinXVelocity = 10;
    public float StartMaxVelocity = 15;
    public float StartMinVelocity = 10;
    public float XVelocityIncrement = 1;

    public float JumpVelocity = 5;

    public void IncrementXVelocity()
    {
        MaxXVelocity += XVelocityIncrement;
        MinXVelocity += XVelocityIncrement;
    }
}
