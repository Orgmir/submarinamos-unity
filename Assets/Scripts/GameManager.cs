using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public bool generateObstacles = true;

	// PlayerController playerController;
	CloudSpawner cloudSpawner;
	OldObstacleSpawner obstacleSpawner;
    WaterController waterController;

	public static bool gameHasStarted;

	void Awake() {
		if (instance == null) 
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		// GameObject player = GameObject.FindWithTag ("Player");
		// playerController = player.GetComponent<PlayerController> ();
		cloudSpawner = GetComponentInChildren<CloudSpawner> ();	
		obstacleSpawner = GetComponentInChildren<OldObstacleSpawner> ();
        GameObject water = GameObject.FindWithTag("Water");
        waterController = water.GetComponent<WaterController>();
    }

	void Start () {
		StartGame ();
	}

	void Update () {
		if (!gameHasStarted && Input.GetMouseButton (0)) {
			gameHasStarted = true;
		}
	}

	public void ResetGame() {
		gameHasStarted = false;
		SceneManager.LoadScene ("Main");
	}

	public static void EndGame() {
		instance.ResetGame();
	}

    public static void onClick(int value)
    {

    }

	void StartGame() {
		if (generateObstacles) {
			cloudSpawner.StartSpawn ();
			obstacleSpawner.StartSpawn ();	
		}
	}

    public static float GetWaterLevel()
    {
        return instance.waterController.GetWaterLevel();
    }
    
}
