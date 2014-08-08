using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Handles collisions with all passable ground tiles.
 */
public class Passable : MonoBehaviour {

	public PlayerController player;
	private BoxCollider2D boxCollider2D;
	private CircleCollider2D circleCollider2D;

	private Vector2 downwardsVector;
	private Vector2 upwardsVector;
	private Vector2 rightVector;

	private int passableGroundLayerIndex;

	void Awake() {
		player = GetComponent<PlayerController>();

		boxCollider2D = GetComponent<BoxCollider2D>();
		circleCollider2D = GetComponent<CircleCollider2D>();
	}

	void Start() {
		downwardsVector = new Vector2(0, -1);
		upwardsVector = new Vector2(0, 1);
		rightVector = new Vector2(1, 0);

		passableGroundLayerIndex = LayerMask.NameToLayer("Passable Ground");
	}

	public void HandlePassablePlatforms() {
		float halfColliderWidth = (boxCollider2D.size.x * transform.localScale.x) / 2;
		float halfColliderHeight = (boxCollider2D.size.y * transform.localScale.y) / 2;

		// Transform the collider box's coordinates from local space to world space
		Vector3 boxCenter3D = transform.TransformPoint(boxCollider2D.center);
		Vector2 boxCenter = new Vector2(boxCenter3D.x, boxCenter3D.y);
		Vector2 leftCenter = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y);
		
		Vector2 bottomCenter = new Vector2(boxCenter.x, boxCenter.y - halfColliderHeight);
		Vector2 lowerLeft = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y - halfColliderHeight);
		Vector2 lowerRight = new Vector2(boxCenter.x + halfColliderWidth, boxCenter.y - halfColliderHeight);

		Vector2 topCenter = new Vector2(boxCenter.x, boxCenter.y + halfColliderHeight);
		Vector2 topLeft = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y + halfColliderHeight);
		Vector2 topRight = new Vector2(boxCenter.x + halfColliderWidth, boxCenter.y + halfColliderHeight);

		HandleDownHits(bottomCenter, lowerLeft, lowerRight);
		HandleLateralHits(leftCenter, lowerLeft, lowerRight);
		HandleUpHits(topCenter, topLeft, topRight);
	}

	/**
	* Handles all passable tiles below the player. All passable tiles below the player will be set to collide with player.
	*/
	private void HandleDownHits(Vector2 bottomCenter, Vector2 lowerLeft, Vector2 lowerRight) {
		RaycastHit2D middleDownHit = Physics2D.Raycast(bottomCenter, downwardsVector, Mathf.Infinity, 1 << passableGroundLayerIndex);
		RaycastHit2D leftDownHit = Physics2D.Raycast(lowerLeft, downwardsVector, Mathf.Infinity, 1 << passableGroundLayerIndex);
		RaycastHit2D rightDownHit = Physics2D.Raycast(lowerRight, downwardsVector, Mathf.Infinity, 1 << passableGroundLayerIndex);

		List<RaycastHit2D> downHits = new List<RaycastHit2D>();
		downHits.Add(middleDownHit);
		downHits.Add(leftDownHit);
		downHits.Add(rightDownHit);

		// Activate collisions for tiles found below player's bottom edge only if player's bottom edge is not currently passing through a passable tile.
		if(!Physics2D.Raycast(bottomCenter, upwardsVector, .1f, 1 << passableGroundLayerIndex) &&
		!Physics2D.Raycast(lowerLeft, upwardsVector, .1f, 1 << passableGroundLayerIndex) &&
		!Physics2D.Raycast(lowerRight, upwardsVector, .1f, 1 << passableGroundLayerIndex) &&
		 !player.DroppingThroughPlatform) {
			foreach(RaycastHit2D hit in downHits) {
				Physics2D.IgnoreCollision(boxCollider2D, hit.collider, false);
				Physics2D.IgnoreCollision(circleCollider2D, hit.collider, false);
			}
		}
	}

	/**
	* Handles all passable tiles above the player. All passable tiles above the player will be set to ignore collision.
	*/
	private void HandleUpHits(Vector2 topCenter, Vector2 topLeft, Vector2 topRight) {
		RaycastHit2D middleUpHit = Physics2D.Raycast(topCenter, upwardsVector, Mathf.Infinity, 1 << passableGroundLayerIndex);
		RaycastHit2D leftUpHit = Physics2D.Raycast(topLeft, upwardsVector, Mathf.Infinity, 1 << passableGroundLayerIndex);
		RaycastHit2D rightUpHit = Physics2D.Raycast(topRight, upwardsVector, Mathf.Infinity, 1 << passableGroundLayerIndex);

		List<RaycastHit2D> upHits = new List<RaycastHit2D>();
		upHits.Add(middleUpHit);
		upHits.Add(leftUpHit);
		upHits.Add(rightUpHit);

		// Disable collisions for tiles found above player so that player can jump through them.
		foreach(RaycastHit2D hit in upHits) {
			Physics2D.IgnoreCollision(boxCollider2D, hit.collider, true);
			Physics2D.IgnoreCollision(circleCollider2D, hit.collider, true);
		}
	}

	/**
	* Handles all passable tiles to left/right of the player. All passable tiles to sides of the player will be set to ignore collision.
	*/
	private void HandleLateralHits(Vector2 leftCenter, Vector2 lowerLeft, Vector2 lowerRight) {
		RaycastHit2D lateralTopHit = Physics2D.Raycast(new Vector2(leftCenter.x - 100f, leftCenter.y), rightVector, boxCollider2D.size.x + 200f, 1 << passableGroundLayerIndex);
		RaycastHit2D[] lateralMiddleHits = Physics2D.RaycastAll(new Vector2(lowerLeft.x - 100f, lowerLeft.y), rightVector, boxCollider2D.size.x + 2000f, 1 << passableGroundLayerIndex);
		RaycastHit2D lateralBottomHit = Physics2D.Raycast(new Vector2(lowerRight.x - 100f, lowerRight.y), rightVector, boxCollider2D.size.x + 200f, 1 << passableGroundLayerIndex);

		List<RaycastHit2D> lateralHits = new List<RaycastHit2D>();
		lateralHits.Add(lateralTopHit);
		lateralHits.AddRange(lateralMiddleHits);
		lateralHits.Add(lateralBottomHit);

		foreach(RaycastHit2D hit in lateralHits) {
			Physics2D.IgnoreCollision(boxCollider2D, hit.collider, true);
			Physics2D.IgnoreCollision(circleCollider2D, hit.collider, true);
		}

	}
}
