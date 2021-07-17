using System.Collections;
using UnityEngine;

public class MovingElement : WorldElementBase
{
    float xSpeed;
    float ySpeed;
    int changeYTime;
    public override void Activate(Vector3 firstPos, Vector3 secondPos)
    {
        xSpeed = -Random.Range(.01f, .1f);
        ySpeed = Random.Range(.01f, .1f);
        changeYTime = Random.Range(100, 1000);
        base.Activate(firstPos, secondPos);
        StartCoroutine(Move());
    }

    public override void Desactivate()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    IEnumerator Move()
    {
        while (true)
        {
            transform.position += Vector3.up * ySpeed + Vector3.right * xSpeed;
            if (changeYTime <= 0)
            {
                changeYTime = Random.Range(100, 1000);
                ySpeed = -ySpeed;
            }
            yield return new WaitForFixedUpdate();
        }
    }

}
