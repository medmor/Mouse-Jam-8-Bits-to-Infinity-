using UnityEngine;
using UnityEngine.U2D;

public class GroundGenerator : MonoBehaviour
{
    public SpriteShapeController FloorShapeController;
    private Spline ground;

    void Start()
    {
        ground = FloorShapeController.spline;
        GameManager.Instance.PlayerKilled.AddListener(() =>
        {
            Destroy(gameObject);
        });
        FirstExtend();
    }
    public void FirstExtend()
    {
        for (var i = 0; i < 8; i++)
        {
            InsertNewPoint(false);
        }
    }
    void Update()
    {
        var p = Camera.main.WorldToViewportPoint(ground.GetPosition(ground.GetPointCount() / 2) + FloorShapeController.transform.position);
        if (p.x <= float.Epsilon)
        {
            InsertNewPoint(true);
        }
    }
    void InsertNewPoint(bool removeFirst)
    {
        if (removeFirst)
            ground.RemovePointAt(0);

        var count = ground.GetPointCount();
        var firstPos = ground.GetPosition(count - 1);
        var secondPos = firstPos + Vector3.right * Random.Range(5, 15) + Vector3.up * Random.Range(-5, 5);
        ground.InsertPointAt(count, secondPos);

        ground.SetTangentMode(count - 1, ShapeTangentMode.Continuous);
        ground.SetRightTangent(count - 1, 2 * Vector3.right);
        ground.SetLeftTangent(count - 1, 2 * -Vector3.right);

        ground.SetTangentMode(count, ShapeTangentMode.Continuous);
        GameManager.Instance.FloorExtended.Invoke(firstPos, secondPos);
    }
}
