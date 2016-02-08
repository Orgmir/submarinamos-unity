using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	public float minMargin = 0.5f;
	public float maxMargin = 2f;
	public GameObject cloud;

	private float lastXPos;
	private int maxNumberSpawns = 30;
	private int spawnCounter = 0;

	private GameObject container;

	void Awake() {
		container = new GameObject ();
		container.name = "Cloud Container";
	}

	public void StartSpawn() {
		Spawn ();
		InvokeRepeating ("Spawn", 1f, 1f);
	}

	void Spawn() {
		if (spawnCounter >= maxNumberSpawns)
			return;
		Vector2 pos = new Vector2 (lastXPos + Random.Range (minMargin, maxMargin), transform.position.y);
		GameObject childCloud = Instantiate (cloud, pos, Quaternion.identity) as GameObject;
		childCloud.transform.parent = container.transform;
		lastXPos = pos.x;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
