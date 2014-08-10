﻿using UnityEngine;
using System.Collections;

public class EmptyObjectiveTaskInteraction : PlayerInteraction {
	private ObjectiveTask objectiveTask;

	protected override void Start(){
		base.Start();
		objectiveTask = GetComponent<ObjectiveTask>();
	}

	protected override InteractionState PerformInteraction(){
		objectiveTask.AttemptSetCompletion(true);

		if(objectiveTask.IsComplete){
			return InteractionState.Complete;
		} else {
			return InteractionState.Pending;
		}
	}
}