using UnityEngine;
using UnityEngine.U2D;

public class GroundGenerator : MonoBehaviour
{
    public SpriteShapeController FloorShapeController;
    private Spline floor;

    void Start()
    {
        floor = FloorShapeController.spline;
        for (var i = 0; i < 10; i++)
        {
            InsertNewPoint(false);
        }
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            Destroy(gameObject);
        });
    }

    void Update()
    {
        var p = Camera.main.WorldToViewportPoint(floor.GetPosition(floor.GetPointCount() / 2) + FloorShapeController.transform.position);
        if (p.x <= float.Epsilon)
        {
            InsertNewPoint(true);
        }
    }
    void InsertNewPoint(bool removeFirst)
    {
        if (removeFirst)
            floor.RemovePointAt(0);

        var count = floor.GetPointCount();
        var firstPos = floor.GetPosition(count - 1);
        var secondPos = firstPos + Vector3.right * Random.Range(5, 15) + Vector3.up * Random.Range(-5, 5);
        floor.InsertPointAt(count, secondPos);

        floor.SetTangentMode(count - 1, ShapeTangentMode.Continuous);
        floor.SetRightTangent(count - 1, 2 * Vector3.right);
        floor.SetLeftTangent(count - 1, 2 * -Vector3.right);

        floor.SetTangentMode(count, ShapeTangentMode.Continuous);
        GameManager.Instance.FloorExtended.Invoke(firstPos, secondPos);
    }
}
