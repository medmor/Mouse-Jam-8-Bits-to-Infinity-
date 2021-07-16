using UnityEngine;

public abstract class WorldElementBase : MonoBehaviour
{
    private void Update()
    {
        if (IsOutView()) Desactivate();
    }
    public virtual void Activate(Vector3 firstPos, Vector3 secondPos)
    {
        transform.position = firstPos + Vector3.up * Random.Range(-4, 4) + Random.value * (secondPos - firstPos);
        gameObject.SetActive(true);
    }
    public virtual void Desactivate()
    {
        gameObject.SetActive(false);
    }
    public bool IsActif()
    {
        return gameObject.activeSelf;
    }
    public bool IsOutView()
    {
        return Camera.main.transform.position.x - transform.position.x > 15;
    }

}
