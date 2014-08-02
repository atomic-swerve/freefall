using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float maxGlideSpeed = 7f;
	public float glideSpeedIncrement = .8f;
	public float glideSpeedDecrement = .3f;

	private bool airborne = true;


	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if(airborne) {
			Glide();
		}
	}

	private void Glide() {
		Vector2 movement = rigidbody2D.velocity;

		if(Input.GetAxis("X-Axis") < 0 && -rigidbody2D.velocity.x < maxGlideSpeed) {
			movement.x -= glideSpeedIncrement;
			if(movement.x < -maxGlideSpeed) {
				movement.x = -maxGlideSpeed;
			} 
		}
		if(Input.GetAxis("X-Axis") > 0 && rigidbody2D.velocity.x < maxGlideSpeed) {
			movement.x += glideSpeedIncrement; 
			if(movement.x > maxGlideSpeed) {
				movement.x = maxGlideSpeed;
			} 
		}

		if(Input.GetAxis("Y-Axis") > 0 && rigidbody2D.velocity.y < maxGlideSpeed) {
			movement.y += glideSpeedIncrement;
			if(movement.y > maxGlideSpeed) {
				movement.y = maxGlideSpeed;
			}  
		}
		if(Input.GetAxis("Y-Axis") < 0 && -rigidbody2D.velocity.y < maxGlideSpeed) {
			movement.y -= glideSpeedIncrement; 
			if(movement.y < -maxGlideSpeed) {
				movement.y = -maxGlideSpeed;
			} 
		}

		if(Input.GetAxis("X-Axis") == 0 && Input.GetAxis("Y-Axis") == 0) {
			if(movement.x < 0) {
				if(movement.x + glideSpeedDecrement > 0) {
					movement.x = 0;
				} else {
					movement.x += glideSpeedDecrement;
				}
			}
			if(movement.x > 0) {
				if(movement.x - glideSpeedDecrement < 0) {
					movement.x = 0;
				} else {
					movement.x -= glideSpeedDecrement;
				}
			}
			if(movement.y < 0) {
				if(movement.y + glideSpeedDecrement > 0) {
					movement.y = 0;
				} else {
					movement.y += glideSpeedDecrement;
				}
			}
			if(movement.y > 0) {
				if(movement.y - glideSpeedDecrement < 0) {
					movement.y = 0;
				} else {
					movement.y -= glideSpeedDecrement;
				}
			}
		}
		rigidbody2D.velocity = movement;
	}
}
