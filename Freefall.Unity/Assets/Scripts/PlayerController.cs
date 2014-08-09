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
		Gliding = true; // Start with glide enabled for testing.
	}
	
	// Update is called once per frame
	void Update () {
		passable.HandlePassablePlatforms();

		Grounded = groundChecker.CheckGrounded(LayerMask.NameToLayer("Ground")) ||
		(!DroppingThroughPlatform && groundChecker.CheckGrounded(LayerMask.NameToLayer("Passable Ground")));
		
		if(DroppingThroughPlatform && dropThrough.TileCleared()) {
			dropThrough.DeactivateDrop();
		}

		if(Grounded) {
			dropThrough.HandleDropInput();

			if(!DroppingThroughPlatform) {
				playerGravity.DisableGravity();
				this.DisableVerticalVelocity();
				glideMotion.DeactivateGlide();

				jumpMotion.HandleJumpInput();
				crouchMotion.HandleCrouchInput();				
			}
		} else {
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

	// Stop player's descent.
	public void DisableVerticalVelocity() {
		if(rigidbody2D.velocity.y < 0) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);			
		}
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        HealthCollectableController healthCollectable = collision.gameObject.GetComponent<HealthCollectableController>();
        DamageCollectableController damageCollectable = collision.gameObject.GetComponent<DamageCollectableController>();

        if (healthCollectable != null)
        {
            this.totalHealth = healthCollectable.Heal(totalHealth, maxHealth);
            print("Player total health: " + this.totalHealth);
        }

        if (damageCollectable != null)
        {
            this.damagePower = damageCollectable.DamageUp(damagePower);
            print("Player total power: " + this.damagePower);
        }
    }
}
