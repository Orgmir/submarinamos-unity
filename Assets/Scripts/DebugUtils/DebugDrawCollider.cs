using UnityEngine;
using System.Collections;

public class DebugDrawCollider : MonoBehaviour {

	private BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
	}

	void Update() {
		var verticalOffset = boxCollider.bounds.size.y * 0.5f;
		var horizontalOffset = boxCollider.bounds.size.x * 0.5f;
		var leftTop = transform.TransformVector (Vector3.up * verticalOffset + Vector3.left * horizontalOffset);
		var rightTop = transform.TransformVector (Vector3.up * verticalOffset + Vector3.right * horizontalOffset);
		var leftBot = transform.TransformVector (Vector3.down * verticalOffset + Vector3.left * horizontalOffset);
		var rightBot = transform.TransformVector (Vector3.down * verticalOffset + Vector3.right * horizontalOffset);

		DrawLine (leftTop, rightTop);
		DrawLine (leftBot, rightBot);
		DrawLine (leftTop, leftBot);
		DrawLine (rightTop, rightBot);
	}

	void DrawLine(Vector3 start, Vector3 end) {
		Debug.DrawLine (start, end, Color.green);
	}

}
