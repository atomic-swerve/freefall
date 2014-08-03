using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float defaultGravity = 40f;

	// Detection of a ground tile within this distance of bottom left/corner/right of collider will cause 
	// checkGrounded() to evaluate to true.
	private float colliderToGroundDistance = .1f;

	private int groundLayerIndex;

	private bool gliding = true;
	private bool grounded = false;
	private bool jumping = false;
	private bool crouching = false;
	
	private Transform groundCheck;
	private BoxCollider2D boxCollider2D;

	// Motion Controls
	private GlideMotion glideMotion;
	private NonGlideMotion nonGlideMotion;
	private JumpMotion jumpMotion;

	// Use this for initialization
	void Awake () {
		groundCheck = transform.Find("groundCheck");
		groundLayerIndex = LayerMask.NameToLayer("Ground");

		glideMotion = transform.GetComponent<GlideMotion>();
		nonGlideMotion = transform.GetComponent<NonGlideMotion>();
		jumpMotion = transform.GetComponent<JumpMotion>();
		boxCollider2D = transform.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = checkGrounded();
		
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

	bool checkGrounded() {
		float halfColliderWidth = (boxCollider2D.size.x * transform.localScale.x) / 2;
		float halfColliderHeight = (boxCollider2D.size.y * transform.localScale.y) / 2;

		// Transform the collider box's coordinates from local space to world space
		Vector3 boxCenter3D = transform.TransformPoint(boxCollider2D.center);

		Vector2 boxCenter = new Vector2(boxCenter3D.x, boxCenter3D.y);
		Vector2 boxLeftEdge = new Vector2(boxCenter3D.x - halfColliderWidth, boxCenter3D.y);
		Vector2 boxRightEdge = new Vector2(boxCenter3D.x + halfColliderWidth, boxCenter3D.y);
		
		Vector2 boxBottomCenter = new Vector2(boxCenter.x, boxCenter.y - halfColliderHeight);
		Vector2 boxLowerLeftCorner = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y - halfColliderHeight);
		Vector2 boxLowerRightCorner = new Vector2(boxCenter.x + halfColliderWidth, boxCenter.y - halfColliderHeight);

		Vector2 centerVector = new Vector2(boxBottomCenter.x - boxCenter.x, boxBottomCenter.y - boxCenter.y);
		Vector2 leftEdgeVector = new Vector2(boxLowerLeftCorner.x - boxLeftEdge.x, boxLowerLeftCorner.y - boxLeftEdge.y);
		Vector2 rightEdgeVector = new Vector2(boxLowerRightCorner.x - boxRightEdge.x, boxLowerRightCorner.y - boxRightEdge.y);

		// Draw a ray down the left edge, center, and right edge in order to detect ground.
		return Physics2D.Raycast(boxCenter, centerVector, centerVector.magnitude + colliderToGroundDistance, 1 << groundLayerIndex)
			|| Physics2D.Raycast(boxLeftEdge, leftEdgeVector, leftEdgeVector.magnitude + colliderToGroundDistance, 1 << groundLayerIndex)
			|| Physics2D.Raycast(boxRightEdge, rightEdgeVector, rightEdgeVector.magnitude + colliderToGroundDistance, 1 << groundLayerIndex);
	}

	private void ActivateGlide() {
		DisableGravity();
		gliding = true;
	}

	// DeactivateGlide is public because external events such as attacks or environment changes can force the
	// player out of glide mode.
	public void DeactivateGlide() {
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
