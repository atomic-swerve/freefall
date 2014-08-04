using UnityEngine;
using System.Collections;

public class GroundChecker : MonoBehaviour {
	// Detection of a ground tile within this distance of bottom left/corner/right of collider will cause 
	// checkGrounded() to evaluate to true.
	private float colliderToGroundDistance = .1f;

	private int groundLayerIndex;

	private BoxCollider2D boxCollider2D;

	void Awake() {
		boxCollider2D = GetComponent<BoxCollider2D>();
		groundLayerIndex = LayerMask.NameToLayer("Ground");
	}

	public bool checkGrounded() {
		float halfColliderWidth = (boxCollider2D.size.x * transform.localScale.x) / 2;
		float halfColliderHeight = (boxCollider2D.size.y * transform.localScale.y) / 2;

		// Transform the collider box's coordinates from local space to world space
		Vector3 boxCenter3D = transform.TransformPoint(boxCollider2D.center);

		Vector2 boxCenter = new Vector2(boxCenter3D.x, boxCenter3D.y);
		Vector2 boxLeftEdge = new Vector2(boxCenter3D.x - halfColliderWidth, boxCenter3D.y);
		Vector2 boxRightEdge = new Vector2(boxCenter3D.x + halfColliderWidth, boxCenter3D.y);
		
		Vector2 boxBottomCenter = new Vector2(boxCenter.x, boxCenter.y - halfColliderHeight);
		Vector2 boxLowerLeftCorner = new Vector2(boxCenter.x - halfColliderWidth, boxCenter.y - halfColliderHeight);
		Vector2 boxLowerRightCorner = new Vector2(boxCenter.x + halfColliderWidth, boxCenter.y - halfColliderHeight);

		Vector2 centerVector = new Vector2(boxBottomCenter.x - boxCenter.x, boxBottomCenter.y - boxCenter.y);
		Vector2 leftEdgeVector = new Vector2(boxLowerLeftCorner.x - boxLeftEdge.x, boxLowerLeftCorner.y - boxLeftEdge.y);
		Vector2 rightEdgeVector = new Vector2(boxLowerRightCorner.x - boxRightEdge.x, boxLowerRightCorner.y - boxRightEdge.y);

		// Draw a ray down the left edge, center, and right edge in order to detect ground.
		return Physics2D.Raycast(boxCenter, centerVector, centerVector.magnitude + colliderToGroundDistance, 1 << groundLayerIndex)
			|| Physics2D.Raycast(boxLeftEdge, leftEdgeVector, leftEdgeVector.magnitude + colliderToGroundDistance, 1 << groundLayerIndex)
			|| Physics2D.Raycast(boxRightEdge, rightEdgeVector, rightEdgeVector.magnitude + colliderToGroundDistance, 1 << groundLayerIndex);
	}
}
