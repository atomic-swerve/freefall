using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Objective : ScriptableObject {
	public GameObject[] ObjectiveTasks;
	public ObjectiveCompletion CompletionAction;

	private IList<ObjectiveTask> TaskComponents { get; set; }

	public void BindTasks(){
		TaskComponents = new List<ObjectiveTask>();

		foreach(GameObject gameObject in ObjectiveTasks){
			ObjectiveTask task = gameObject.GetComponent<ObjectiveTask>();
			if(task == null){
				Debug.LogWarning("Objective assigned without an ObjectiveTask!");
			}

			task.OnTaskStatusChange += ReceiveTaskStatusChange;
			TaskComponents.Add(task);
		}
	}

	private void ReceiveTaskStatusChange(){
		foreach(ObjectiveTask task in TaskComponents){
			if(!task.IsComplete){
				return;
			}
		}

		if(CompletionAction != null){
			CompletionAction.Complete();
		}
		Destroy(this);
	}
}
