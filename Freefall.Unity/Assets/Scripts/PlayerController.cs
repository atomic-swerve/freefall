using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Vector2 facingVector = new Vector2(1, 0);

	// Motion Controls
	private GlideMotion glideMotion;
	private NonGlideMotion nonGlideMotion;
	private JumpMotion jumpMotion;
	private CrouchMotion crouchMotion;
	private DropThrough dropThrough;
	private Passable passable;

	// Non-Motion State Control Components
	private GroundChecker groundChecker;
	private PlayerGravity playerGravity;

	// Player State
	public bool Gliding { get; set; }
	public bool Jumping { get; set; }
	public bool Grounded { get; set; }
	public bool Crouching { get; set; }
	public bool DroppingThroughPlatform { get; set; }

	// Use this for initialization
	void Awake () {
		//Initialize references to other components of Player.
		glideMotion = GetComponent<GlideMotion>();
		nonGlideMotion = GetComponent<NonGlideMotion>();
		jumpMotion = GetComponent<JumpMotion>();
		crouchMotion = GetComponent<CrouchMotion>();
		dropThrough = GetComponent<DropThrough>();
		passable = GetComponent<Passable>();

		groundChecker = GetComponent<GroundChecker>();
		playerGravity = GetComponent<PlayerGravity>();
	}

	void Start() {
		rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
		DroppingThroughPlatform = false;
	}
	
	// Update is called once per frame
	void Update () {
		passable.HandlePassablePlatforms();

		Grounded = groundChecker.CheckGrounded(LayerMask.NameToLayer("Ground")) ||
		(!DroppingThroughPlatform && groundChecker.CheckGrounded(LayerMask.NameToLayer("Passable Ground")));
		
		if(DroppingThroughPlatform && dropThrough.TileCleared()) {
			dropThrough.DeactivateDrop();
		}

		if ((facingVector.x > 0) && (rigidbody2D.velocity.x <= -float.Epsilon)) {
			facingVector = new Vector2(-1, 0);
			transform.localScale = new Vector3(-1, 1, 1);
		} else if ((facingVector.x < 0) && (rigidbody2D.velocity.x >= float.Epsilon)) {
			facingVector = new Vector2(1, 0);
			transform.localScale = new Vector3(1, 1, 1);
		}

		Animator animator = GetComponent<Animator>();
		if(Grounded) {
			animator.SetBool("Grounded", true);
			animator.SetBool("Jumping", false);
			dropThrough.HandleDropInput();

			if(!DroppingThroughPlatform) {
				playerGravity.DisableGravity();
				this.DisableVerticalVelocity();
				glideMotion.DeactivateGlide();

				jumpMotion.HandleJumpInput();
				crouchMotion.HandleCrouchInput();				
			}
		} else {
			animator.SetBool("Grounded", false);
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
			glideMotion.HandleGlideMovement();
		} else {
			nonGlideMotion.HandleNonGlideMovement();
		} 

		if(Jumping) {
			jumpMotion.HandleJumpState();
		}
	}

	// Stop player's descent.
	public void DisableVerticalVelocity() {
		if(rigidbody2D.velocity.y < 0) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);			
		}
	}
}
