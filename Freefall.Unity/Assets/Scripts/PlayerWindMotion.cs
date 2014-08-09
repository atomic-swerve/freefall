using UnityEngine;
using System.Collections;

public class PlayerWindMotion : MonoBehaviour {
	public float WindModifierX { get; set; }
	public float WindModifierY { get; set; }
	public bool InWindlessArea { get; set; }

	private PlayerController player;

	void Awake() {
		player = GetComponent<PlayerController>();
	}

	public void ApplyWindEffect(ref Vector2 velocity) {
		if(!InWindlessArea || player.Gliding) {
			velocity.x += WindModifierX;
			velocity.y += WindModifierY;
		}
	}
}
