using UnityEngine;
using System.Collections;

public class PlayerWindMotion : MonoBehaviour {
	public float WindModifierX { get; set; }
	public float WindModifierY { get; set; }
	public bool InWindlessArea { get; set; }

	public void ApplyWindEffect(ref Vector2 velocity) {
		velocity.x += WindModifierX;
		velocity.y += WindModifierY;
	}
}
