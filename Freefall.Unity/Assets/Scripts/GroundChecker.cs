using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundChecker : MonoBehaviour {
	// Detection of a ground tile within this distance of bottom left/corner/right of collider will cause 
	// checkGrounded() to evaluate to true.
	public float groundCheckRayLength = 1f;
	public float margin = 1;
	public int numVerticalRays = 4;

	private BoxCollider2D boxCollider2D;
	private PlayerController player;

	void Awake() {
		player = GetComponent<PlayerController>();
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	public bool CheckGrounded(int groundLayerIndex) {
		Rect box = new Rect(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y, boxCollider2D.bounds.size.x, boxCollider2D.bounds.size.y);

		Vector2 startPoint = new Vector2(box.xMin + margin, box.yMin);
		Vector2 endPoint = new Vector2(box.xMax - margin, box.yMin);

		bool connected = false;

		for(int i = 0; i < numVerticalRays; i++) {
			float lerpAmount = (float)i / ((float) numVerticalRays - 1);
			Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);

			RaycastHit2D downHit = Physics2D.Raycast(origin, -Vector2.up, groundCheckRayLength, 1 << groundLayerIndex);
			RaycastHit2D upHit = Physics2D.Raycast(new Vector2(origin.x, origin.y + .1f), Vector2.up, groundCheckRayLength, 1 << groundLayerIndex);

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
		List<RaycastHit2D> result = new List<RaycastHit2D>();

		Rect box = new Rect(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y, boxCollider2D.bounds.size.x, boxCollider2D.bounds.size.y);

		Vector2 startPoint = new Vector2(box.xMin, box.yMin);
		Vector2 endPoint = new Vector2(box.xMax, box.yMin);

		for(int i = 0; i < numVerticalRays; i++) {
			float lerpAmount = (float)i / ((float)numVerticalRays - 1);
			Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);

			Debug.DrawRay(origin, -Vector2.up * groundCheckRayLength, Color.green);
			result.AddRange(Physics2D.RaycastAll(origin, -Vector2.up, groundCheckRayLength, 1 << groundLayerIndex));
		}

		return result.ToArray();
	}
}
