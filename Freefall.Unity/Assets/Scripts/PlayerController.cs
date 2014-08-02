using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float defaultGravity = 15f;

	private bool gliding = true;
	private bool grounded = false;
	private bool shouldJump = false;
	
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
				shouldJump = true;
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

		if(shouldJump) {
			jumpMotion.Jump();
			shouldJump = false;
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
}
