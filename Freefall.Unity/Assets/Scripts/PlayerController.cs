using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public int totalHealth = 10;
    public int maxHealth = 10;

	public float maxGlideSpeed = 15f;
	public float glideAcceleration = 1.2f;
	public float glideDeceleration = .6f;

	private bool gliding = true;

	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if(gliding) {
			Glide();
		}
	}

	private void Glide() {
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        HealthCollectableController healthCollectable = collision.gameObject.GetComponent<HealthCollectableController>();

        if (healthCollectable != null)
        {
            this.totalHealth = healthCollectable.Heal(totalHealth, maxHealth);
            print("Player total health: " + this.totalHealth);
        }
    }
}
