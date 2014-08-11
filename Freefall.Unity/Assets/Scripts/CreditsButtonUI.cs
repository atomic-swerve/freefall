using UnityEngine;
using System.Collections;

public class CreditsButtonUI : MenuButtonUI {
	
	public override void OnActivated() {
		MainMenuUI mainMenuUI = GUIManager.GetCurrentGUI().MainMenu;
		if (mainMenuUI != null) {
			mainMenuUI.ShowCredits();
		}
	}
}
