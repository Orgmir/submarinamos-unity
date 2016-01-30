using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float horizontalSpeed = 5f;
	public float moveForce = 10f;
	public float maxSpeed = 5f;

	private Rigidbody2D rb2d;

	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		if (GameManager.gameHasStarted) {
			DoMovement();
		}
	}

	public void DoMovement() {
		rb2d.velocity = new Vector2 (horizontalSpeed, rb2d.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D other) {
		GameManager.EndGame ();
	}
}
