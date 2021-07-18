using System.Collections;
using UnityEngine;
public class StaticElements : WorldElementBase
{
    LayerMask overlapingCheck;
    float size;
    private void Start()
    {
        overlapingCheck = LayerMask.GetMask("Ground", "NotOverlaping");
        size = GetComponent<Renderer>().bounds.size.x;
    }
    public override void Activate(Vector3 firstPos, Vector3 secondPos)
    {
        transform.position = firstPos + Vector3.up * Random.Range(2, 20) + Random.value * (secondPos - firstPos);
        gameObject.SetActive(true);
        StartCoroutine(CheckOverlaping());
    }
    IEnumerator CheckOverlaping()
    {
        while (gameObject.activeSelf)
        {
            var over = Physics2D.OverlapCircle(transform.position, size, overlapingCheck);
            if (over && over.transform != transform)
                Desactivate();

            yield return new WaitForFixedUpdate();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, size);
    }
}
