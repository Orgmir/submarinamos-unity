using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	public float minX = -0.5f;
	public float maxX = 1f;
    public GameObject[] obstacles;

    public string seed;
    public bool useRandomSeed;

	private GameObject container;
    
	void Awake() {
		container = new GameObject ();
		container.name = "Obstacle Container";
    }

	public void GenerateObstacles (float distance) {
		if (useRandomSeed){
            seed = Time.time.ToString();
        }

        System.Random random = new System.Random(seed.GetHashCode());

        float lastXPosition = 0;
        do {
            //TODO generate random position
            //TODO save lastX
            //Spawn object
        }while( lastXPosition < distance);
	}

	void SpawnObstacle(Vector3 pos) {
        GameObject prefab = obstacles[Random.Range(0, obstacles.Length)];
        GameObject obj = GameObject.Instantiate(prefab) as GameObject;
        obj.transform.position = pos;
        obj.transform.parent = container.transform;
    }

    // void SpawnSingle(GameObject obj, float lastX)
    // {
    //     Vector2 pos = new Vector2(lastXPos + Random.Range(minX, maxX), 0);
    //     GameObject childObstacle = Instantiate(obj, pos, Quaternion.identity) as GameObject;
    //     childObstacle.transform.parent = container.transform;
    // }

}
