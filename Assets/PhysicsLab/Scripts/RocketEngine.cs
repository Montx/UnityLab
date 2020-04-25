using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour
{
    public float fuelMass;              // [kg]
    public float maxThrust;             // kN [kg m s^-2]

    [Range(0, 1f)]
    public float thrustPercent;         // [none]

    public Vector3 thrustUnitVector;    // [none]

    private PhysicsEngine physicsEngine;
    private float currentThrust;        // N

    // Start is called before the first frame update
    void Start()
    {
        physicsEngine = GetComponent<PhysicsEngine>();
        physicsEngine.mass += fuelMass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fuelMass > FuelThisUpdate()) {
            fuelMass -= FuelThisUpdate();
            physicsEngine.mass -= FuelThisUpdate();
            ExtertForce();
        } else {
            Debug.Log("Out of rocket fuel.");
        }
    }

    private float FuelThisUpdate() {
        float exhaustMassFlow = 0;                  // [kg]
        float effectiveExhaustVelocity = 4462f;     // [m s^-1] liquid H O

        exhaustMassFlow = currentThrust / effectiveExhaustVelocity;

        return exhaustMassFlow * Time.deltaTime;
    }

    private void ExtertForce() {
        currentThrust = thrustPercent * maxThrust * 1000f; // N < kN (kN * 1000.0f)
        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust; // N
        physicsEngine.AddForce(thrustVector);
    }
}
