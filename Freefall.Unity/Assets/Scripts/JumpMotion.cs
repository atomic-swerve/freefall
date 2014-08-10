using UnityEngine;
using System.Collections;

public class JumpMotion : MonoBehaviour {
	public float jumpVelocity = 160f;
	public float jumpDecelerationRate = 3f;

	private PlayerController player;

	void Awake () {
		player = GetComponent<PlayerController>();
	}

	public void HandleJumpInput() {
		if(Input.GetButtonDown("A")) {
			player.Jumping = true;
			InitiateJump();
		}
	}

	public void InitiateJump() {
		Vector2 newVelocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
		rigidbody2D.velocity = newVelocity;
	}

	public void HandleJumpState() {
		if(rigidbody2D.velocity.y <= 0) {
			player.Jumping = false;
		}	

		if(!Input.GetButton("A") && player.Jumping) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y - jumpDecelerationRate);				
		}
	}
}
