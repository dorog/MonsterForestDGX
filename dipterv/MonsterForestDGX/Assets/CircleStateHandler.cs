using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleStateHandler : MonoBehaviour
{
    public Paint paint;

    public void OnEnable()
    {
        paint.circleOn = true;
    }

    public void OnDisable()
    {
        paint.circleOn = false;
    }
}
