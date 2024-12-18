using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathLineDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public LineRenderer LineRenderer
    {
        get
        {
            if (_lineRenderer == null)
                _lineRenderer = GetComponent<LineRenderer>();
            return _lineRenderer;
        }
    }

    public void DrawLine(Vector3[] linePoints)
    {
        LineRenderer.positionCount = linePoints.Length;
        LineRenderer.SetPositions(linePoints);
    }

    public void SetActiveLine(bool isActive)
    {
        LineRenderer.enabled = isActive;
    }
}
