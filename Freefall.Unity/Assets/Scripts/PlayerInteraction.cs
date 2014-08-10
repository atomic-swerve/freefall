using UnityEngine;
using System.Collections;

public abstract class PlayerInteraction : MonoBehaviour
{
		public bool ShouldShowIndicator;
		
		protected enum InteractionState
		{
				Pending,
				Running,
				Complete
		}
	
		/*
		 * Override this with the behaviour to perform as the equivalent
		 * to an 'Update' frame on the interactable object.
	 	 *
	 	 * - Returning 'Pending' will stop interaction processing and
	 	 *   allow the player to restart the interaction.
	 	 * - Returning 'Running' will continue running the interaction
	 	 *   processing on the subsequent frame.
	 	 * - Returning 'Complete' will permanently disable the interaction.
	 	 */
		abstract protected InteractionState PerformInteraction ();

		// Message Hooks

		virtual protected void Awake(){
			IsPlayerColliding = false;
			CurrentInteractionState = InteractionState.Pending;
		}

		virtual protected void Start(){
			SpriteRenderer = GetComponent<SpriteRenderer>();
		}

		virtual protected void Update ()
		{
			if (IsStatePending && IsAttemptingInteraction && IsPlayerColliding) {
					CurrentInteractionState = InteractionState.Running;
			}
			if (IsStateRunning) {
					CurrentInteractionState = PerformInteraction();
			}
			if (IsStateComplete) {
					Destroy (this);
			}

			SpriteRenderer.enabled = ShouldShowIndicator && IsStatePending;
		}

		virtual protected void OnTriggerEnter2D (Collider2D collider)
		{
				if (collider.gameObject.name == "PlayerInteraction") {
						IsPlayerColliding = true;
				}
		}

		virtual protected void OnTriggerExit2D (Collider2D collider)
		{
				if (collider.gameObject.name == "PlayerInteraction") {
						IsPlayerColliding = false;
				}
		}

		// Behavioral Properties

		private bool IsAttemptingInteraction {
			get {
				return Input.GetButtonDown("B");
			}
		}
	
		private bool IsPlayerColliding { get; set; }

		private InteractionState CurrentInteractionState { get; set; }
	
		private bool IsStatePending {
			get {
				return CurrentInteractionState == InteractionState.Pending;
			}
		}
		
		private bool IsStateRunning {
			get {
				return CurrentInteractionState == InteractionState.Running;
			}
		}
		
		private bool IsStateComplete {
			get {
				return CurrentInteractionState == InteractionState.Complete;
			}
		}
	
		private SpriteRenderer SpriteRenderer { get; set; }
}
