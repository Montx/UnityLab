using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followedObject;

    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = followedObject.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (previousPosition != followedObject.position) {
            Vector3 dirTomove = (followedObject.position - previousPosition);
            transform.position += dirTomove;
            previousPosition = followedObject.position;
        }
    }
}
