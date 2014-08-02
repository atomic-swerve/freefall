using UnityEngine;
using System.Collections;

public class JumpMotion : MonoBehaviour {
	public float jumpVelocity = 100f;

	public void Jump() {
		Vector2 newVelocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
		rigidbody2D.velocity = newVelocity;
	}
}
