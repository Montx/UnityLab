using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
    public Vector2 _worldBounds { get; private set; }

    public Vector2 WrapAround(Vector2 position) {
        if (position.x < -_worldBounds.x) {
            position.x = _worldBounds.x;
        } else if (position.x > _worldBounds.x) {
            position.x = -_worldBounds.x;
        }

        if (position.y < -_worldBounds.y) {
            position.y = _worldBounds.y;
        } else if (position.y > _worldBounds.y) {
            position.y = -_worldBounds.y;
        }

        return position;
    }

    void Start()
    {
        _worldBounds = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));

        GetComponent<ISpacePartitioner>().DivideSpace(_worldBounds);
    }

    private void OnDrawGizmos() {
        DrawWorldBounds();
    }

    void DrawWorldBounds() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-_worldBounds.x, _worldBounds.y, 0), new Vector3(_worldBounds.x, _worldBounds.y, 0));
        Gizmos.DrawLine(new Vector3(-_worldBounds.x, -_worldBounds.y, 0), new Vector3(_worldBounds.x, -_worldBounds.y, 0));
        Gizmos.DrawLine(new Vector3(_worldBounds.x, _worldBounds.y, 0), new Vector3(_worldBounds.x, -_worldBounds.y, 0));
        Gizmos.DrawLine(new Vector3(-_worldBounds.x, _worldBounds.y, 0), new Vector3(-_worldBounds.x, -_worldBounds.y, 0));
    }
}
