using UnityEngine;
using System.Collections;

public class ObjectiveTask : MonoBehaviour {
	public delegate bool TaskStatusChangeCallback();
	public TaskStatusChangeCallback ShouldTaskStatusChange;

	public bool IsComplete { get; private set; }

	public void AttemptSetCompletion(bool newCompletionStatus){
		bool oldCompletion = IsComplete;
		IsComplete = newCompletionStatus;
		if(!ShouldTaskStatusChange()){
			IsComplete = oldCompletion;
		}
	}
}
