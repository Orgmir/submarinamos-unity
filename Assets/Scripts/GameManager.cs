using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	PlayerController playerController;
	CloudSpawner cloudSpawner;
	ObstacleSpawner obstacleSpawner;

	public static bool gameHasStarted;

	void Awake() {
		if (instance == null) 
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		GameObject player = GameObject.FindWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();
		cloudSpawner = GetComponentInChildren<CloudSpawner> ();	
		obstacleSpawner = GetComponentInChildren<ObstacleSpawner> ();
	}

	void Start () {
		StartGame ();
	}

	void Update () {
		if (!gameHasStarted && Input.GetMouseButton (0)) {
			gameHasStarted = true;
		}
	}

	void ResetGame() {
		gameHasStarted = false;
		SceneManager.LoadScene ("Main");
	}

	public static void EndGame() {
		instance.ResetGame();
	}

	void StartGame() {
		cloudSpawner.StartSpawn ();
		obstacleSpawner.StartSpawn ();
	}
}
