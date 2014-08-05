using UnityEngine;
using System.Collections;

public class DropThrough : MonoBehaviour {
	// Length of time that the dropping-through state lasts
	public float dropThroughDuration = .3f;

	private PlayerController player;
	private PlayerGravity gravity;
	private GroundChecker groundChecker;

	void Awake () {
		player = GetComponent<PlayerController>();	
		gravity = GetComponent<PlayerGravity>();
		groundChecker = GetComponent<GroundChecker>();
	}

	public void ActivateDrop() {
		player.DroppingThroughPlatform = true;
		gravity.EnableGravity();
	}

	public void DeactivateDrop() {
		player.DroppingThroughPlatform = false;
	}
	
	public void HandleDropInput() {
		if(Input.GetAxis("Y-Axis") < 0 && Input.GetButtonDown("A") && groundChecker.CheckGrounded(LayerMask.NameToLayer("Dropthrough Ground"))) {
			StartCoroutine("DropThroughPlatform");
		}
	}

	private IEnumerator DropThroughPlatform() {
		RaycastHit2D[] hits = groundChecker.GetHits(LayerMask.NameToLayer("Dropthrough Ground"));

		DisableRigidbodies(hits);
		ActivateDrop();

		yield return new WaitForSeconds(dropThroughDuration);
		
		DeactivateDrop();
		EnableRigidbodies(hits);
	}

	private void DisableRigidbodies(RaycastHit2D[] hits) {
		foreach (RaycastHit2D hit in hits) {
			if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Dropthrough Ground")) {
				hit.collider.isTrigger = true;
			}
		}		
	}

	private void EnableRigidbodies(RaycastHit2D[] hits) {
		foreach (RaycastHit2D hit in hits) {
			if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Dropthrough Ground")) {
				hit.collider.isTrigger = false;
			}
		}		
	}
}
