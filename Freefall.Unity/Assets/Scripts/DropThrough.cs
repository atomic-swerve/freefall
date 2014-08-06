using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropThrough : MonoBehaviour {
	// Height of a dropThrough tile
	public float dropThroughTileHeight = 8f;

	private PlayerController player;
	private PlayerGravity gravity;
	private GroundChecker groundChecker;
	private BoxCollider2D boxCollider2D;
	private CircleCollider2D circleCollider2D;

	private float originalBoxCollider2DSizeX;

	// List of tiles that are being ignored during a drop (ignored so that they can be passed through).
	private List<Collider2D> ignoredTiles;

	// Position at the beginning of a drop.
	private float positionAtDropStartY;

	void Awake() {
		player = GetComponent<PlayerController>();	
		gravity = GetComponent<PlayerGravity>();
		groundChecker = GetComponent<GroundChecker>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		circleCollider2D = GetComponent<CircleCollider2D>();
		ignoredTiles = new List<Collider2D>();
	}

	void Start() {
		originalBoxCollider2DSizeX = boxCollider2D.size.x;
	}


	public void ActivateDrop() {
		positionAtDropStartY = transform.position.y;

		player.DroppingThroughPlatform = true;
		boxCollider2D.size = new Vector2(boxCollider2D.size.x * .99f, boxCollider2D.size.y);
		gravity.EnableGravity();

		RaycastHit2D[] hits = groundChecker.GetHits(LayerMask.NameToLayer("Dropthrough Ground"));

		// Ignore tiles
		foreach (RaycastHit2D hit in hits) {
			Physics2D.IgnoreCollision(boxCollider2D, hit.collider, true);
			Physics2D.IgnoreCollision(circleCollider2D, hit.collider, true);
			ignoredTiles.Add(hit.collider);
		}
	}

	public void DeactivateDrop() {
		player.DroppingThroughPlatform = false;
		boxCollider2D.size = new Vector2(originalBoxCollider2DSizeX, boxCollider2D.size.y);

		// Unignore tiles
		foreach (Collider2D collider in ignoredTiles) {
			Physics2D.IgnoreCollision(boxCollider2D, collider, false);
			Physics2D.IgnoreCollision(circleCollider2D, collider, false);
		}

		ignoredTiles = new List<Collider2D>();
	}
	
	public void HandleDropInput() {
		if(Input.GetAxis("Y-Axis") < 0 && Input.GetButtonDown("A") && groundChecker.CheckGrounded(LayerMask.NameToLayer("Dropthrough Ground"))) {
			ActivateDrop();
		}
	}

	public bool TileCleared() {
		return transform.position.y < positionAtDropStartY - (dropThroughTileHeight + (boxCollider2D.size.y * transform.localScale.y));
	}
}
