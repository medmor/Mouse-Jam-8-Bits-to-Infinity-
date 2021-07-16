using System.Collections;
using UnityEngine;
public class StaticElements : WorldElementBase
{
    public LayerMask Ground;
    public override void Activate(Vector3 firstPos, Vector3 secondPos)
    {
        transform.position = firstPos + Vector3.up * Random.Range(-4, 4) + Random.value * (secondPos - firstPos);
        gameObject.SetActive(true);
        StartCoroutine(CheckGround());
    }
    IEnumerator CheckGround()
    {
        while (gameObject.activeSelf)
        {
            if (Physics2D.OverlapCircle(transform.position, 1.5f, Ground))
                Desactivate();
            yield return new WaitForFixedUpdate();
        }
    }
}
