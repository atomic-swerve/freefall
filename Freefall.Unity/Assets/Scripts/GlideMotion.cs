using UnityEngine;
using System.Collections;

public class GlideMotion : MonoBehaviour {
	public float maxGlideSpeedBase = 80f;
	public float glideAcceleration = 10f;
	public float glideDeceleration = 3f;

	private PlayerController player;
	private PlayerGravity playerGravity;
	private PlayerWindMotion playerWindMotion;

	// These values are used for deciding whether or not to apply deceleration. Deceleration is never applied against wind, and is only
	// applied between a player releasing directional button and the player's velocity reaching zero.
	public bool EnableDecelerationX { get; set; }
	public bool EnableDecelerationY { get; set; }

	void Awake() {
		player = GetComponent<PlayerController>();
		playerGravity = GetComponent<PlayerGravity>();
		playerWindMotion = GetComponent<PlayerWindMotion>();
	}

	void Start() {
		EnableDecelerationX = false;
		EnableDecelerationY = false;
	}

	public void HandleGlideInput() {
		if(Input.GetButtonDown("A")) { // If player is not grounded, then the "A" button activates glide mode.
			if(!player.Gliding) {
				ActivateGlide();
			} else {
				DeactivateGlide();
			}
		}
	}

	public void ActivateGlide() {
		playerGravity.DisableGravity();
		EnableDecelerationY = true;
		EnableDecelerationX = true;
		player.Gliding = true;
	}

	public void DeactivateGlide() {
		player.Gliding = false;
	}

	public void HandleGlideMovement() {
		Vector2 movement = rigidbody2D.velocity;

		// Accelerate on any axis receiving input.
		if(Input.GetAxis("X-Axis") < 0 && rigidbody2D.velocity.x > -maxGlideSpeedBase) {
			EnableDecelerationX = true;
			movement.x -= glideAcceleration;
		}
		if(Input.GetAxis("X-Axis") > 0 && rigidbody2D.velocity.x < maxGlideSpeedBase) {
			EnableDecelerationX = true;
			movement.x += glideAcceleration; 
		}
		if(Input.GetAxis("Y-Axis") > 0 && rigidbody2D.velocity.y < maxGlideSpeedBase && !playerWindMotion.InWindlessArea) {
			EnableDecelerationY = true;
			movement.y += glideAcceleration;
		}
		if(Input.GetAxis("Y-Axis") < 0 && rigidbody2D.velocity.y > -maxGlideSpeedBase) {
			EnableDecelerationY = true;
			movement.y -= glideAcceleration; 
		}

		// Decelerate on any axis receiving no input.
		if(Input.GetAxis("X-Axis") == 0 && EnableDecelerationX) {
			if(movement.x < 0 && playerWindMotion.WindModifierX >= 0) {
				movement.x += glideDeceleration;
				if(movement.x > 0) {
					EnableDecelerationX = false;
					movement.x = 0;
				}
			}
			if(movement.x > 0 && playerWindMotion.WindModifierX <= 0) {
				movement.x -= glideDeceleration;
				if(movement.x < 0) {
					EnableDecelerationX = false;
					movement.x = 0;
				}
			}
		}
		if(Input.GetAxis("Y-Axis") == 0 && EnableDecelerationY) {
			if(movement.y < 0 && playerWindMotion.WindModifierY >= 0) {
				movement.y += glideDeceleration;
				if(movement.y > 0) {
					EnableDecelerationY = false;
					movement.y = 0;
				}
			}
			if(movement.y > 0 && playerWindMotion.WindModifierY <= 0) {
				movement.y -= glideDeceleration;
				if(movement.y < 0) {
					EnableDecelerationY = false;
					movement.y = 0;
				}
			}
		}

		playerWindMotion.ApplyWindEffect(ref movement);

        EnforceMaximumSpeed(ref movement, maxGlideSpeedBase + playerWindMotion.WindModifierX, maxGlideSpeedBase + playerWindMotion.WindModifierY);

		rigidbody2D.velocity = movement;
	}

    private void EnforceMaximumSpeed(ref Vector2 movement, float maxGlideSpeedX, float maxGlideSpeedY) {
        if(movement.x > maxGlideSpeedX) {
            movement.x = maxGlideSpeedX;
        }
        if(movement.x < -maxGlideSpeedX) {
            movement.x = -maxGlideSpeedX;
        }
        if(movement.y > maxGlideSpeedY) {
            movement.y = maxGlideSpeedY;
        }
        if(movement.y < -maxGlideSpeedY) {
            movement.y = -maxGlideSpeedY;
        }
    }
}
