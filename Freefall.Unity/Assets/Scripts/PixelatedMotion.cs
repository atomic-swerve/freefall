using UnityEngine;
using System.Collections;

public abstract class PixelatedMotion : MonoBehaviour {

	protected Transform parent;

	protected abstract float GetXOffset();

	protected abstract float GetYOffset();

	void Update () {
		float px = Mathf.RoundToInt(parent.position.x) + GetXOffset();
		float py = Mathf.RoundToInt(parent.position.y) + GetYOffset();
		float pz = Mathf.RoundToInt(parent.position.z);
		Debug.Log ("("+px+", "+py+", "+pz+")");
		transform.position = new Vector3(px, py, pz);
	}
}
