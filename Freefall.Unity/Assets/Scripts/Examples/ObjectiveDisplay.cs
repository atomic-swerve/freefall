﻿using UnityEngine;
using System.Collections;

public class ObjectiveDisplay : MonoBehaviour {
	public string Text { get; set; }

	void Awake(){
		Text = "No objectives completed.";
	}

	void OnGUI(){
		GUI.TextArea(new Rect(10, 10, 200, 20), Text);
	}
}