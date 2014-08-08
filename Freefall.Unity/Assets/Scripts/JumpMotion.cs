using UnityEngine;
using System.Collections;

public class JumpMotion : MonoBehaviour {
	public float jumpVelocity = 160f;

	private PlayerController player;

	void Awake () {
		player = GetComponent<PlayerController>();
	}

	public void HandleJumpInput() {
		if(Input.GetButtonDown("A")) {
			player.Jumping = true;
			Jump();
		}
	}

	public void Jump() {
		Vector2 newVelocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
		rigidbody2D.velocity = newVelocity;
	}
}
