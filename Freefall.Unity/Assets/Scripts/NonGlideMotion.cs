using UnityEngine;
using System.Collections;

public class NonGlideMotion : MonoBehaviour {
	public float maxNonGlideSpeed = 80f; // "NonGlide" motion is all non-gliding movement (so while either grounded or in free fall).

	public float groundAcceleration = 20f;
	public float groundDeceleration = 3f;

	public float airAcceleration = 15f;
	public float airDeceleration = 3f;

	private PlayerController player;
	private PlayerWindMotion playerWindMotion;

	void Awake() {
		player = GetComponent<PlayerController>();
		playerWindMotion = GetComponent<PlayerWindMotion>();
	}

	public void HandleNonGlideMovement() {
		if(player.Grounded) {
			Move(groundAcceleration, groundDeceleration);
		} else {
			Move(airAcceleration, airDeceleration);
		}
	}

	public void Move(float acceleration, float deceleration) {
		Vector2 movement = rigidbody2D.velocity;

		if(Input.GetAxis("X-Axis") < 0 && rigidbody2D.velocity.x > -maxNonGlideSpeed) {
			movement.x -= acceleration;
			if(movement.x < -maxNonGlideSpeed) {
				movement.x = -maxNonGlideSpeed;
			} 
		}
		if(Input.GetAxis("X-Axis") > 0 && rigidbody2D.velocity.x < maxNonGlideSpeed) {
			movement.x += acceleration; 
			if(movement.x > maxNonGlideSpeed) {
				movement.x = maxNonGlideSpeed;
			} 
		}

		if(Input.GetAxis("X-Axis") == 0) {
			if(movement.x < 0) {
				movement.x += deceleration;
				if(movement.x > 0) {
					movement.x = 0;
				}
			}
			if(movement.x > 0) {
				movement.x -= deceleration;
				if(movement.x < 0) {
					movement.x = 0;
				}
			}
		}

		if(!player.Grounded) {
			playerWindMotion.ApplyWind(ref movement);
		}

		rigidbody2D.velocity = movement;
	}
}
