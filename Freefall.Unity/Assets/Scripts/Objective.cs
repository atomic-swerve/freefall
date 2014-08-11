using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Objective {
	public string ObjectiveName;
	public GameObject[] ObjectiveTasks;
	public GameObject CompletionObject;

	private IList<ObjectiveTask> TaskComponents { get; set; }
	private ObjectiveCompletion CompletionAction { get; set; }

	private Predicate<Objective> IsCurrentObjective { get; set; }

	private Action<Objective> OnCompletion { get; set; }

	public void BindTasks(Predicate<Objective> isCurrentObjective, Action<Objective> onCompletion){
		IsCurrentObjective = isCurrentObjective;
		OnCompletion = onCompletion;

		TaskComponents = new List<ObjectiveTask>();

		foreach(GameObject gameObject in ObjectiveTasks){
			ObjectiveTask task = gameObject.GetComponent<ObjectiveTask>();
			if(task == null){
				Debug.LogWarning("Objective assigned without an ObjectiveTask!");
			}

			task.ShouldTaskStatusChange += ReceiveTaskStatusChange;
			TaskComponents.Add(task);
		}

		CompletionAction = CompletionObject.GetComponent<ObjectiveCompletion>();
	}

	private bool ReceiveTaskStatusChange(){
		if(!IsCurrentObjective(this)){
			return false;
		}

		foreach(ObjectiveTask task in TaskComponents){
			if(!task.IsComplete){
				return true;
			}
		}

		if(CompletionAction != null){
			CompletionAction.Complete();
		}
		OnCompletion(this);
		return true;
	}
}
