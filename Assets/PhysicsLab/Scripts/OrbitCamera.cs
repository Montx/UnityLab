using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;

    float xAxis;
    float yAxis;

    const float distance = 10.0f;
    const float speed = 5.0f;

    void Update() {

        xAxis += Input.GetAxis("Mouse X") * speed;
        yAxis  -= Input.GetAxis("Mouse Y") * speed;
 
        Quaternion rotation = Quaternion.Euler(yAxis, xAxis, 0.0f);

        transform.rotation = rotation; 
 
        transform.position = target.position + rotation* new Vector3(0.0f, 0.0f, -distance);
    }
}
