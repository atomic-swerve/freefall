using UnityEngine;
using System.Collections;

public class DamageCollectableController : MonoBehaviour {

    public static int DAMAGE_UP_VALUE = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int DamageUp(int currentDamageValue)
    {
        Destroy(gameObject);

        return currentDamageValue + DAMAGE_UP_VALUE;
    }
}
