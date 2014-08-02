using UnityEngine;
using System.Collections;

public class SpriteMotion : MonoBehaviour {

	private Vector2 currentVelocity;
	private bool jumping;

	void Start () {
		currentVelocity = Vector2.zero;
		jumping = false;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			currentVelocity = new Vector2(5, 0);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			currentVelocity = new Vector2(-5, 0);
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			currentVelocity = Vector2.zero;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {

	}
	
	void FixedUpdate () {
		// do some logic and figure out the current velocity
		rigidbody2D.velocity = currentVelocity;
	}
}
