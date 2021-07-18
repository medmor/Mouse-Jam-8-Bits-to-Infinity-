using System.Collections;
using UnityEngine;

public class GroundedElement : WorldElementBase
{
    LayerMask ground;
    LayerMask overlapingCheck;
    Vector3 size;
    Vector3 checkPos;
    private void Start()
    {
        ground = LayerMask.GetMask("Ground");
        overlapingCheck = LayerMask.GetMask("NotOverlaping");
        size = GetComponent<Renderer>().bounds.size;
    }
    public override void Activate(Vector3 firstPos, Vector3 secondPos)
    {
        transform.position = firstPos + 5 * Vector3.up + Random.value * (secondPos - firstPos);
        checkPos = transform.position + 1.3f * size.y / 3 * Vector3.down;
        gameObject.SetActive(true);
        StartCoroutine(FallToGround());
    }
    public IEnumerator FallToGround()
    {
        while (!Physics2D.OverlapCircle(checkPos, .1f, ground))
        {
            checkPos = transform.position + 1.3f * size.y / 3 * Vector3.down;
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
            var over = Physics2D.OverlapCircle(checkPos, size.x, overlapingCheck);
            if (over && over.transform != transform)
                Desactivate();
            yield return new WaitForFixedUpdate();
        }
    }

}
