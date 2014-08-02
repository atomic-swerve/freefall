using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float defaultGravity = 15f;

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
			if(gliding) {
				DeactivateGlide();
			}
			if(Input.GetButtonDown("A")) {
				jumping = true;
			}
			if(Input.GetAxis("Y-Axis") < 0) {
				if(!crouching) {
					ActivateCrouch();
				}
			} else {
				if(crouching) {
					DeactivateCrouch();
				}
			}
			if(Input.GetAxis("X-Axis") != 0) {
				DeactivateCrouch();
			}
		} else {
			if(Input.GetButtonDown("A")) { // If player is not grounded, then the "A" button activates glide mode.
				ActivateGlide();
			}
		}
	}

	void FixedUpdate() {
		if(gliding) {
			glideMotion.Glide();
		} else {
			nonGlideMotion.Move();
		} 

		if(jumping) {
			jumpMotion.Jump();
			jumping = false;
		}
	}

	private void ActivateGlide() {
		rigidbody2D.gravityScale = 0;
		gliding = true;
	}

	private void DeactivateGlide() {
		rigidbody2D.gravityScale = defaultGravity;
		gliding = false;
	}

	public void ActivateCrouch() {
		Debug.Log("Activating crouch.");
		crouching = true;
		// Do all other crouch-related logic here
	}

	public void DeactivateCrouch() {
		Debug.Log("Deactivating crouch.");
		crouching = false;
		// Do all other crouch-related logic here
	}
}
