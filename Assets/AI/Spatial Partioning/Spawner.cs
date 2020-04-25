using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Vector2 _worldBounds;

    public GameObject spawnedObject;
    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        _worldBounds = GameObject.Find("WorldManager").GetComponent<WorldManager>()._worldBounds;

        ISpacePartitioner sp = GameObject.Find("WorldManager").GetComponent<ISpacePartitioner>();

        for (int i = 0; i < amount; i++) {
            GameObject gameObject = Instantiate<GameObject>(spawnedObject);

            gameObject.transform.position = new Vector3(
                Random.Range(-_worldBounds.x, _worldBounds.x), 
                Random.Range(-_worldBounds.y, _worldBounds.y), 
                0.0f);

            Steering steering = gameObject.GetComponent<Steering>();

            steering._velocity = new Vector2(
                Random.Range(-1.0f, 1.0f),
                Random.Range(-1.0f, 1.0f));

            sp.AddObject(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
