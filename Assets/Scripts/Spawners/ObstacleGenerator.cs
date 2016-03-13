using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	public float minOffsetX = -0.5f;
	public float maxOffsetX = 1f;
    public float minOffsetY = 0f;
    public float maxOffsetY = 0.6f;
    public float startingX = 5f;
    public float startingYOffset = 1.5f;
    public TopColumnController[] topColumns;
    public BottomColumnController[] botColumns;

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
 
        Vector2 cameraSize = CameraUtils.GetCameraSize();
        Debug.Log("Camera size: " + cameraSize);
        float lastXPosition = startingX;
        float spawnY = cameraSize.y - startingYOffset;
        do {
            float x = lastXPosition + Random.Range(minOffsetX, maxOffsetX);

            GameObject topPrefab = topColumns[Random.Range(0, topColumns.Length)].gameObject;
            SpawnColumn(x, spawnY, topPrefab);

            GameObject botPrefab = botColumns[Random.Range(0, botColumns.Length)].gameObject;
            SpawnColumn(x, spawnY, botPrefab);           

            lastXPosition = x;
        }while( lastXPosition < distance);
	}

    void SpawnColumn(float x, float spawnY, GameObject prefab) {
        // float y = Random.Range(minOffsetY, maxOffsetY) * Mathf.Sign(Random.Range(-1, 1));
        float y = spawnY + Random.Range(minOffsetY, maxOffsetY);
        
        ColumnController controller = prefab.GetComponent<ColumnController>();
        ColumnController.ColumnType type = controller.type;
        if (type == ColumnController.ColumnType.Top) {
            y = -y;
        }
        
        SpawnObstacle(prefab, x, y);
    }

	void SpawnObstacle(GameObject prefab, float x, float y) {        
        GameObject obj = GameObject.Instantiate(prefab) as GameObject;
        obj.transform.position = new Vector3(x, y, 0);
        obj.transform.parent = container.transform;
    }

    void ClearContainer() {
        for( int i = 0; i < container.transform.childCount; i++) {
            GameObject child = container.transform.GetChild(i).gameObject;
            GameObject.Destroy(child);
        }
    }

}
