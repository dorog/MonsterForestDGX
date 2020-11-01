using UnityEngine;

public class ContinousRotationVR : MonoBehaviour
{
    public float rotationSpeed = 180;

    private Vector3 rotation;

    private AxisInput rotationInput;

    public Transform target;

    public void SetInput(AxisInput _rotationInput)
    {
        rotationInput = _rotationInput;

        rotationInput.SubscibeToAxisChange(Rotate);
    }

    private void Rotate(Vector2 axis)
    {
        rotation += new Vector3(0, axis.x * rotationSpeed * Time.deltaTime, 0);
    }

    public void FixedUpdate()
    {
        target.Rotate(rotation);
        rotation = Vector3.zero;
    }
}
