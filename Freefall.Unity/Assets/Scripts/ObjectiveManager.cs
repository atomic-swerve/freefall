using UnityEngine;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour {
	public IList<IObjective> Objectives { get; set; }

	private IObjective CurrentObjective {
		get {
			return Objectives.Count == 0 ? null : Objectives[0];
		}
	}

	public bool ReceiveObjectiveTrigger<T>() where T:IObjective {
		if(CurrentObjective == null){
			Debug.LogWarning(
				"Received objective trigger attempt when no objective remains to be completed.  " +
				"Objective type received: " + typeof(T).FullName, this);
			return false;
		}

		if (CurrentObjective is T) {
			CompleteCurrentObjective();
			return true;
		} else {
			return false;
		}
	}

	private void CompleteCurrentObjective(){
		IObjective completedObjective = CurrentObjective;
		Objectives.RemoveAt(0);
		completedObjective.Complete();
	}
}
