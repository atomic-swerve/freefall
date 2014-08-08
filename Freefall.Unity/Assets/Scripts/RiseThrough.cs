﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiseThrough : MonoBehaviour {

	private BoxCollider2D boxCollider2D;
	private CircleCollider2D circleCollider2D;

	void Awake() {
		boxCollider2D = GetComponent<BoxCollider2D>();
		circleCollider2D = GetComponent<CircleCollider2D>();
	}

	public void HandleRiseThroughPlatforms() {
		int riseThroughGroundLayerIndex = LayerMask.NameToLayer("RiseThrough Ground");

		float halfColliderWidth = (boxCollider2D.size.x * transform.localScale.x) / 2;
		float halfColliderHeight = (boxCollider2D.size.y * transform.localScale.y) / 2;

		// Transform the collider box's coordinates from local space to world space
		Vector3 boxCenter3D = transform.TransformPoint(boxCollider2D.center);

		Vector2 boxCenter = new Vector2(boxCenter3D.x, boxCenter3D.y);
		Vector2 leftCenter = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y);
		
		// Subtract raisedPlatformTileSize so that the raycast doesn't un-ignore the collision layer while player is still passing through the tile.
		Vector2 bottomCenter = new Vector2(boxCenter.x, boxCenter.y - halfColliderHeight);
		Vector2 lowerLeft = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y - halfColliderHeight);
		Vector2 lowerRight = new Vector2(boxCenter.x + halfColliderWidth, boxCenter.y - halfColliderHeight);

		Vector2 topCenter = new Vector2(boxCenter.x, boxCenter.y + halfColliderHeight);
		Vector2 topLeft = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y + halfColliderHeight);
		Vector2 topRight = new Vector2(boxCenter.x + halfColliderWidth, boxCenter.y + halfColliderHeight);

		Vector2 downwardsVector = new Vector2(0, -1);
		Vector2 upwardsVector = new Vector2(0, 1);
		Vector2 rightVector = new Vector2(1, 0);

		RaycastHit2D middleDownHit = Physics2D.Raycast(bottomCenter, downwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);
		RaycastHit2D leftDownHit = Physics2D.Raycast(lowerLeft, downwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);
		RaycastHit2D rightDownHit = Physics2D.Raycast(lowerRight, downwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);

		RaycastHit2D middleUpHit = Physics2D.Raycast(topCenter, upwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);
		RaycastHit2D leftUpHit = Physics2D.Raycast(topLeft, upwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);
		RaycastHit2D rightUpHit = Physics2D.Raycast(topRight, upwardsVector, Mathf.Infinity, 1 << riseThroughGroundLayerIndex);

		RaycastHit2D lateralTopHit = Physics2D.Raycast(new Vector2(leftCenter.x - 15f, leftCenter.y), rightVector, boxCollider2D.size.x + 30f, 1 << riseThroughGroundLayerIndex);
		RaycastHit2D lateralMiddleHit = Physics2D.Raycast(new Vector2(lowerLeft.x - 15f, lowerLeft.y), rightVector, boxCollider2D.size.x + 30f, 1 << riseThroughGroundLayerIndex);
		RaycastHit2D lateralBottomHit = Physics2D.Raycast(new Vector2(lowerRight.x - 15f, lowerRight.y), rightVector, boxCollider2D.size.x + 30f, 1 << riseThroughGroundLayerIndex);

		List<RaycastHit2D> downHits = new List<RaycastHit2D>();
		downHits.Add(middleDownHit);
		downHits.Add(leftDownHit);
		downHits.Add(rightDownHit);

		List<RaycastHit2D> upHits = new List<RaycastHit2D>();
		upHits.Add(middleUpHit);
		upHits.Add(leftUpHit);
		upHits.Add(rightUpHit);

		List<RaycastHit2D> lateralHits = new List<RaycastHit2D>();
		lateralHits.Add(lateralTopHit);
		lateralHits.Add(lateralMiddleHit);
		lateralHits.Add(lateralBottomHit);

		if(!Physics2D.Raycast(bottomCenter, upwardsVector, .1f, 1 << riseThroughGroundLayerIndex)) {
			foreach(RaycastHit2D hit in downHits) {
				Physics2D.IgnoreCollision(boxCollider2D, hit.collider, false);
				Physics2D.IgnoreCollision(circleCollider2D, hit.collider, false);
			}
		}

		if(!Physics2D.Raycast(bottomCenter, upwardsVector, .1f, 1 << riseThroughGroundLayerIndex) && 
			!Physics2D.Raycast(boxCenter, rightVector, .1f, 1 << riseThroughGroundLayerIndex) &&
			!Physics2D.Raycast(topCenter, downwardsVector, .1f, 1 << riseThroughGroundLayerIndex)) {
			foreach(RaycastHit2D hit in lateralHits) {
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
