﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// Motion Controls
	private GlideMotion glideMotion;
	private NonGlideMotion nonGlideMotion;
	private JumpMotion jumpMotion;
	private CrouchMotion crouchMotion;
	private DropThrough dropThrough;

	// Non-Motion State Control Components
	private GroundChecker groundChecker;
	private PlayerGravity playerGravity;

	// Player State
	public bool Gliding { get; set; }
	public bool Jumping { get; set; }
	public bool Grounded { get; set; }
	public bool Crouching { get; set; }
	public bool DroppingThroughPlatform { get; set; }
	public bool Airborne { get; set; }

	// Use this for initialization
	void Awake () {
		//Initialize references to other components of Player.
		glideMotion = GetComponent<GlideMotion>();
		nonGlideMotion = GetComponent<NonGlideMotion>();
		jumpMotion = GetComponent<JumpMotion>();
		crouchMotion = GetComponent<CrouchMotion>();
		dropThrough = GetComponent<DropThrough>();

		groundChecker = GetComponent<GroundChecker>();
		playerGravity = GetComponent<PlayerGravity>();
	}

	void Start() {
		Gliding = true; // Start with glide enabled for testing.
	}
	
	// Update is called once per frame
	void Update () {
		Grounded = groundChecker.CheckGrounded(LayerMask.NameToLayer("Ground")) || 
		(!DroppingThroughPlatform && groundChecker.CheckGrounded(LayerMask.NameToLayer("DropThrough Ground")));
		
		if(DroppingThroughPlatform && dropThrough.TileCleared()) {
			dropThrough.DeactivateDrop();
		}

		if(Grounded) {
			Airborne = true;
			dropThrough.HandleDropInput();

			if(!DroppingThroughPlatform) {
				playerGravity.DisableGravity();
				this.DisableVerticalVelocity();
				glideMotion.DeactivateGlide();

				jumpMotion.HandleJumpInput();
				crouchMotion.HandleCrouchInput();				
			}
		} else {
			Airborne = false;

			// Enable gravity for free fall.
			if(!Gliding) {
				playerGravity.EnableGravity();
			}

			// Handle all possible airborne player actions
			if(!DroppingThroughPlatform) {
				glideMotion.HandleGlideInput();
			}
		}
	}

	void FixedUpdate() {	
		if(Gliding) {
			glideMotion.Glide();
		} else {
			nonGlideMotion.Move();
		} 

		if(Jumping) {
			playerGravity.EnableGravity();
			jumpMotion.Jump();
			Jumping = false;
		}
	}

	private void DisableVerticalVelocity() {
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
	}
}
