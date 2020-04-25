using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidDrag : MonoBehaviour
{
    [Range(1, 2f)]
    public float velocityExponent; // [none]
    public float dragConstant;

    private PhysicsEngine physicsEngine;

    private void Start() {
        physicsEngine = GetComponent<PhysicsEngine>();
    }

    private void FixedUpdate() {
        Vector3 velocity = physicsEngine.velocity;
        float speed = velocity.magnitude;
        float dragMagnitude = CalculateDrag(speed);
        physicsEngine.AddForce(-velocity.normalized * dragMagnitude);
    }

    private float CalculateDrag(float speed) {
        return dragConstant * Mathf.Pow(speed, velocityExponent); // Stokes Equation
    }
}
