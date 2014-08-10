﻿using UnityEngine;
using System.Collections;

public class ObjectiveAInteraction : PlayerInteraction {
	private ObjectiveTask objectiveTask;

	protected override void Start(){
		base.Start();
		objectiveTask = GetComponent<ObjectiveTask>();
	}

	protected override InteractionState PerformInteraction(){
		objectiveTask.IsComplete = true;
		return InteractionState.Complete;
	}
}