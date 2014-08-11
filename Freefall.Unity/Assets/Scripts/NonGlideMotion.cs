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
		Animator animator = GetComponent<Animator>();
		if(player.Grounded) {
			Vector2 movement = rigidbody2D.velocity;
			if (Mathf.Abs(movement.magnitude) >= float.Epsilon) {
				animator.SetFloat("GroundSpeed", 1);
			} else {
				animator.SetFloat("GroundSpeed", 0);
			}

			Move(groundAcceleration, groundDeceleration);
		} else {
			animator.SetFloat("GroundSpeed", 0);
			Move(airAcceleration, airDeceleration);
		}
	}

	public void Move(float acceleration, float deceleration) {
		Vector2 movement = rigidbody2D.velocity;

		if(Input.GetAxis("X-Axis") < 0 && rigidbody2D.velocity.x > -maxNonGlideSpeed) {
			movement.x -= acceleration;
		}
		if(Input.GetAxis("X-Axis") > 0 && rigidbody2D.velocity.x < maxNonGlideSpeed) {
			movement.x += acceleration; 
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

		rigidbody2D.velocity = movement;

        EnforceMaximumSpeed(ref movement);
	}

    private void EnforceMaximumSpeed(ref Vector2 movement)
    {
        if (movement.x > maxNonGlideSpeed)
        {
            movement.x = maxNonGlideSpeed;
        }
        if (movement.x < -maxNonGlideSpeed)
        {
            movement.x = -maxNonGlideSpeed;
        }
    }
}
