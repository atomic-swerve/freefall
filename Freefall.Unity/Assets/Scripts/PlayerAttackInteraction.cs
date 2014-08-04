using UnityEngine;
using System.Collections;

public class PlayerAttackInteraction : PlayerInteraction {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected override InteractionState PerformInteraction()
    {
        Destroy(gameObject);
        return InteractionState.Complete;
    }
}
