﻿using UnityEngine;
using System.Collections;

public class ObjectiveACompletion : ObjectiveCompletion {
	public override void Complete(){
		ObjectiveDisplay display = GetComponentInParent<ObjectiveDisplay>();
		if(display == null){
			Debug.LogWarning("No objective display found.");
		} else {
			display.Text = "Objective A Complete!";
		}
	}
}
