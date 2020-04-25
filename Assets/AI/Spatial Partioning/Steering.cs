using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public float _detectionRadius;
    public float _maxSpeed;
    public float _mass;
    public float _wanderRadius;
    public float _wanderDistance;
    public float _wanderJitter;

    public Vector2 _velocity;

    WorldManager _worldBounds;
    Vector2 _wanderTarget;
    ISpacePartitioner _cp;

    int _currentIndex;

    private void Awake() {
        _wanderTarget = transform.up * _wanderRadius;
    }

    private void Start() {
        _worldBounds = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        _cp = GameObject.Find("WorldManager").GetComponent<ISpacePartitioner>();

        _currentIndex = _cp.GetIndex(transform.position);
    }

    private void Update() {
        Vector2 topLeft = new Vector3(transform.position.x - _detectionRadius, transform.position.y + _detectionRadius, 0);
        Vector2 bottomRight = new Vector3(transform.position.x + _detectionRadius, transform.position.y - _detectionRadius, 0);

        List<GameObject> neighboors = _cp.GetNeighboors(gameObject, topLeft, bottomRight, _detectionRadius);

        Vector3 detection = new Vector3(_detectionRadius, _detectionRadius, 0);

        foreach (GameObject spaceship in neighboors) {
            Vector3 toNeighboor = (spaceship.transform.position - transform.position);
            float sqrDistance = toNeighboor.sqrMagnitude;
            float sqrDetectionRadius = detection.sqrMagnitude;

            if (sqrDistance > _detectionRadius * _detectionRadius) {
                Debug.Log("Ups");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 netForce = Wander();
        Vector2 accelaration = netForce / _mass;
        _velocity += accelaration * Time.deltaTime;
        transform.up = _velocity.normalized;

        if (_velocity.sqrMagnitude > _maxSpeed * _maxSpeed) {
            _velocity = transform.up * _maxSpeed;
        }

        transform.position += new Vector3(_velocity.x, _velocity.y, 0.0f) * Time.deltaTime ;
        transform.position = _worldBounds.WrapAround(transform.position);

        int i = _cp.GetIndex(transform.position);

        if (i != _currentIndex) {
            _cp.UpdateObject(gameObject, _currentIndex, i);
            _currentIndex = i;
        }
    }

    Vector2 Wander() {
        _wanderTarget += new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * _wanderJitter;
        _wanderTarget = _wanderTarget.normalized * _wanderRadius;

        Vector2 wanderProjection = _wanderTarget + new Vector2(transform.up.x, transform.up.y) * _wanderDistance;

        return (wanderProjection - new Vector2(transform.position.x, transform.position.y));
    }

    private void OnDrawGizmos() {
        /*
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, _wanderRadius);
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position + transform.up * _wanderDistance, transform.forward, _wanderRadius);
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawLine(transform.position + transform.up * _wanderRadius, transform.position + transform.up * _wanderDistance);
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawSolidDisc(
            new Vector3(
                (transform.position + transform.up * _wanderDistance).x + _wanderTarget.x,
                (transform.position + transform.up * _wanderDistance).y + _wanderTarget.y, 
                transform.position.z), 
            transform.forward, 
            0.1f);
        */

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, _detectionRadius);

        Vector2 topLeft = new Vector3(transform.position.x - _detectionRadius, transform.position.y + _detectionRadius, 0);
        Vector2 topRight = new Vector3(transform.position.x + _detectionRadius, transform.position.y + _detectionRadius, 0);
        Vector2 bottomLeft = new Vector3(transform.position.x - _detectionRadius, transform.position.y - _detectionRadius, 0);
        Vector2 bottomRight = new Vector3(transform.position.x + _detectionRadius, transform.position.y - _detectionRadius, 0);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(topLeft.x, topLeft.y, 0), new Vector3(topRight.x, topRight.y, 0));
        Gizmos.DrawLine(new Vector3(bottomLeft.x, bottomLeft.y, 0), new Vector3(bottomRight.x, bottomRight.y, 0));
        Gizmos.DrawLine(new Vector3(topLeft.x, topLeft.y, 0), new Vector3(bottomLeft.x, bottomLeft.y, 0));
        Gizmos.DrawLine(new Vector3(topRight.x, topRight.y, 0), new Vector3(bottomRight.x, bottomRight.y, 0));
    }
}
