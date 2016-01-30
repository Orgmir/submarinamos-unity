using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public float minX = -0.5f;
	public float maxX = 1f;
	public GameObject[] obstacles;

	private float lastXPos;
	private int maxNumberSpawns = 30;
	private int spawnCounter = 0;

	private GameObject container;

	void Awake() {
		container = new GameObject ();
		container.name = "Obstacle Container";
	}

	public void StartSpawn () {
		lastXPos = 2;
		SpawnObstacle ();
		SpawnObstacle ();
		SpawnObstacle ();
		InvokeRepeating ("SpawnObstacle", 1f, 1f);
	}

	void SpawnObstacle() {
		if (spawnCounter >= maxNumberSpawns)
			return;
		GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];
		Vector2 pos = new Vector2 (lastXPos + Random.Range (minX, maxX), 0);
		GameObject childObstacle = Instantiate (obstacle, pos, Quaternion.identity) as GameObject;
		childObstacle.transform.parent = container.transform;
		lastXPos = pos.x;
	}

}
