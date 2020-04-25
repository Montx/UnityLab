using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public PhysicsEngine projectille;
    public float maxLaunchSpeed;

    private float launchSpeed;
    private float extraSpeedPerFrame;

    private void Start() {
        launchSpeed = 1;
        extraSpeedPerFrame = (maxLaunchSpeed * Time.fixedDeltaTime);
    }

    private void OnMouseDown() {
        launchSpeed = 0;
        InvokeRepeating("IncreaseLaunchSpeed", 0.5f, Time.fixedDeltaTime);
    }

    private void OnMouseUp() {
        CancelInvoke();
        PhysicsEngine obj = Instantiate(projectille) as PhysicsEngine;
        obj.transform.position = GameObject.Find("LaunchedBalls").transform.position;
        obj.velocity = transform.up * launchSpeed;
    }

    private void IncreaseLaunchSpeed() {
        if (launchSpeed <= maxLaunchSpeed) {
            launchSpeed += extraSpeedPerFrame;
        }
    }
}
