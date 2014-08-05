using UnityEngine;
using System.Collections;

public class DropThrough : MonoBehaviour {
	// Length of time that the dropping-through state lasts
	public float dropThroughDuration = .3f;

	private PlayerController player;
	private PlayerGravity gravity;
	private GroundChecker groundChecker;
	private BoxCollider2D boxCollider2D;

	void Awake () {
		player = GetComponent<PlayerController>();	
		gravity = GetComponent<PlayerGravity>();
		groundChecker = GetComponent<GroundChecker>();
		boxCollider2D = GetComponent<BoxCollider2D>();
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

		float boxColliderOriginalSizeX = boxCollider2D.size.x;

		// Shrink boxCollider2D to avoid collision with adjacent non-dropthrough tile.
		boxCollider2D.size = new Vector2(boxCollider2D.size.x * .99f, boxCollider2D.size.y);
		DisableRigidbodies(hits);
		ActivateDrop();

		yield return new WaitForSeconds(dropThroughDuration);
		
		// Restore boxCollider2D size
		boxCollider2D.size = new Vector2(boxColliderOriginalSizeX, boxCollider2D.size.y);
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
