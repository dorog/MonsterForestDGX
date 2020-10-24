using System.Collections;
using UnityEngine;

public class MockAxisInput : AxisInput
{
    private bool working = false;
    private Vector2 direction;

    public void SetGetValue(Vector2 mockDirection, float time)
    {
        working = true;
        direction = mockDirection;

        StartCoroutine(SetEnd(time));
    }

    private IEnumerator SetEnd(float time)
    {
        yield return new WaitForSeconds(time);

        working = false;
    }

    protected override Vector2 GetValue()
    {
        if (working)
        {
            return direction;
        }

        return Vector2.zero;
    }
}
