using UnityEngine;
using System.Collections;

public class QuitButtonUI : MenuButtonUI {
	
	public override void OnActivated() {
		Application.Quit();
	}
}
