﻿using UnityEngine;

public class ContinousRotationVR : MonoBehaviour
{
    public float rotationSpeed = 180;

    private Vector3 rotation;

    private AxisInput rotationInput;

    public Player player;

    private void Start()
    {
        rotationInput = KeyBindingManager.GetInstance().continousRotationAxisInput;

        rotationInput.SubscibeToAxisChange(Rotate);

        player.Stopped += rotationInput.Deactivate;
        player.Go += rotationInput.Activate;
    }

    private void Rotate(Vector2 axis)
    {
        rotation += new Vector3(0, axis.x * rotationSpeed * Time.deltaTime, 0);
    }

    private void FixedUpdate()
    {
        transform.Rotate(rotation);
        rotation = Vector3.zero;
    }
}
