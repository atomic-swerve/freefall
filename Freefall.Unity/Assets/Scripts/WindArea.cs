using UnityEngine;
using System.Collections;

/**
 * Attached to all areas that apply a wind-related effect.
*/
public class WindArea : MonoBehaviour {
	public float windyModifierX = 1f;
	public float windyModifierY = 1f;
	public float windlessModifierY = 1f;

	public GlideMotion glideMotion;

	public bool windless = false;

	void Awake() {
		glideMotion = GameObject.FindWithTag("Player").GetComponent<GlideMotion>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			PlayerWindMotion playerWindMotion = other.GetComponent<PlayerWindMotion>();
			if(windless) {
				playerWindMotion.InWindlessArea = true;
				playerWindMotion.WindModifierY = windlessModifierY;
			} else {
				playerWindMotion.InWindlessArea = false;
				playerWindMotion.WindModifierX = windyModifierX;
				playerWindMotion.WindModifierY = windyModifierY;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			PlayerWindMotion playerWindMotion = other.GetComponent<PlayerWindMotion>();
			playerWindMotion.WindModifierX = 0;
			playerWindMotion.WindModifierY = 0;
			if(windless) {
				playerWindMotion.InWindlessArea = false;
			} else {
				glideMotion.EnableDecelerationX = true;
				glideMotion.EnableDecelerationY = true;
			}
		}		
	}
}
