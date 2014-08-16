using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundChecker : MonoBehaviour {
	// Detection of a ground tile within this distance of bottom left/corner/right of collider will cause 
	// checkGrounded() to evaluate to true.
	public float colliderToGroundDistance = .2f;

	private BoxCollider2D boxCollider2D;

	private PlayerController player;

	private float margin = 1;
	private int verticalRays = 4;

	void Awake() {
		player = GetComponent<PlayerController>();
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	public bool CheckGrounded(int groundLayerIndex) {
		Rect box = new Rect(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y, boxCollider2D.bounds.size.x, boxCollider2D.bounds.size.y);

		Vector2 startPoint = new Vector2(box.xMin + margin, box.yMin);
		Vector2 endPoint = new Vector2(box.xMax - margin, box.yMin);

		bool connected = false;

		for(int i = 0; i < verticalRays; i++) {
			float lerpAmount = (float)i / ((float) verticalRays - 1);
			Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);

			RaycastHit2D downHit = Physics2D.Raycast(origin, -Vector2.up, margin, 1 << groundLayerIndex);
			RaycastHit2D upHit = Physics2D.Raycast(new Vector2(origin.x, origin.y + .1f), Vector2.up, margin, 1 << groundLayerIndex);

			if(upHit.collider != null) {
				connected = false;
				break;
			}

			if(downHit.collider != null) {
				connected = true;
			}

			if(connected && !player.Grounded) {
				transform.position = new Vector2(transform.position.x, downHit.collider.bounds.max.y + .2f);
				break;
			}
		}

		return connected;
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
}
