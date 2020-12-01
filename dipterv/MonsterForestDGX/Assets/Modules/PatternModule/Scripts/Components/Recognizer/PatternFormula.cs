using System.Collections.Generic;
using UnityEngine;

public class PatternFormula
{
    private readonly List<Rectangle> rectangles = new List<Rectangle>();
    private readonly float step = 10;
    private int lastId = int.MinValue;

    private Vector2 previous = Vector2.zero;
    private bool notFirstGuess = false;

    public float PatternLength { get; private set; } = 0;
    public float MissLength { get; private set; } = 0;

    public PatternFormula(List<Vector2> points, float width = 10)
    {
        int id = 0;
        for(int i = 0; i < points.Count - 1; i++)
        {
            int maxHit = Mathf.CeilToInt((points[i + 1] - points[i]).magnitude / step);
            rectangles.Add(new Rectangle(id, step, points[i], points[i + 1], width));
            PatternLength += (points[i] - points[i + 1]).magnitude;
            id += maxHit;
        }
    }

    public void Guess(Vector2 point)
    {
        bool hit = false;
        for(int i = 0; i < rectangles.Count; i++)
        {
            HitResult hitResult = rectangles[i].Guess(previous, point, lastId);
            if (hitResult.Hit != false)
            {
                hit = true;
                lastId = hitResult.Id;

                if (hitResult.Full)
                {
                    break;
                }
                else
                {
                    MissLength += (hitResult.StartPoint - previous).magnitude;

                    if(i == rectangles.Count - 1)
                    {
                        MissLength += (point - hitResult.LastPoint).magnitude;
                    }
                }

                previous = hitResult.LastPoint;
            }
        }

        if (!hit && notFirstGuess)
        {
            MissLength += (point - previous).magnitude;
        }

        notFirstGuess = true;
        previous = point;
    }

    public float GetResult()
    {
        Debug.Log("Miss(%): " + (MissLength / PatternLength));
        if(MissLength / PatternLength > 0.2)
        {
            return 0;
        }

        int correct = 0;
        int max = 0;
        for(int i = 0; i < rectangles.Count; i++)
        {
            correct += rectangles[i].GetHitNumber();
            max += rectangles[i].GetMaxHitNumber();
        }

        return ((float)correct) / max;
    }

    public void Reset()
    {
        lastId = int.MinValue;
        for (int i = 0; i < rectangles.Count; i++)
        {
            rectangles[i].Reset();
        }

        MissLength = 0;
    }
}
