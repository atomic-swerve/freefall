using UnityEngine;
using System.Collections;

public class SpritePixelSnapping : PixelatedMotion {

	void Start () {
		Rigidbody2D parentRigidbody = GetComponentInParent<Rigidbody2D>();
		if (parentRigidbody == null) {
			Debug.LogError("SpritePixelSnapping expected parent Rigidbody, but none found");
		}
		parent = parentRigidbody.gameObject.transform;
	}
	
	protected override float GetXOffset () { return 0.0f; }
	protected override float GetYOffset () { return 0.0f; }
}
