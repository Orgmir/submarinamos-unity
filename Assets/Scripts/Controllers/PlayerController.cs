using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float horizontalSpeed = 5f;
	public float moveForce = 10f;
	public float maxSpeed = 5f;
    public float rotationSpeed = .2f;
    public float minAngle = -30f;
    public float maxAngle = 30f;
    public float isInNearSurfaceDelta = 0.5f;
    public int surfaceAverageSamples = 10;
    public float timeUntilFixRotation = 1f;

    public float knockbackForce = 10f;

	private Rigidbody2D rb2d;
    private bool isInWater;

    private Quaternion originalRotation;
    private float[] surfaceWaterDeltas;
    private int deltaCounter = 0;
    private float timeToRotate = 0f;

	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
        surfaceWaterDeltas = new float[surfaceAverageSamples];
	}

    void Start() {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        isInWater = transform.position.y <= GameManager.GetWaterLevel();

        if(Input.GetMouseButton(0)){
            timeToRotate = Time.time + timeUntilFixRotation;
        }

        if (GameManager.gameHasStarted) {
            SaveSurfaceWaterDelta();
            UpdateRotation();
        }
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

    void UpdateRotation() {

        Quaternion rotation;
        if (IsNearSurface() && Time.time > timeToRotate){
            rotation = originalRotation;
        } else {
            rotation = GetCorrectRotation();
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.time * rotationSpeed );
    }

    Quaternion GetCorrectRotation() {
        Vector2 v = rb2d.velocity;
        var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        var normalizedAngle = (angle + 90) / 180;
        var newAngle = (1 - normalizedAngle)*minAngle + normalizedAngle*maxAngle; 
        return Quaternion.AngleAxis(newAngle, Vector3.forward);
    }

    bool IsNearSurface() {
        var delta = CalculateAverageSurfaceDelta();
        return delta <= isInNearSurfaceDelta;
    }

    void SaveSurfaceWaterDelta() {
        surfaceWaterDeltas[deltaCounter++ % surfaceWaterDeltas.Length] = Mathf.Abs(GameManager.GetWaterLevel() - transform.position.y);
    }

    float CalculateAverageSurfaceDelta() {
        float sum = 0f;
        for(int i = 0; i < surfaceWaterDeltas.Length; i++){
            sum += surfaceWaterDeltas[i]; 
        }
        return sum / surfaceWaterDeltas.Length;
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
