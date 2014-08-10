using UnityEngine;
using System.Collections;

public class CameraInterpolationExample : MonoBehaviour {

	private bool interpolating = false;
	public CameraMotion TargetCamera;

	void Update () {
		if (Input.GetKeyDown(KeyCode.I) && (!TargetCamera.IsInterpolating())) {
			Vector3 cameraPos = TargetCamera.gameObject.transform.position;
			Vector2 initPos = new Vector2(cameraPos.x, cameraPos.y);
			Vector2 targetPos = new Vector2(cameraPos.x + 100, cameraPos.y + 100);
			float duration = 1.0f;
			CameraMotion.CameraMotionInterp interp = new CameraMotion.CameraMotionInterp() {
				initialPosition = initPos,
				targetPosition = targetPos,
				duration = duration
			};
			TargetCamera.BeginInterpolation(interp);
		} else if (Input.GetKeyDown (KeyCode.O)) {
			TargetCamera.StopInterpolation();
			TargetCamera.FollowTarget();
		}
	}
}
