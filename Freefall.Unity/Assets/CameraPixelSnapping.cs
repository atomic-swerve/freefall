using UnityEngine;
using System.Collections;

public class CameraPixelSnapping : PixelatedMotion {

	void Start () {
		parent = transform.parent;
		if (parent == null) {
			Debug.LogError("CameraPixelMapping expected parent Transform, but none found");
		}
	}

	protected override float GetXOffset ()
	{
		return 0.5f;
	}

	protected override float GetYOffset ()
	{
		return -0.5f;
	}
}
