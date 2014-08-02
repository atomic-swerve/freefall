using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private bool gliding = true;
	private bool grounded = false;
	
	private Transform groundCheck;
	private GlideMotion glideMotion;
	private NonGlideMotion nonGlideMotion;

	// Use this for initialization
	void Awake () {
		groundCheck = transform.Find("groundCheck");
		glideMotion = transform.GetComponent<GlideMotion>();
		nonGlideMotion = transform.GetComponent<NonGlideMotion>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if(grounded) {
			if(gliding) {
				EndGlide();
			}
		}
	}

	void FixedUpdate() {
		if(gliding) {
			glideMotion.Glide();
		} else {
			nonGlideMotion.Move();
		}
	}

	private void InitiateGlide() {
		rigidbody2D.gravityScale = 0;
		gliding = true;
	}

	private void EndGlide() {
		rigidbody2D.gravityScale = 5;
		gliding = false;
	}

}
