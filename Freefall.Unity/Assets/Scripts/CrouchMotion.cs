using UnityEngine;
using System.Collections;

public class CrouchMotion : MonoBehaviour {

	private PlayerController player;
	private BoxCollider2D box;

	void Awake () {
		player = GetComponent<PlayerController>();
		box = GetComponent<BoxCollider2D> ();
	}

	public void ActivateCrouch() {
		if(!player.Crouching) {
			player.Crouching = true;
			//box.center = new Vector2 (0, 11);
			//box.size = new Vector2 (10, 22);
			// Do all other crouch-related logic here
		}
	}

	public void DeactivateCrouch() {
		if(player.Crouching) {
			player.Crouching = false;
			//box.center = new Vector2 (0, 14);
			//box.size = new Vector2 (10, 28);
			// Do all other crouch-related logic here
		}
	}

	public void HandleCrouchInput() {
		if(Input.GetAxis("Y-Axis") < 0) {
			ActivateCrouch();
		} else {
			DeactivateCrouch();
		}
		if(Input.GetAxis("X-Axis") != 0) {
			DeactivateCrouch();
		}
	}
}
