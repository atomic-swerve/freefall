using UnityEngine;
using System.Collections;

public class PlayerWindMotion : MonoBehaviour {
	public float WindModifierX { get; set; }
	public float WindModifierY { get; set; }
	public bool InWindlessArea { get; set; }

	void Start() {
		WindModifierX = 0;
		WindModifierY = 0;
		InWindlessArea = false;
	}

	public void ApplyWindEffect(ref Vector2 velocity) {
		velocity.x += WindModifierX;
		velocity.y += WindModifierY;
	}
}
