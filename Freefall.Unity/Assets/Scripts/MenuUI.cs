using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuUI : MonoBehaviour {

	public MenuButtonUI[] MenuButtons;

	private int currentButtonIndex;
	private bool yReleased = true;

	void Update () {
						if (yReleased && Input.GetButtonDown ("Y-Axis")) {
								yReleased = false;
								float y = Input.GetAxis ("Y-Axis");
								if (y < 0) {
										currentButtonIndex = Mathf.Min (currentButtonIndex + 1, MenuButtons.Length - 1);
								} else if (y > 0) {
										currentButtonIndex = Mathf.Max (currentButtonIndex - 1, 0);
								}
						} else if (Input.GetButtonUp ("Y-Axis")) {
								yReleased = true;
						}

						foreach (MenuButtonUI button in MenuButtons) {
								button.Selected = false;
						}
						MenuButtons [currentButtonIndex].Selected = true;
				}

}
