using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player;
	public float smoothTime = .3f;

	private Vector3 velocity;
	private Vector3 originalOffset;

	void Start()  {
		originalOffset = transform.position;
	}

	void Update () {
		Vector3 targetVector = new Vector3 (player.transform.position.x + originalOffset.x, 0, transform.position.z);
		transform.position = Vector3.SmoothDamp (transform.position, targetVector, ref velocity, smoothTime);
	}
}
