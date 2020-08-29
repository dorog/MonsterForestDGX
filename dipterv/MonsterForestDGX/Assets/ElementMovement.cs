using UnityEngine;

public class ElementMovement : MonoBehaviour
{
    public float openingTime = 5;
    public float openingDistance = 20;

    private bool opened = false;
    private bool opening = false;

    private Vector3 openedPositon;

    private float speed;
    private float traveledDistance = 0f;

    private void Start()
    {
        openedPositon = transform.position + new Vector3(0, openingDistance, 0);
        speed = openingDistance / openingTime;
    }

    public void OpenInstantly()
    {
        transform.position = openedPositon;
        opened = true;
    }

    public void OpenContinously()
    {
        if(!opened)
        {
            opening = true;
        }
    }

    private void Update()
    {
        if (opening)
        {
            float distance = Time.deltaTime * speed;

            traveledDistance += distance;

            if (traveledDistance > Mathf.Abs(openingDistance))
            {
                transform.position = openedPositon;
                opening = false;
                opened = true;
            }
            else
            {
                transform.position += new Vector3(0, distance, 0);
            }
        }
    }
}
