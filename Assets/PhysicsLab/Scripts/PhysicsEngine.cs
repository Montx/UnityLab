using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public float mass;          // [kg]
    public Vector3 netForce;    // N [kg m s^-2]
    public Vector3 velocity;    // [m s^-2]

    public Vector3 angularVelocity;

    private List<Vector3> forces = new List<Vector3>();

    // Update is called once per frame
    void FixedUpdate()
    {
        AddForces();
        UpdateVelocity();
        UpdatePosition();

        UpdateRotation();
    }

    public void AddForce(Vector3 force) {
        forces.Add(force);
    }

    void AddForces() {
        netForce = Vector3.zero;
        foreach (Vector3 force in forces) {
            netForce += force;
        }

        forces.Clear();
    }

    void UpdateVelocity() {
        Vector3 accelaration = netForce / mass;
        velocity += accelaration * Time.deltaTime;
    }

    void UpdatePosition() {
        transform.position += velocity * Time.deltaTime;
    }

    void UpdateRotation() {
        Vector3 angularDisplacement = angularVelocity * Time.deltaTime;
        Vector3 newRotation = transform.rotation.eulerAngles + angularDisplacement;
        Quaternion qNewRotation = new Quaternion(0, newRotation.x, newRotation.y, newRotation.z);
        qNewRotation = transform.rotation * qNewRotation * Quaternion.Inverse(transform.rotation);

        transform.rotation = qNewRotation;
    }
}
