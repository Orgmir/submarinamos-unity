using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

	public float horizontalSpeed = 3f;
	public Transform target;

	void Update() {
		float step = horizontalSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
	}
}
