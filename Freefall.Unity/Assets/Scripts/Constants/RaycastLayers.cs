using UnityEngine;
using System.Collections;

public class RaycastLayers {

	public static readonly int groundLayer;

	static RaycastLayers() {
		groundLayer = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Passable");
	}
}
