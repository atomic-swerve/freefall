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
	
		// Internal mechanisms
		void Start ()
		{
				IsPlayerColliding = false;
				CurrentInteractionState = InteractionState.Pending;
				SpriteRenderer = this.GetComponent<SpriteRenderer> ();
		}

		void Update ()
		{
				if (IsPlayerColliding && IsAttemptingInteraction && IsStatePending) {
						CurrentInteractionState = InteractionState.Running;
				}
				if (IsStateRunning) {
						CurrentInteractionState = PerformInteraction ();
				}
				if (IsStateComplete) {
						Destroy (this);
				}

			SpriteRenderer.enabled = ShouldShowIndicator && IsStatePending;
		}

		void OnCollisionEnter2D (Collision2D collision)
		{
				if (collision.gameObject.tag == "PlayerInteraction") {
						IsPlayerColliding = true;
				}
		}

		void OnCollisionExit2D (Collision2D collision)
		{
				if (collision.gameObject.tag == "PlayerInteraction") {
						IsPlayerColliding = false;
				}
		}

		private bool IsAttemptingInteraction {
				get {
						return Input.GetButtonDown ("B");
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
