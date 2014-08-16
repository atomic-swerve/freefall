using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Handles collisions with all passable ground tiles.
 */
public class Passable : MonoBehaviour {

	public PlayerController player;
	private BoxCollider2D boxCollider2D;

	private Vector2 downwardsVector;
	private Vector2 upwardsVector;
	private Vector2 rightVector;

	private int passableGroundLayerIndex;

	private int numVerticalRays = 4;
	private int numHorizontalRays = 6;
	private int numDiagonalRays = 4;

	private Rect box;

	void Awake() {
		player = GetComponent<PlayerController>();
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	void Start() {
		passableGroundLayerIndex = LayerMask.NameToLayer("Passable Ground");
	}

	public void HandlePassablePlatforms() {
		box = new Rect(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y, boxCollider2D.bounds.size.x, boxCollider2D.bounds.size.y);

		HandlePassableTilesBelow();
		HandleLateralHits();
		HandlePassableTilesAbove();
		HandleDiagonalHits();
	}

	/**
	* Handles all passable tiles below the player. All passable tiles below the player will be set to collide with player.
	*/
	private void HandlePassableTilesBelow() {
		List<RaycastHit2D> downHits = 
			GetRaycastResults(new Vector2(box.xMin, box.yMin), new Vector2(box.xMax, box.yMin), -Vector2.up, Mathf.Infinity, numVerticalRays);
		List<RaycastHit2D> upHits = 
			GetRaycastResults(new Vector2(box.xMin, box.yMin), new Vector2(box.xMax, box.yMin), Vector2.up, .1f, numVerticalRays);

		// Activate collisions for tiles found below player's bottom edge only if player's bottom edge is not currently passing through a passable tile.
		if(upHits.Count == 0 && !player.DroppingThroughPlatform) {
			SetIgnoreCollisionForHits(downHits, false);
		}
	}

	/**
	* Handles all passable tiles above the player. All passable tiles above the player will be set to ignore collision.
	*/
	private void HandlePassableTilesAbove() {
		List<RaycastHit2D> upHits = 
			GetRaycastResults(new Vector2(box.xMin, box.yMax), new Vector2(box.xMax, box.yMax), Vector2.up, Mathf.Infinity, numVerticalRays);

		SetIgnoreCollisionForHits(upHits, true);
	}

	/**
	* Handles all passable tiles to left/right of the player. All passable tiles to sides of the player will be set to ignore collision.
	*/
	private void HandleLateralHits() {
		List<RaycastHit2D> leftLateralHits = 
			GetRaycastResults(new Vector2(box.xMin, box.yMax), new Vector2(box.xMin, box.yMin), -Vector2.right, Mathf.Infinity, numHorizontalRays);
		List<RaycastHit2D> rightLateralHits = 
			GetRaycastResults(new Vector2(box.xMax, box.yMax), new Vector2(box.xMax, box.yMin), Vector2.right, Mathf.Infinity, numHorizontalRays);

		SetIgnoreCollisionForHits(leftLateralHits, true);
		SetIgnoreCollisionForHits(rightLateralHits, true);
	}

	private void HandleDiagonalHits() {
		List<RaycastHit2D> upperRightHits =
			GetRaycastResults(new Vector2(box.center.x, box.yMax), new Vector2(box.xMax, box.center.y), new Vector2(1, 1), Mathf.Infinity, numDiagonalRays);

		List<RaycastHit2D> upperLeftHits = 
			GetRaycastResults(new Vector2(box.center.x, box.yMax), new Vector2(box.xMin, box.center.y), new Vector2(-1, 1), Mathf.Infinity, numDiagonalRays);

		List<RaycastHit2D> upperDiagonalHits = new List<RaycastHit2D>(upperRightHits);
		upperDiagonalHits.AddRange(upperLeftHits);

		SetIgnoreCollisionForHits(upperDiagonalHits, true);
	}

	private void SetIgnoreCollisionForHits(List<RaycastHit2D> hits, bool ignoreCollision) {
		foreach(RaycastHit2D hit in hits) {
			Physics2D.IgnoreCollision(boxCollider2D, hit.collider, ignoreCollision);
		}
	}

	private List<RaycastHit2D> GetRaycastResults(Vector2 startPoint, Vector2 endPoint, Vector2 direction, float distance, int numRays) {
		List<RaycastHit2D> result = new List<RaycastHit2D>();

		for(int i = 0; i < numRays; i++) {
			float lerpAmount = (float)i / ((float)numRays - 1);
			Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);

			RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance, 1 << passableGroundLayerIndex);

			result.AddRange(hits);
		}

		return result;
	}
}
