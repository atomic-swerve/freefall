using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float defaultGravity = 40f;

	private bool crouching = false;

	// Motion Controls
	private GlideMotion glideMotion;
	private NonGlideMotion nonGlideMotion;
	private JumpMotion jumpMotion;

	// Contains logic to check if player is touching ground.
	private GroundChecker groundChecker;

	public bool Gliding { get; set; }
	public bool Jumping { get; set; }
	public bool Grounded {get; set; }

	// Use this for initialization
	void Awake () {
		Gliding = true;

		glideMotion = transform.GetComponent<GlideMotion>();
		nonGlideMotion = transform.GetComponent<NonGlideMotion>();
		jumpMotion = transform.GetComponent<JumpMotion>();

		groundChecker = transform.GetComponent<GroundChecker>();
	}
	
	// Update is called once per frame
	void Update () {
		Grounded = groundChecker.checkGrounded();
		
		if(Grounded) {
			DisableGravity();
			DisableVerticalVelocity();
			glideMotion.DeactivateGlide();

			jumpMotion.HandleJumpInput();
			HandleCrouchInput();
		} else {
			// Enable gravity for free fall.
			if(!Gliding) {
				EnableGravity();
			}

			glideMotion.HandleGlideInput();
		}
	}

	void FixedUpdate() {	
		if(Gliding) {
			glideMotion.Glide();
		} else {
			nonGlideMotion.Move();
		} 

		if(Jumping) {
			EnableGravity();
			jumpMotion.Jump();
			Jumping = false;
		}
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

	public void EnableGravity() {
		rigidbody2D.gravityScale = defaultGravity;
	}

	public void DisableGravity() {
		rigidbody2D.gravityScale = 0;
	}

	private void DisableVerticalVelocity() {
		Vector2 newVelocity = new Vector2(rigidbody2D.velocity.x, 0);
		rigidbody2D.velocity = newVelocity;
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


}
