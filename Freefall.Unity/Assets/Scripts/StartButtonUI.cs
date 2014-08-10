using UnityEngine;
using System.Collections;

public class StartButtonUI : MenuButtonUI {

	public string SceneName = "Scenes/Levels/level1";

	public override void OnActivated() {
		Application.LoadLevel(SceneName);
	}
}
