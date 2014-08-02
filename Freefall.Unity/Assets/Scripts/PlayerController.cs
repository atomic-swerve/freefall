using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float maxGlideSpeed = 20f;

	private bool airborne = true;
	private float glideSpeedIncrement = .5f;

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

		if(Input.GetKey(KeyCode.LeftArrow) && -rigidbody2D.velocity.x < maxGlideSpeed) {
			movement.x -= glideSpeedIncrement; 
		}
		if(Input.GetKey(KeyCode.RightArrow) && rigidbody2D.velocity.x < maxGlideSpeed) {
			movement.x += glideSpeedIncrement; 
		}
		if(Input.GetKey(KeyCode.UpArrow) && rigidbody2D.velocity.y < maxGlideSpeed) {
			movement.y += glideSpeedIncrement; 
		}
		if(Input.GetKey(KeyCode.DownArrow) && -rigidbody2D.velocity.x < maxGlideSpeed) {
			movement.y -= glideSpeedIncrement; 
		}

		if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow)
			&& !Input.GetKey(KeyCode.DownArrow)) {
			if(movement.x < 0) {
				movement.x += glideSpeedIncrement;
			}
			if(movement.x > 0) {
				movement.x -= glideSpeedIncrement;
			}
			if(movement.y < 0) {
				movement.y += glideSpeedIncrement;
			}
			if(movement.y > 0) {
				movement.y -= glideSpeedIncrement;
			}
		}
		rigidbody2D.velocity = movement;
	}
}
