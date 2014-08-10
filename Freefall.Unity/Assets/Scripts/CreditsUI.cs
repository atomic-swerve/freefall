using UnityEngine;
using System.Collections;

public class CreditsUI : MonoBehaviour {

	void Update () {
		if (Input.GetButtonDown("B")) {
			MainMenuUI mainMenuUI = GUIManager.GetCurrentGUI().MainMenu;
			if (mainMenuUI != null) {
				mainMenuUI.ShowMenu();
			}
		}
	}
}
