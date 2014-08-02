using UnityEngine;
using System.Collections;

public abstract class PlayerInteraction : MonoBehaviour
{
		protected enum InteractionState
		{
				Pending,
				Running,
				Complete
		}
	
		/*
		 * Override this with the behaviour to perform.
	 	 * Use 'yield' to finish behaviour processing for the current frame.
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

				SpriteRenderer.enabled = IsStatePending;
		}

		void OnCollisionEnter2D (Collision2D collision)
		{
				if (collision.gameObject.tag == "Player") {
						IsPlayerColliding = true;
				}
		}

		void OnCollisionExit2D (Collision2D collision)
		{
				if (collision.gameObject.tag == "Player") {
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
