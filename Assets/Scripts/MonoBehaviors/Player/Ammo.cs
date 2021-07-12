using UnityEngine;

public class Ammo : MonoBehaviour
{
    private float speed = 2f;

    void FixedUpdate()
    {
        transform.position += Vector3.right * speed;
        print(transform.position.x);
    }
}
