using UnityEngine;
using System.Collections;

public class CameraMotion : MonoBehaviour {

    public Transform TargetToFollow;
    public bool FollowingTarget = true;
    
    public struct CameraMotionInterp {
		public Vector2 initialPosition;
        public Vector2 targetPosition;
        public float duration;
    }
    
    private CameraMotionInterp currentInterp;
	private bool interpolating = false;
	private float interpStartTime = 0.0f;

	public void BeginInterpolation(CameraMotionInterp interp, bool continueFromCurrent = false) {
		if (continueFromCurrent) {
			interp = new CameraMotionInterp() { 
				initialPosition = transform.position,
				targetPosition = interp.targetPosition,
				duration = interp.duration
			};
		}
		currentInterp = interp;
		FollowingTarget = false;
		interpolating = true;
		interpStartTime = Time.time;
	}

	public void StopInterpolation() {
		interpolating = false;
		interpStartTime = 0.0f;
	}

	public bool IsInterpolating() {
		return interpolating;
	}

	public void FollowTarget(Transform target = null) {
		if (target != null) {
			TargetToFollow = target;
		}
		if (TargetToFollow != null) {
			FollowingTarget = true;
		}
		StopInterpolation();
	}

	void Update () {
		if ((TargetToFollow != null) && FollowingTarget) {
			Vector3 targetPos = TargetToFollow.transform.position;
			transform.position = new Vector3(targetPos.x, targetPos.y + 16, transform.position.z);
		} else if (interpolating) {
			Vector2 pos;
			float currentTime = Time.time;
			float timeDelta = currentTime - interpStartTime;
			float timeRatio = timeDelta / currentInterp.duration;
			if (timeRatio < 1.0f) {
				pos = Vector2.Lerp(currentInterp.initialPosition, 
				                   currentInterp.targetPosition, 
				                   timeRatio);
			} else {
				pos = currentInterp.targetPosition;
				StopInterpolation();
			}
			transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
	}
}
