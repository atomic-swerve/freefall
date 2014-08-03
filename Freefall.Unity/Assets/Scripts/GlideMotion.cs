﻿using UnityEngine;
using System.Collections;

public class GlideMotion : MonoBehaviour {
	public float maxGlideSpeed = 80f;
	public float glideAcceleration = 10f;
	public float glideDeceleration = 3f;

	public void Glide() {
		Vector2 movement = rigidbody2D.velocity;

		// Accelerate on any axis receiving input.
		if(Input.GetAxis("X-Axis") < 0 && rigidbody2D.velocity.x > -maxGlideSpeed) {
			movement.x -= glideAcceleration;
			if(movement.x < -maxGlideSpeed) {
				movement.x = -maxGlideSpeed;
			} 
		}
		if(Input.GetAxis("X-Axis") > 0 && rigidbody2D.velocity.x < maxGlideSpeed) {
			movement.x += glideAcceleration; 
			if(movement.x > maxGlideSpeed) {
				movement.x = maxGlideSpeed;
			} 
		}
		if(Input.GetAxis("Y-Axis") > 0 && rigidbody2D.velocity.y < maxGlideSpeed) {
			movement.y += glideAcceleration;
			if(movement.y > maxGlideSpeed) {
				movement.y = maxGlideSpeed;
			}  
		}
		if(Input.GetAxis("Y-Axis") < 0 && rigidbody2D.velocity.y > -maxGlideSpeed) {
			movement.y -= glideAcceleration; 
			if(movement.y < -maxGlideSpeed) {
				movement.y = -maxGlideSpeed;
			} 
		}

		// Decelerate on any axis receiving no input.
		if(Input.GetAxis("X-Axis") == 0) {
			if(movement.x < 0) {
				movement.x += glideDeceleration;
				if(movement.x > 0) {
					movement.x = 0;
				}
			}
			if(movement.x > 0) {
				movement.x -= glideDeceleration;
				if(movement.x < 0) {
					movement.x = 0;
				}
			}
		}
		if(Input.GetAxis("Y-Axis") == 0) {
			if(movement.y < 0) {
				movement.y += glideDeceleration;
				if(movement.y > 0) {
					movement.y = 0;
				}
			}
			if(movement.y > 0) {
				movement.y -= glideDeceleration;
				if(movement.y < 0) {
					movement.y = 0;
				}
			}
		}

		rigidbody2D.velocity = movement;
	}
}