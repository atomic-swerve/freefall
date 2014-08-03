using UnityEngine;
using System.Collections;

public class NonGlideMotion : MonoBehaviour {
	public float maxNonGlideSpeed = 80f; // "NonGlide" motion is all non-gliding movement (so while either grounded or in free fall).
	public float nonGlideAcceleration = 20f;
	public float nonGlideDeceleration = 3f;

	public void Move() {
		Vector2 movement = rigidbody2D.velocity;

		if(Input.GetAxis("X-Axis") < 0 && rigidbody2D.velocity.x > -maxNonGlideSpeed) {
			movement.x -= nonGlideAcceleration;
			if(movement.x < -maxNonGlideSpeed) {
				movement.x = -nonGlideAcceleration;
			} 
		}
		if(Input.GetAxis("X-Axis") > 0 && rigidbody2D.velocity.x < maxNonGlideSpeed) {
			movement.x += nonGlideAcceleration; 
			if(movement.x > maxNonGlideSpeed) {
				movement.x = nonGlideAcceleration;
			} 
		}

		if(Input.GetAxis("X-Axis") == 0) {
			if(movement.x < 0) {
				movement.x += nonGlideDeceleration;
				if(movement.x > 0) {
					movement.x = 0;
				}
			}
			if(movement.x > 0) {
				movement.x -= nonGlideDeceleration;
				if(movement.x < 0) {
					movement.x = 0;
				}
			}
		}

		rigidbody2D.velocity = movement;
	}
}
