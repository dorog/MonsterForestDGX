using UnityEngine;

public class ContinousRotationVR : MonoBehaviour
{
    public float rotationSpeed = 180;

    private Vector3 rotation;

    private AxisInput rotationInput;

    private void Start()
    {
        rotationInput = KeyBindingManager.GetInstance().continousRotationAxisInput;
    }

    public void Update()
    {
        Vector2 axis = rotationInput.GetValue();
        rotation += new Vector3(0, axis.x * rotationSpeed * Time.deltaTime, 0);
    }

    private void FixedUpdate()
    {
        transform.Rotate(rotation);
        rotation = Vector3.zero;
    }
}
