﻿using System.Collections.Generic;
using UnityEngine;
using static IntersectionAlgorithm;

public class Rectangle
{
    private readonly int id;
    private readonly float step;
    private readonly bool[] dones;
    private readonly int maxHit;

    private Straight maxY;
    private Straight minY;

    private Straight maxX;
    private Straight minX;

    private Vector2 distancePoint;
    private Vector2 direction;

    private bool? lastHitResult = null;

    public Rectangle(int id, float step, Vector2 startPoint, Vector2 endPoint, float width)
    {
        this.id = id;
        this.step = step;
        maxHit = Mathf.CeilToInt((endPoint - startPoint).magnitude / step);
        dones = new bool[maxHit];

        CalculateLines(startPoint, endPoint, width);
    }

    private void CalculateLines(Vector2 startPoint, Vector2 endPoint, float width)
    {

        direction = (endPoint - startPoint).normalized;
        float signedAngle = Vector2.SignedAngle(Vector2.right, direction);

        if (signedAngle >= 0 && signedAngle < 90)
        {
            // 0 and I
            SetUpLines(startPoint, direction, startPoint, endPoint, width);
        }
        else if (signedAngle >= 90 && signedAngle < 180)
        {
            // 90 and II
            Vector2 half = (endPoint - startPoint) / 2 + startPoint;
            Vector2 normal = new Vector2(direction.y, -direction.x);
            SetUpLines(startPoint, normal, half - width * normal, half + width * normal, (startPoint - endPoint).magnitude / 2);
        }
        else if (signedAngle < 0 && signedAngle >= -90)
        {
            // -90 and IV
            Vector2 half = (endPoint - startPoint) / 2 + startPoint;
            Vector2 normal = new Vector2(-direction.y, direction.x);
            SetUpLines(startPoint, normal, half - width * normal, half + width * normal, (startPoint - endPoint).magnitude / 2);
        }
        else
        {
            // 180 and III
            SetUpLines(startPoint, -direction, endPoint, startPoint, width);
        }
    }

    private void SetUpLines(Vector2 distancePoint, Vector2 direction, Vector2 startPoint, Vector2 endPoint, float width)
    {
        this.distancePoint = distancePoint;

        Vector2 normal = new Vector2(-direction.y, direction.x);

        maxY = new Straight(startPoint + width * normal, normal, endPoint + width * normal);
        minY = new Straight(startPoint - width * normal, normal, endPoint - width * normal);

        maxX = new Straight(endPoint + width * normal, direction, endPoint - width * normal);
        minX = new Straight(startPoint + width * normal, direction, startPoint - width * normal);
    }

    private bool Include(Vector2 point)
    {
        float maxValueY = maxY.GetY(point.x);
        float minValueY = minY.GetY(point.x);

        float maxValueX = maxX.GetX(point.y);
        float minValueX = minX.GetX(point.y);

        return (point.y >= minValueY && point.y <= maxValueY) && (point.x <= maxValueX && point.x >= minValueX);
    }

