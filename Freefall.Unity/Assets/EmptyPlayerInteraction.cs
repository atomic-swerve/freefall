using UnityEngine;
using System.Collections;

public class EmptyPlayerInteraction : PlayerInteraction {

	override protected InteractionState PerformInteraction (){
		return InteractionState.Pending;
	}
}
