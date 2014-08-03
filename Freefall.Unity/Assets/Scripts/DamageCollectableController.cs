using UnityEngine;
using System.Collections;

public class DamageCollectableController : MonoBehaviour {

    public int damageUpValue = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int DamageUp(int currentDamageValue)
    {
        Destroy(gameObject);

        return currentDamageValue + damageUpValue;
    }
}
