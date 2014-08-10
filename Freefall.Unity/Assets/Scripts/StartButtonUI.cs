using UnityEngine;
using System.Collections;

public class StartButtonUI : MenuButtonUI {

	public string SceneName = "Scenes/Playgrounds/LevelGenTest";

	public override void OnActivated() {
		Application.LoadLevel(SceneName);
	}
}
