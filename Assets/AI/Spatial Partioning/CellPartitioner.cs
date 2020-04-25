using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Cell {
    public Cell() {
        _objects = new List<GameObject>();
    }

    public List<GameObject> _objects;
}

public class CellPartitioner : MonoBehaviour, ISpacePartitioner {

    Vector2 _worldBounds;
    int _totalCells;

    public int _divisions;

    int _lineOfCells;

    float cellWidth;
    float cellHeight;

    private Cell[] cells;

    public void DivideSpace(Vector2 worldBounds) {
        _worldBounds = worldBounds;
        _lineOfCells = (int)Mathf.Pow(2, _divisions);
        _totalCells = _lineOfCells * _lineOfCells;
        cellWidth = (_worldBounds.x * 2.0f) / _lineOfCells;
        cellHeight = (_worldBounds.y * 2.0f) / _lineOfCells;

        cells = new Cell[_totalCells];

        for (int i = 0; i <_totalCells; i++) {
            cells[i] = new Cell();
        }
    }

    public void UpdateObject(GameObject obj, int fromCell, int toCell) {
        foreach (GameObject cObj in cells[fromCell]._objects) {
            if (cObj == obj) {
                cells[fromCell]._objects.Remove(cObj);
                break;
            }
        }

        cells[toCell]._objects.Add(obj);
    }

    public int GetIndex(Vector3 position) {
        Vector2 gridPos = GetGridLocation(position.x, position.y);
        return GetIndex(gridPos.x, gridPos.y);
    }

    public void AddObject(GameObject obj) {
        Vector2 gridPos = GetGridLocation(
            obj.transform.position.x,
            obj.transform.position.y);
        cells[GetIndex(gridPos.x, gridPos.y)]._objects.Add(obj);
    }

    public Vector2 GetGridLocation(float x, float y) {
        int i = (int)((x + _worldBounds.x) * _lineOfCells / (_worldBounds.x * 2.0f));
        int j = Mathf.Abs((int)((y - _worldBounds.y) * _lineOfCells / (_worldBounds.y * 2.0f)));
        return new Vector2(i, j);
    }

    public List<GameObject> GetNeighboors(GameObject obj, Vector2 topLeft, Vector2 bottomRight, float radius) {
        List<GameObject> neighboors = new List<GameObject>();


        for (float y = topLeft.y; y > bottomRight.y; y -= cellHeight) {
            for (float x = topLeft.x; x < bottomRight.x; x += cellWidth) {
                Vector2 gridPos = GetGridLocation(x, y);
                int index = GetIndex(gridPos.x, gridPos.y);

                foreach (GameObject cObj in cells[index]._objects) {
                    float sqrDistance = (cObj.transform.position - obj.transform.position).sqrMagnitude;
                    if (sqrDistance <= radius * radius && cObj != obj) {
                        neighboors.Add(cObj);
                    }
                }
            }
        }

        return neighboors;
    }

    private int GetIndex(float x, float y) {
        return Mathf.Clamp((int)(x + y * _lineOfCells), 0, _totalCells - 1);
    }

    private void OnDrawGizmos() {
        for (int c = 0; c < _lineOfCells; c++) {
            for (int r = 0; r < _lineOfCells; r++) {
                if (cells[GetIndex(c, r)]._objects.Count > 0) {
                    Gizmos.color = Color.green;
                }

                Gizmos.DrawLine(
                    new Vector3(-_worldBounds.x + cellWidth * c, _worldBounds.y - cellHeight * r),
                    new Vector3(-_worldBounds.x + cellWidth * (c + 1), _worldBounds.y - cellHeight * r));
                Gizmos.DrawLine(
                    new Vector3(-_worldBounds.x + cellWidth * c, _worldBounds.y - cellHeight * r),
                    new Vector3(-_worldBounds.x + cellWidth * c, _worldBounds.y - cellHeight * (r + 1)));
                Gizmos.DrawLine(
                    new Vector3(-_worldBounds.x + cellWidth * (c + 1), _worldBounds.y - cellHeight * r),
                    new Vector3(-_worldBounds.x + cellWidth * (c + 1), _worldBounds.y - cellHeight * (r + 1)));
                Gizmos.DrawLine(
                    new Vector3(-_worldBounds.x + cellWidth * c, _worldBounds.y - cellHeight * (r + 1)),
                    new Vector3(-_worldBounds.x + cellWidth * (c + 1), _worldBounds.y - cellHeight * (r + 1)));

                Gizmos.color = Color.white;
            }
        }

    }
}
