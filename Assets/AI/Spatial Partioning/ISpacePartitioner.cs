using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpacePartitioner
{
    void AddObject(GameObject obj);
    int GetIndex(Vector3 position);

    void UpdateObject(GameObject obj, int fromCell, int toCell);

    void DivideSpace(Vector2 worldBounds);

    List<GameObject> GetNeighboors(GameObject obj, Vector2 topLeft, Vector2 bottomRight, float radius);
}
