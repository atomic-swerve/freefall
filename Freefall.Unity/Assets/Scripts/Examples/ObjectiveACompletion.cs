using UnityEngine;
using System.Collections;

[System.Serializable]
public class ObjectiveACompletion : ObjectiveCompletion {
	public override void Complete(){
		GameObject displayWrapper = GameObject.Find("ObjectiveManager");
		ObjectiveDisplay display = displayWrapper.GetComponent<ObjectiveDisplay>();
		if(display == null){
			Debug.LogWarning("No objective display found.");
		} else {
			display.Text = "Objective A Complete!";
		}
	}
}
