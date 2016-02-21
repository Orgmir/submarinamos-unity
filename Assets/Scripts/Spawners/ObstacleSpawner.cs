using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public float minX = -0.5f;
	public float maxX = 1f;
    public TopColumnController[] topColumns;
    public BottomColumnController[] botColumns;
    public GameObjectPool[] topPools;
    public GameObjectPool[] botPools;

	private GameObject container;
    // private float lastXPos;

	void Awake() {
		container = new GameObject ();
		container.name = "Obstacle Container";

        topPools = new GameObjectPool[topColumns.Length];
        botPools = new GameObjectPool[botColumns.Length];
        
        for(var i = 0; i < topColumns.Length; i++)
        {
            topPools[i] = new GameObjectPool(topColumns[i].gameObject);
        }
        for (var i = 0; i < botColumns.Length; i++)
        {
            botPools[i] = new GameObjectPool(botColumns[i].gameObject);
        }
    }

	public void StartSpawn () {
		InvokeRepeating ("SpawnObstacle", 1f, 1f);
	}

	void SpawnObstacle() {
        //GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];
        //Vector2 pos = new Vector2(lastXPos + Random.Range(minX, maxX), 0);
        //GameObject childObstacle = Instantiate(obstacle, pos, Quaternion.identity) as GameObject;
        //childObstacle.transform.parent = container.transform;
        //lastXPos = pos.x;

        // Vector2 pos = new Vector2(lastXPos + Random.Range(minX, maxX), 0);
        // GameObject obj = botPools[Random.Range(0, botPools.Length)].Dequeue(pos);

        // Vector2 pos = new Vector2(lastXPos + Random.Range(minX, maxX), 0);
        // GameObject childObstacle = Instantiate(obstacle, pos, Quaternion.identity) as GameObject;
        // childObstacle.transform.parent = container.transform;
        // lastXPos = pos.x;
    }

    // void SpawnSingle(GameObject obj, float lastX)
    // {
    //     Vector2 pos = new Vector2(lastXPos + Random.Range(minX, maxX), 0);
    //     GameObject childObstacle = Instantiate(obj, pos, Quaternion.identity) as GameObject;
    //     childObstacle.transform.parent = container.transform;
    // }

}
