using UnityEngine;
using System.Collections;

public class DropThrough : MonoBehaviour {
	// Length of time that the dropping-through state lasts
	public float dropThroughDuration = .3f;

	private PlayerController player;
	private PlayerGravity gravity;
	private BoxCollider2D boxCollider2D;
	private CircleCollider2D circleCollider2D;
	private GroundChecker groundChecker;

	void Awake () {
		player = GetComponent<PlayerController>();	
		gravity = GetComponent<PlayerGravity>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		circleCollider2D = GetComponent<CircleCollider2D>();
		groundChecker = GetComponent<GroundChecker>();
	}

	public void ActivateDrop() {
		player.DroppingThroughPlatform = true;
		boxCollider2D.isTrigger = true;
		circleCollider2D.isTrigger = true;
		gravity.EnableGravity();
	}

	public void DeactivateDrop() {
		player.DroppingThroughPlatform = false;
		boxCollider2D.isTrigger = false;
		circleCollider2D.isTrigger = false;
	}
	
	public void HandleDropInput() {
		if(Input.GetAxis("Y-Axis") < 0 && Input.GetButtonDown("A") && groundChecker.CheckGrounded(LayerMask.NameToLayer("Dropthrough Ground"))) {
			print("dropping.");
			StartCoroutine("DropThroughPlatform");
		}
	}

	private IEnumerator DropThroughPlatform() {
		ActivateDrop();
		yield return new WaitForSeconds(dropThroughDuration);
		DeactivateDrop();
	}
}
