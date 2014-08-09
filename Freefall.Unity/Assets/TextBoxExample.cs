using UnityEngine;
using System.Collections;

public class TextBoxExample : MonoBehaviour {

	void Update () {
		TextBoxUI textBox = GUIManager.GetCurrentGUI().TextBox;
		if ((!textBox.gameObject.activeInHierarchy) && (Input.GetButtonDown ("Start"))) {
			textBox.Show("example.txt");
		}
	}
}
