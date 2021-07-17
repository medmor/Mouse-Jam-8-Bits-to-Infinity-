using System.Collections;
using UnityEngine;

public class GroundedElement : WorldElementBase
{
    LayerMask ground;
    LayerMask overlapingCheck;
    float size;
    private void Start()
    {
        ground = LayerMask.GetMask("Ground");
        overlapingCheck = LayerMask.GetMask("NotOverlaping");
        size = GetComponent<Renderer>().bounds.size.x;
    }
    public override void Activate(Vector3 firstPos, Vector3 secondPos)
    {

        transform.position = firstPos + 5 * Vector3.up + Random.value * (secondPos - firstPos);
        gameObject.SetActive(true);
        StartCoroutine(FallToGround());
    }
    public IEnumerator FallToGround()
    {
        while (!Physics2D.OverlapCircle(transform.position, GetComponent<Renderer>().bounds.size.y / 2, ground))
        {
            transform.Translate(Vector2.down * .1f);
            yield return new WaitForEndOfFrame();
        }

        if (IsActif())
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
}
