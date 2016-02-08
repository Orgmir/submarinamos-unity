using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float horizontalSpeed = 5f;
	public float moveForce = 10f;
	public float maxSpeed = 5f;

    public float knockbackForce = 10f;

	private Rigidbody2D rb2d;
    private bool isInWater;

	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

    void Update()
    {
        isInWater = transform.position.y <= GameManager.GetWaterLevel();
    }

	void FixedUpdate() {
		if (GameManager.gameHasStarted) {
			DoMovement();
		}
	}

	public void DoMovement() {
        if (isInWater)
        {
            rb2d.velocity = new Vector2(horizontalSpeed, rb2d.velocity.y);
        }
	}

	void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Column")
        {
            CollisionWithColumn(other);
        }
    }

    void CollisionWithColumn(Collision2D other)
    {
        //GameManager.EndGame();

        //Vector3 direction = rb2d.velocity.normalized * -1f; //oposite direction
        //rb2d.velocity = Vector2.zero;
        //rb2d.AddForce(direction * knockbackForce);
    }
}
