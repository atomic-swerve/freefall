using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float glideForce = 100f;
	public float maxGlideSpeed = 20f;

	private bool airborne = true;

	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if(airborne) {
			Glide();
		}
	}

	private void Glide() {
		if(Input.GetKey(KeyCode.LeftArrow) && -rigidbody2D.velocity.x < maxGlideSpeed) {
			rigidbody2D.AddForce(-Vector2.right * glideForce);
		}
		if(Input.GetKey(KeyCode.RightArrow) && rigidbody2D.velocity.x < maxGlideSpeed) {
			rigidbody2D.AddForce(Vector2.right * glideForce);
		}
		if(Input.GetKey(KeyCode.UpArrow) && rigidbody2D.velocity.y < maxGlideSpeed) {
			rigidbody2D.AddForce(Vector2.up * glideForce);
		}
		if(Input.GetKey(KeyCode.DownArrow) && -rigidbody2D.velocity.y < maxGlideSpeed) {
			rigidbody2D.AddForce(-Vector2.up * glideForce);
		}
	}
}
