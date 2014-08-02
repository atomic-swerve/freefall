using UnityEngine;
using System.Collections;

public class EmptyPlayerInteraction : PlayerInteraction {
	private int i = 0;

	override protected InteractionState PerformInteraction (){
		if(++i == 200){
			i = 0;
			return InteractionState.Pending;
		}

		return InteractionState.Running;
	}
}
