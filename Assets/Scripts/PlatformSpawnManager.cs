using UnityEngine;
using System.Collections;

public class PlatformSpawnManager : MonoBehaviour {

	public int maxPlatforms = 20;
	public float verticalSpawnOffset = 1f;
	public float minHorizontalStep = 0.7f;
	public float maxHorizontalStep = 1.5f;
	public float horizontalVariance = 0.5f;
	public float verticalMin = -1;
	public float verticalMax = 1;
	public GameObject[] topPlatforms;
	public GameObject[] bottomPlatforms;

	private float originStep;

	void Start () {
		originStep = transform.position.x;
		Spawn ();
	}

	void Spawn() {

		for (int i = 0; i < maxPlatforms; i++) 
		{
			float step = originStep + Random.Range (minHorizontalStep, maxHorizontalStep);
			originStep = step;

			Vector2 topPos = RandomPosition(step, verticalSpawnOffset);
			Vector2 bottomPos = RandomPosition (step, -verticalSpawnOffset);

			InstantiatePlatform (topPlatforms, topPos);
			InstantiatePlatform (bottomPlatforms, bottomPos);
		}
	}

	void InstantiatePlatform(GameObject[] platforms, Vector2 pos) {
		GameObject platform = platforms [Random.Range (0, platforms.Length)];
		var renderer = platform.GetComponent<SpriteRenderer> ();
		float height = renderer.bounds.size.y;
		Vector2 correctPos = pos + new Vector2 (0, height * 0.5f * Mathf.Sign(pos.y));
		Instantiate (platform, correctPos, Quaternion.identity);
	}

	Vector2 RandomPosition(float step, float verticalOffset) {
		return new Vector2 (step + Random.Range (-horizontalVariance, horizontalVariance), 
			verticalOffset + Random.Range (verticalMin, verticalMax));
	}
}
