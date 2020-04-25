using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour
{
    private const double gravitationalConstant = 6.673e-11; // N [m^3 s^-2 kg^-1]

    private PhysicsEngine[] physicsEngineArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
        CalculateGravity();
    }

    void CalculateGravity() {
        foreach (PhysicsEngine physicsEngineA in physicsEngineArray) {
            foreach (PhysicsEngine physicsEngineB in physicsEngineArray) {
                if (physicsEngineA == physicsEngineB) { continue; }
                Debug.Log("Calculate gravitational force exerted on " + physicsEngineA.name +
                          " due to gravity of " + physicsEngineB.name);

                Vector3 displacementAToB = physicsEngineB.transform.position - physicsEngineA.transform.position;
                float distanceBetweenBodies = displacementAToB.sqrMagnitude;
                float gravityForceMagnitude = (float)gravitationalConstant * (physicsEngineA.mass * physicsEngineB.mass) / distanceBetweenBodies;
                Vector3 gravityForce = displacementAToB.normalized * gravityForceMagnitude;

                physicsEngineA.AddForce(gravityForce);
            }
        }
    }
}
