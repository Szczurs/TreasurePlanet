using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMagic : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float yaw = 0.0f;
    public float pitch = 0.0f;


    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        //pitch -= speedH * Input.GetAxis("Mouse y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
