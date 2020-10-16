using UnityEngine;

public class CircleStateHandler : MonoBehaviour
{
    public Paint paint;
    public LineRenderer lineRenderer;

    public void OnEnable()
    {
        paint.circleOn = true;
    }

    public void OnDisable()
    {
        paint.circleOn = false;
        lineRenderer.positionCount = 0;
    }
}
