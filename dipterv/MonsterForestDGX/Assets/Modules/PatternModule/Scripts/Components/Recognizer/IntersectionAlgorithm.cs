using UnityEngine;

public class IntersectionAlgorithm
{
    //Copy
    //"Faster Line Segment Intersection" by Franklin Antonio(1992).
    public static IntersectionResult LineIntersection(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
    {
        float Ax, Bx, Cx, Ay, By, Cy, d, e, f, num, offset;
        float x1lo, x1hi, y1lo, y1hi;

        Ax = p2.x - p1.x;
        Bx = q1.x - q2.x;

        // X bound box test/
        if (Ax < 0)
        {
            x1lo = p2.x; x1hi = p1.x;
        }
        else
        {
            x1hi = p2.x; x1lo = p1.x;
        }

        if (Bx > 0)
        {
            if (x1hi < q2.x || q1.x < x1lo)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }
        else
        {
            if (x1hi < q1.x || q2.x < x1lo)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }

        Ay = p2.y - p1.y;
        By = q1.y - q2.y;

        // Y bound box test//
        if (Ay < 0)
        {
            y1lo = p2.y; y1hi = p1.y;
        }
        else
        {
            y1hi = p2.y; y1lo = p1.y;
        }

        if (By > 0)
        {
            if (y1hi < q2.y || q1.y < y1lo)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }
        else
        {
            if (y1hi < q1.y || q2.y < y1lo)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }

        Cx = p1.x - q1.x;
        Cy = p1.y - q1.y;
        d = By * Cx - Bx * Cy;  // alpha numerator//
        f = Ay * Bx - Ax * By;  // both denominator//

        // alpha tests//
        if (f > 0)
        {
            if (d < 0 || d > f)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }
        else
        {
            if (d > 0 || d < f)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }

        e = Ax * Cy - Ay * Cx;  // beta numerator//

        // beta tests //
        if (f > 0)
        {
            if (e < 0 || e > f)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }
        else
        {
            if (e > 0 || e < f)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }

        // check if they are parallel
        if (f == 0)
        {
            return new IntersectionResult()
            {
                Intersected = false
            };
        }

        Vector2 intersection = Vector2.zero;

        // compute intersection coordinates //
        num = d * Ax; // numerator //
        offset = same_sign(num, f) ? f * 0.01f : -f * 0.01f;   // round direction //
        intersection.x = p1.x + (num + offset) / f;

        num = d * Ay;
        offset = same_sign(num, f) ? f * 0.01f : -f * 0.01f;
        intersection.y = p1.y + (num + offset) / f;

        return new IntersectionResult()
        {
            Intersected = true,
            Point = intersection
        };
    }

    private static bool same_sign(float a, float b)
    {
        return ((a * b) >= 0f);
    }

    public class IntersectionResult
    {
        public bool Intersected { get; set; }
        public Vector2 Point { get; set; }

        public float GetDistanceFrom(Vector2 from)
        {
            return (Point - from).magnitude;
        }
    }
}
