using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class ObjectiveCompletion : ScriptableObject {
	public abstract void Complete();
}