    public HitResult Guess(Vector2 previous, Vector2 point, int lastId)
    {
        if (id + maxHit <= lastId)
        {
            return new HitResult()
            {
                Hit = false
            };
        }

        if (lastHitResult != null)
        {
            lastHitResult = Include(previous);

            List<IntersectionResult> results = new List<IntersectionResult>
            {
                LineIntersection(minX.P1, minX.P2, previous, point),
                LineIntersection(minY.P1, minY.P2, previous, point),
                LineIntersection(maxX.P1, maxX.P2, previous, point),
                LineIntersection(maxY.P1, maxY.P2, previous, point)
            };

            results.RemoveAll(x => !x.Intersected);
            results = Distinct(results);

            if (results.Count > 0)
            {
                if (results.Count > 1)
                {
                    List<int> indexes = new List<int>();
                    foreach (var result in results)
                    {
                        int index = GetCell(result.Point);
                        indexes.Add(index);
                    }

                    indexes.Sort();

                    int lastIndex = indexes[0];
                    int actualIndex = indexes[1];

                    //Same #1
                    lastHitResult = true;

                    if (actualIndex > lastIndex)
                    {
                        for (int i = lastIndex; i <= actualIndex; i++)
                        {
                            if (i + id > lastId)
                            {
                                dones[i] = true;
                            }
                        }

                        return new HitResult()
                        {
                            Hit = true,
                            Id = actualIndex + id,
                            LastPoint = results[1].Point,
                            StartPoint = results[0].Point
                        };
                    }
                    else
                    {
                        return new HitResult()
                        {
                            Hit = false,
                        };
                    }
                }
                else
                {
                    int lastIndex = -1;
                    int actualIndex = -2;

                    Vector2 startPoint;
                    Vector2 lastPoint;

                    if (lastHitResult == true)
                    {
                        startPoint = previous;
                        lastPoint = results[0].Point;

                        lastIndex = GetCell(previous);
                        actualIndex = GetCell(results[0].Point);
                    }
                    else
                    {
                        startPoint = results[0].Point;
                        lastPoint = point;

                        lastIndex = GetCell(results[0].Point);
                        actualIndex = GetCell(point);
                    }

                    //Same #3
                    lastHitResult = true;

                    if (actualIndex > lastIndex)
                    {
                        for (int i = lastIndex; i <= actualIndex; i++)
                        {
                            if (i + id > lastId)
                            {
                                dones[i] = true;
                            }
                        }

                        return new HitResult()
                        {
                            Hit = true,
                            Id = actualIndex + id,
                            LastPoint = lastPoint,
                            StartPoint = startPoint
                        };
                    }
                    else
                    {
                        return new HitResult()
                        {
                            Hit = false
                        };
                    }
                }
            }
            else
            {
                if (Include(point) && lastHitResult == true)
                {
                    int lastIndex = GetCell(previous);
                    int actualIndex = GetCell(point);

                    //Same #2
                    lastHitResult = true;

                    if (actualIndex > lastIndex)
                    {
                        for(int i = lastIndex; i <= actualIndex; i++)
                        {
                            if(i + id > lastId)
                            {
                                dones[i] = true;
                            }
                        }

                        return new HitResult()
                        {
                            Hit = true,
                            Id = actualIndex + id,
                            LastPoint = point,
                            StartPoint = previous,
                            Full = true,
                        };
                    }
                    else
                    {
                        return new HitResult()
                        {
                            Hit = false
                        };
                    }
                }
                else
                {
                    lastHitResult = false;

                    return new HitResult()
                    {
                        Hit = false
                    };
                }
            }
        }
        else
        {
            if (Include(point))
            {
                lastHitResult = true;

                int calculatedLastId = GetCell(point);

                dones[calculatedLastId] = true;

                return new HitResult()
                {
                    Hit = true,
                    Id = calculatedLastId + id,
                    LastPoint = point
                };
            }
            else
            {
                lastHitResult = false;

                return new HitResult()
                {
                    Hit = false,
                };
            }
        }
    }

    private List<IntersectionResult> Distinct(List<IntersectionResult> source)
    {
        List<IntersectionResult> result = new List<IntersectionResult>();

        HashSet<Vector2> keys = new HashSet<Vector2>();
        foreach (IntersectionResult element in source)
        {
            if (!keys.Contains(element.Point))
            {
                result.Add(element);
                keys.Add(element.Point);
            }
        }

        return result;
    }

    public int GetCell(Vector2 point)
    {
        Vector2 dir = point - distancePoint;

        float angle = Vector2.Angle(dir, direction);
        float length = dir.magnitude;

        if (dir.magnitude < 0.1)
        {
            return 0;
        }

        float distance = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * angle) * length);

        int calculatedId = Mathf.FloorToInt(distance / step);
        calculatedId = calculatedId >= dones.Length ? dones.Length - 1 : calculatedId;

        return calculatedId;
    }

    public int GetHitNumber()
    {
        int hits = 0;
        for (int i = 0; i < dones.Length; i++)
        {
            if (dones[i])
            {
                hits++;
            }
        }

        return hits;
    }

    public int GetMaxHitNumber()
    {
        return maxHit;
    }

    public void Reset()
    {
        for(int i = 0; i < dones.Length; i++)
        {
            dones[i] = false;
        }
    }
}
