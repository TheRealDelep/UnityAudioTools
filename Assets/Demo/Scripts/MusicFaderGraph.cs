using Delep.Audio.Toys;

using UnityEngine;

public class MusicFaderGraph : MonoBehaviour
{
    private const int lineRendererPositionCount = 50;

    [SerializeField]
    private Transform StartPos;

    [SerializeField]
    private Transform EndPos;

    public SlopeType SlopeType { get; set; }
    public float SlopeValue { get; set; }
    public float Time { get; set; }

    private Vector2 CurvePoint { get; set; }
    private LineRenderer LineRenderer { get; set; }

    public void Draw()
    {
        if (LineRenderer == null)
        {
            LineRenderer = GetComponent<LineRenderer>();
            LineRenderer.positionCount = lineRendererPositionCount;
        }

        SetCurvePoint();

        for (int i = 0; i < lineRendererPositionCount; i++)
        {
            var p0 = InterpolateVectors(StartPos.position, CurvePoint, i);
            var p1 = InterpolateVectors(CurvePoint, EndPos.position, i);

            LineRenderer.SetPosition(i, InterpolateVectors(p0, p1, i));
        }
    }

    private void SetCurvePoint()
    {
        var length = EndPos.position.x - StartPos.position.x;

        CurvePoint = SlopeType switch
        {
            SlopeType.Linear => StartPos.position,
            SlopeType.Concave => new Vector3(StartPos.position.x + (SlopeValue * length), StartPos.position.y),
            SlopeType.Convex => new Vector3(EndPos.position.x - (SlopeValue * length), EndPos.position.y)
        };
    }

    private float Interpolate(float start, float dest, float i)
        => start != dest
            ? Mathf.Lerp(start, dest, i * (dest - start) / (lineRendererPositionCount - 1) / (dest - start))
            : start;

    private Vector2 InterpolateVectors(Vector2 start, Vector2 dest, float i)
        => new Vector2(Interpolate(start.x, dest.x, i), Interpolate(start.y, dest.y, i));
}