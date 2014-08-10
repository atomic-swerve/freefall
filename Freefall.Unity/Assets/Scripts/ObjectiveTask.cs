using UnityEngine;
using System.Collections;

public class ObjectiveTask : MonoBehaviour {
	public delegate void TaskStatusChangeCallback();
	public TaskStatusChangeCallback OnTaskStatusChange;

	private bool isComplete;
	[HideInInspector] public bool IsComplete {
		get {
			return isComplete;
		}
		set{
			isComplete = value;
			OnTaskStatusChange();
		}
	}
}
