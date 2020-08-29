using System.Collections.Generic;
using UnityEngine;

public class GuideDrawHelper : MonoBehaviour
{
    private Transform startPoint;
    private List<Transform> guidePoints;

    private Transform target;

    private bool running = false;

    public float speed = 1;

    private int next = 0;

    public void Init(List<Transform> guidePoints, Transform startPoint)
    {
        this.guidePoints = guidePoints;
        this.startPoint = startPoint;
    }

    private void OnEnable()
    {
        StartHelper();
    }

    private void OnDisable()
    {
        running = false;
    }

    public void StartHelper()
    {
        transform.position = startPoint.position;

        next = -1;
        target = GetNext();

        running = true;
    }

    private void Update()
    {
        if (running)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            if(Vector3.Distance(target.position, transform.position) < 0.01)
            {
                target = GetNext();
            }
        }
    }

    private Transform GetNext()
    {
        if(next == guidePoints.Count - 1)
        {
            next = -1;
            transform.position = startPoint.position;
        }
        next++;

        return guidePoints[next];
    }
}
