using UnityEngine;
using System.Collections;

public class WindZone : MonoBehaviour {
	public float windModifierX = 1f;
	public float windModifierY = 1f;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			PlayerWindMotion playerWindMotion = other.GetComponent<PlayerWindMotion>();
			playerWindMotion.WindModifierX = windModifierX;
			playerWindMotion.WindModifierY = windModifierY;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			PlayerWindMotion playerWindMotion = other.GetComponent<PlayerWindMotion>();
			playerWindMotion.WindModifierX = 0;
			playerWindMotion.WindModifierY = 0;
		}		
	}
}
