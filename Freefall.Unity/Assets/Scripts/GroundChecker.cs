using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundChecker : MonoBehaviour {
	// Detection of a ground tile within this distance of bottom left/corner/right of collider will cause 
	// checkGrounded() to evaluate to true.
	public float colliderToGroundDistance = .2f;

	private BoxCollider2D boxCollider2D;
	private CircleCollider2D circleCollider2D;

	void Awake() {
		boxCollider2D = GetComponent<BoxCollider2D>();
		circleCollider2D = GetComponent<CircleCollider2D>();
	}

	public bool CheckGrounded(int groundLayerIndex) {
		float halfColliderWidth = (boxCollider2D.size.x * transform.localScale.x) / 2;
		float halfColliderHeight = (boxCollider2D.size.y * transform.localScale.y) / 2;

		// Transform the collider box's coordinates from local space to world space
		Vector3 boxCenter3D = transform.TransformPoint(boxCollider2D.center);

		Vector2 boxCenter = new Vector2(boxCenter3D.x, boxCenter3D.y);
		
		Vector2 boxBottomCenter = new Vector2(boxCenter.x, boxCenter.y - halfColliderHeight);
		Vector2 boxLowerLeftCorner = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y - halfColliderHeight);
		Vector2 boxLowerRightCorner = new Vector2(boxCenter.x + halfColliderWidth, boxCenter.y - halfColliderHeight);

		Vector2 downwardsVector = new Vector2(0, -1);
		Vector2 upwardsVector = new Vector2(0, 1);

		// Draw a ray down the left edge, center, and right edge in order to detect ground.
		return (Physics2D.Raycast(boxBottomCenter, downwardsVector, colliderToGroundDistance, 1 << groundLayerIndex)
			|| Physics2D.Raycast(boxLowerLeftCorner, downwardsVector, colliderToGroundDistance, 1 << groundLayerIndex)
			|| Physics2D.Raycast(boxLowerRightCorner, downwardsVector, colliderToGroundDistance, 1 << groundLayerIndex))
			&& !Physics2D.Raycast(boxBottomCenter, upwardsVector, .2f, 1 << groundLayerIndex);
	}

	public RaycastHit2D[] GetHits(int groundLayerIndex) {
		float halfColliderWidth = (boxCollider2D.size.x * transform.localScale.x) / 2;
		float halfColliderHeight = (boxCollider2D.size.y * transform.localScale.y) / 2;

		// Transform the collider box's coordinates from local space to world space
		Vector3 boxCenter3D = transform.TransformPoint(boxCollider2D.center);

		Vector2 boxCenter = new Vector2(boxCenter3D.x, boxCenter3D.y);
		
		Vector2 boxBottomCenter = new Vector2(boxCenter.x, boxCenter.y - halfColliderHeight);
		Vector2 boxLowerLeftCorner = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y - halfColliderHeight);
		Vector2 boxLowerRightCorner = new Vector2(boxCenter.x + halfColliderWidth, boxCenter.y - halfColliderHeight);

		Vector2 downwardsVector = new Vector2(0, -1);

		// Draw a ray down the left edge, center, and right edge in order to detect ground.
		RaycastHit2D[] lowerMiddleTileHits = Physics2D.RaycastAll(boxBottomCenter, downwardsVector, colliderToGroundDistance, 1 << groundLayerIndex);
		RaycastHit2D[] lowerLeftTileHits = Physics2D.RaycastAll(boxLowerLeftCorner, downwardsVector, colliderToGroundDistance, 1 << groundLayerIndex);
		RaycastHit2D[] lowerRightTileHits = Physics2D.RaycastAll(boxLowerRightCorner, downwardsVector, colliderToGroundDistance, 1 << groundLayerIndex);

		List<RaycastHit2D> result = new List<RaycastHit2D>();
		result.AddRange(lowerMiddleTileHits);
		result.AddRange(lowerLeftTileHits);
		result.AddRange(lowerRightTileHits);

		return result.ToArray();
	}

	public void HandleRiseThroughPlatforms() {
		int riseThroughGroundLayerIndex = LayerMask.NameToLayer("RiseThrough Ground");

		float halfColliderWidth = (boxCollider2D.size.x * transform.localScale.x) / 2;
		float halfColliderHeight = (boxCollider2D.size.y * transform.localScale.y) / 2;

		// Transform the collider box's coordinates from local space to world space
		Vector3 boxCenter3D = transform.TransformPoint(boxCollider2D.center);

		Vector2 boxCenter = new Vector2(boxCenter3D.x, boxCenter3D.y);
		
		// Subtract raisedPlatformTileSize so that the raycast doesn't un-ignore the collision layer while player is still passing through the tile.
		Vector2 bottomCenter = new Vector2(boxCenter.x, boxCenter.y - halfColliderHeight);
		Vector2 lowerLeft = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y - halfColliderHeight);
		Vector2 lowerRight = new Vector2(boxCenter.x + halfColliderWidth, boxCenter.y - halfColliderHeight);

		Vector2 topCenter = new Vector2(boxCenter.x, boxCenter.y + halfColliderHeight);
		Vector2 topLeft = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y + halfColliderHeight);
		Vector2 topRight = new Vector2(boxCenter.x + halfColliderWidth, boxCenter.y + halfColliderHeight);

		Vector2 downwardsVector = new Vector2(0, -1);
		Vector2 upwardsVector = new Vector2(0, 1);

		RaycastHit2D middleDownHit = Physics2D.Raycast(bottomCenter, downwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);
		RaycastHit2D leftDownHit = Physics2D.Raycast(lowerLeft, downwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);
		RaycastHit2D rightDownHit = Physics2D.Raycast(lowerRight, downwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);

		RaycastHit2D middleUpHit = Physics2D.Raycast(topCenter, upwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);
		RaycastHit2D leftUpHit = Physics2D.Raycast(topLeft, upwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);
		RaycastHit2D rightUpHit = Physics2D.Raycast(topRight, upwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);

		List<RaycastHit2D> downHits = new List<RaycastHit2D>();
		downHits.Add(middleDownHit);
		downHits.Add(leftDownHit);
		downHits.Add(rightDownHit);

		List<RaycastHit2D> upHits = new List<RaycastHit2D>();
		upHits.Add(middleUpHit);
		upHits.Add(leftUpHit);
		upHits.Add(rightUpHit);

		if(!Physics2D.Raycast(bottomCenter, upwardsVector, colliderToGroundDistance, 1 << riseThroughGroundLayerIndex)) {
			foreach(RaycastHit2D hit in downHits) {
				Physics2D.IgnoreCollision(boxCollider2D, hit.collider, false);
				Physics2D.IgnoreCollision(circleCollider2D, hit.collider, false);
			}
		}

		foreach(RaycastHit2D hit in upHits) {
			Physics2D.IgnoreCollision(boxCollider2D, hit.collider, true);
			Physics2D.IgnoreCollision(circleCollider2D, hit.collider, true);
		}
	}
}
