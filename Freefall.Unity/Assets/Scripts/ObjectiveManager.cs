using UnityEngine;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour {
	public Objective[] Objectives;

	void OnValidate(){
		for(int i = 0; i < Objectives.Length; i++){
			if(Objectives[i] == null){
				Objectives[i] = ScriptableObject.CreateInstance<Objective>();
			}
		}
	}

	void Start(){
		foreach(Objective objective in Objectives){
			objective.BindTasks();
		}
	}
}
