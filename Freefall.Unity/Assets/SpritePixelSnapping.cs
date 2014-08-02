using UnityEngine;
using System.Collections;

public class SpritePixelSnapping : MonoBehaviour {

	private Transform parent;

	void Start () {
		Rigidbody2D parentRigidbody = GetComponentInParent<Rigidbody2D>();
		if (parentRigidbody == null) {
			Debug.LogError("SpritePixelSnapping expected parent Rigidbody, but none found");
		}
		parent = parentRigidbody.gameObject.transform;
	}
	
	void Update () {
		int px = Mathf.RoundToInt(parent.position.x);
		int py = Mathf.RoundToInt(parent.position.y);
		int pz = Mathf.RoundToInt(parent.position.z);
		transform.position = new Vector3(px, py, pz);
	}
}
