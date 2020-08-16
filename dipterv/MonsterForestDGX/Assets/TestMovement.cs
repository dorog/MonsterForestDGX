using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{

    public Rigidbody rigid;
    public float speed = 3f;
    public bool start = true;
    public float time = 30;

    void Update()
    {
        if (start && time > 0)
        {
            Vector3 position = transform.position + transform.forward * speed * Time.deltaTime;
            rigid.MovePosition(position);
        }
    }
}
