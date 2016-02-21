using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class WaterBoxProperties {
	public float boxWidth = 70f;
	public float boxHeight = 7f;
	public float maxSurfaceHeight = 1.5f;
	public float minSurfaceHeight = -1.5f;
}

[Serializable]
public class WaterSpeedProperties {
	public float waterUpSpeed = 0.5f;
	public float waterDownSpeed = 0.5f;
}

public class WaterController : MonoBehaviour {

	public WaterBoxProperties boxProps = new WaterBoxProperties();
	public WaterSpeedProperties speedProps = new WaterSpeedProperties();

	private BuoyancyEffector2D effector;
	private GameObject container;

	private float surfaceCurrentVelocity;

	void Awake() {
		effector = GetComponent<BuoyancyEffector2D> ();
	}

	void Start () {
		container = new GameObject ();
		SpawnWaterTiles ();
		SetupColliderSize ();
		effector.surfaceLevel = -1;
		container.transform.position = new Vector3 (container.transform.position.x, effector.surfaceLevel, container.transform.position.z);
	}

	void Update() {
		if (GameManager.gameHasStarted) {
			UpdateSurfaceLevel ();
		}
	}

	void UpdateSurfaceLevel() {
		bool goingUp = Input.GetMouseButton (0);
		float targetValue =  goingUp ? boxProps.maxSurfaceHeight : boxProps.minSurfaceHeight;
		float speed = goingUp ? speedProps.waterUpSpeed : speedProps.waterDownSpeed;
		effector.surfaceLevel = Mathf.SmoothDamp (effector.surfaceLevel, targetValue, ref surfaceCurrentVelocity, speed);

		container.transform.position = new Vector3 (container.transform.position.x, effector.surfaceLevel, container.transform.position.z);
	}

    public float GetWaterLevel()
    {
        return effector.surfaceLevel;    
    }

    void SetupColliderSize() {
		BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
		boxCollider.size = new Vector2 (boxProps.boxWidth, boxProps.boxHeight);
		boxCollider.offset = new Vector2 (boxCollider.size.x * 0.5f, -boxProps.maxSurfaceHeight);
	}

	private void SpawnWaterTiles() {
		SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		container.name = "WaterTilesContainer";

		GameObject childPrefab = new GameObject ();
		childPrefab.transform.position = transform.position;

		SpriteRenderer childRenderer = childPrefab.AddComponent<SpriteRenderer> ();
		childRenderer.sprite = spriteRenderer.sprite;
		childRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
		childRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
		childRenderer.sortingOrder = spriteRenderer.sortingOrder;

		float spriteWidth = spriteRenderer.bounds.size.x;
		float spriteHeight = spriteRenderer.bounds.size.y;
		int horizontalSprites = (int) Mathf.Round(boxProps.boxWidth / spriteWidth);
		int verticalSprites = (int) Mathf.Round(boxProps.boxHeight / spriteHeight);
		for (int i = -1; i < horizontalSprites + 1; i++) {
			for (int j = 0; j < verticalSprites + 1; j++) {
				GameObject child = Instantiate (childPrefab) as GameObject;
				child.transform.position = transform.position + new Vector3(spriteWidth * i, spriteHeight * -j, 0);
				child.transform.parent = container.transform;
			}
		}
			
		container.transform.parent = transform;
		spriteRenderer.enabled = false;

		Destroy (childPrefab);
	}
}

