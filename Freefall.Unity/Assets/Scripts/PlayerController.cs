using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float defaultGravity = 40f;

	private bool gliding = true;
	private bool grounded = false;
	private bool jumping = false;
	private bool crouching = false;
	
	private Transform groundCheck;

	// Motion Controls
	private GlideMotion glideMotion;
	private NonGlideMotion nonGlideMotion;
	private JumpMotion jumpMotion;

	// Use this for initialization
	void Awake () {
		groundCheck = transform.Find("groundCheck");

		glideMotion = transform.GetComponent<GlideMotion>();
		nonGlideMotion = transform.GetComponent<NonGlideMotion>();
		jumpMotion = transform.GetComponent<JumpMotion>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if(grounded) {
			DisableGravity();
			DisableVerticalVelocity();
			DeactivateGlide();

			HandleJumpInput();
			HandleCrouchInput();
		} else {
			// Enable gravity for free fall.
			if(!gliding) {
				EnableGravity();
			}

			HandleGlideInput();
		}
	}

	void FixedUpdate() {	
		if(gliding) {
			glideMotion.Glide();
		} else {
			nonGlideMotion.Move();
		} 

		if(jumping) {
			EnableGravity();
			jumpMotion.Jump();
			jumping = false;
		}
	}

	private void ActivateGlide() {
		DisableGravity();
		DisableVerticalVelocity();
		gliding = true;
	}

	private void DeactivateGlide() {
		gliding = false;
	}

	private void ActivateCrouch() {
		if(!crouching) {
			crouching = true;
			// Do all other crouch-related logic here
		}
	}

	private void DeactivateCrouch() {
		if(crouching) {
			crouching = false;
			// Do all other crouch-related logic here
		}
	}

	private void EnableGravity() {
		rigidbody2D.gravityScale = defaultGravity;
	}

	private void DisableGravity() {
		rigidbody2D.gravityScale = 0;
	}

	private void DisableVerticalVelocity() {
		Vector2 newVelocity = new Vector2(rigidbody2D.velocity.x, 0);
		rigidbody2D.velocity = newVelocity;
	}

	private void HandleJumpInput() {
		if(Input.GetButtonDown("A")) {
				jumping = true;
		}
	}

	private void HandleCrouchInput() {
		if(Input.GetAxis("Y-Axis") < 0) {
			if(!crouching) {
				ActivateCrouch();
			}
		} else {
			DeactivateCrouch();
		}
		if(Input.GetAxis("X-Axis") != 0 && crouching) {
			DeactivateCrouch();
		}
	}

	private void HandleGlideInput() {
		if(Input.GetButtonDown("A")) { // If player is not grounded, then the "A" button activates glide mode.
			if(!gliding) {
				ActivateGlide();
			} else {
				DeactivateGlide();
			}
		}
	}
}
