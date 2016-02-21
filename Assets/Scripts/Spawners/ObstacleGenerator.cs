using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	public float minOffsetX = -0.5f;
	public float maxOffsetX = 1f;
    public float minOffsetY = 2f;
    public float maxOffsetY = 4f;
    public float startingX = 5f;
    public GameObject[] obstacles;

    public string seed;
    public bool useRandomSeed;

	private GameObject container;
    
	void Awake() {
		container = new GameObject ();
		container.name = "Obstacle Container";
    }

	public void GenerateObstacles (float distance) {
        ClearContainer();

		if (useRandomSeed){
            seed = Time.time.ToString();
        }

        Random.seed = seed.GetHashCode();

        float lastXPosition = startingX;
        do {
            float x = lastXPosition + Random.Range(minOffsetX, maxOffsetX);
            float y = Random.Range(minOffsetY, maxOffsetY) * Mathf.Sign(Random.Range(-1, 1));

            SpawnObstacle(lastXPosition, x, y);

            lastXPosition = x;
        }while( lastXPosition < distance);
	}

	void SpawnObstacle(float lastX, float offsetX, float offsetY) {
        GameObject prefab = obstacles[Random.Range(0, obstacles.Length)];
        GameObject obj = GameObject.Instantiate(prefab) as GameObject;
        //TODO get columnScript
        //TODO get camera top and bottom
        //TODO spawn columns in right place with 
        // obj.transform.position = offset;
        obj.transform.parent = container.transform;
    }

    void ClearContainer() {
        for( int i = 0; i < container.transform.childCount; i++) {
            GameObject child = container.transform.GetChild(i).gameObject;
            GameObject.Destroy(child);
        }
    }

}
