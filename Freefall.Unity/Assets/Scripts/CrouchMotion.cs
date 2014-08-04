using UnityEngine;
using System.Collections;

public class CrouchMotion : MonoBehaviour {

	private PlayerController player;

	void Awake () {
		player = GetComponent<PlayerController>();
	}

	public void ActivateCrouch() {
		if(!player.Crouching) {
			player.Crouching = true;
			// Do all other crouch-related logic here
		}
	}

	public void DeactivateCrouch() {
		if(player.Crouching) {
			player.Crouching = false;
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
