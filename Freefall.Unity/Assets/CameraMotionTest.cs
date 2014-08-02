using UnityEngine;
using System.Collections;

public class CameraMotionTest : MonoBehaviour {

	private Vector2 currentVelocity;

	void Start () {
		currentVelocity = Vector2.zero;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.D)) {
			currentVelocity = new Vector2(5, 0);
		
		} else if (Input.GetKeyDown (KeyCode.A)) {
			currentVelocity = new Vector2(-5, 0);
		
		} else if (Input.GetKeyDown (KeyCode.W)) {
			currentVelocity = Vector2.zero;
		}

		transform.position += new Vector3(Time.deltaTime * currentVelocity.x, 
		                                  Time.deltaTime * currentVelocity.y,
		                                  0);
	}
}
