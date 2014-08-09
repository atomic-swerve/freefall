using UnityEngine;
using System.Collections;

public class TextBoxExample : MonoBehaviour {

    void Start() {
        TextBoxUI textBox = GUIManager.GetCurrentGUI().TextBox;
        textBox.AddOnCloseCallback(OnClose);
    }
    
    void OnClose() {
        Debug.Log("Text box closed");
    }

	void Update () {
		TextBoxUI textBox = GUIManager.GetCurrentGUI().TextBox;
		if ((!textBox.gameObject.activeInHierarchy) && (Input.GetButtonDown ("Start"))) {
            textBox.Show("example.txt");
		}
	}
}
