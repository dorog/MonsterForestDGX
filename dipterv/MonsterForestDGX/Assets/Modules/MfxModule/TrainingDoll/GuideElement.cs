using UnityEngine;

public class GuideElement : MonoBehaviour
{
    public void Set(Vector2 startPosition, Vector2 endPosition, float width)
    {
        Vector3 direction = endPosition - startPosition;

        float length = direction.magnitude;

        float angle = Vector2.SignedAngle(direction, Vector2.right) * -1;

        RectTransform rt = GetComponent<RectTransform>();

        rt.localPosition = new Vector3(startPosition.x, startPosition.y, 0);

        rt.localRotation = Quaternion.Euler(0, 0, angle);
        rt.sizeDelta = new Vector2(length, width);
    }
}
