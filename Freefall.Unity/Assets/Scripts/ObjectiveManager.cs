using UnityEngine;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour {
	public Objective[] Objectives;

	private IList<Objective> ObjectiveList;

	void OnValidate(){
		for(int i = 0; i < Objectives.Length; i++){
			if(Objectives[i] == null){
				Objectives[i] = new Objective();
			}
		}
	}

	void Start(){
		foreach(Objective objective in Objectives){
			objective.BindTasks(IsCurrentObjective, RegisterCompleteObjective);
		}
		ObjectiveList = new List<Objective>(Objectives);
	}

	private bool IsCurrentObjective(Objective objective){
		return ObjectiveList.Count > 0 && ObjectiveList[0] == objective;
	}

	private void RegisterCompleteObjective(Objective objective){
		ObjectiveList.Remove(objective);
	}
}
