using UnityEngine;
using System.Collections;

public class PlayerGravity : MonoBehaviour {
	public float defaultGravity = 40f;
	
	public void EnableGravity() {
		rigidbody2D.gravityScale = defaultGravity;
	}

	public void DisableGravity() {
		rigidbody2D.gravityScale = 0;
	}
}
