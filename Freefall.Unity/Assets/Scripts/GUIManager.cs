using UnityEngine;
using System;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public TextBoxUI TextBox;
	public MainMenuUI MainMenu;

	public static GUIManager GetCurrentGUI(bool throwExceptionIfMissing = true) {
		GameObject[] guiObjects = GameObject.FindGameObjectsWithTag("GUI");
		GUIManager activeGUI = null;
		foreach (GameObject guiObject in guiObjects) {
			if (guiObject.activeInHierarchy) {
				activeGUI = guiObject.GetComponent<GUIManager>();
				if (activeGUI != null) {
					break;
				}
			}
		}
		if ((activeGUI == null) && throwExceptionIfMissing) {
			throw new NullReferenceException("GUIManager");
		}
		return activeGUI;
	}
}
