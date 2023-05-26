using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraSpeedX;
    [SerializeField] private float _cameraSpeedY;
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * _cameraSpeedX * Time.deltaTime;
       else if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * _cameraSpeedX * Time.deltaTime;
        //if (Input.GetKey(KeyCode.W))
        //    transform.position += Vector3.up * _cameraSpeedY * Time.deltaTime;
        //else if (Input.GetKey(KeyCode.S))
        //    transform.position += Vector3.down * _cameraSpeedY * Time.deltaTime;
    }
}
