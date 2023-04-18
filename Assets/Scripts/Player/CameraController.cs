using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController target;
    public float smoothTime = 0;
    public float offsetX;
    Vector3 velocity;
    public bool Rightoffset { get; set; }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Rightoffset)
            Move(offsetX);
        else
            Move(-offsetX);
    }

    private void Move(float offset)
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + new Vector3(offset, 1, -10), ref velocity, smoothTime);
    }
}
