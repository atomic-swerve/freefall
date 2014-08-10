using UnityEngine;
using System.Collections;

public abstract class MenuButtonUI : MonoBehaviour {

	public bool Selected;

	public abstract void OnActivated();

	void Update() {
		Animator animator = GetComponent<Animator>();
		bool animSelected = animator.GetBool("selected");
		if (animSelected != Selected) {
			animator.SetBool("selected", Selected);
		}

		if (Selected && (Input.GetAxis("A") > 0)) {
			Debug.Log("A button hit");
			OnActivated();
		}
	}
}
