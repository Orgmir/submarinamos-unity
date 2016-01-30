using UnityEngine;
using System.Collections;

public class DebugDrawCollisions : MonoBehaviour {

	void OnCollisionEnter(Collision collision) { 
		foreach (ContactPoint contact in collision.contacts) { 
			Debug.DrawLine(contact.point, contact.point + contact.normal, Color.green, 2, false); 
		} 
	}
}
