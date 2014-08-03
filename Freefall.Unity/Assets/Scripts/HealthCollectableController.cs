using UnityEngine;
using System.Collections;

public class HealthCollectableController : MonoBehaviour {

    public int healthRecoverValue = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int Heal(int currentHealth, int maxHealth)
    {
        int totalHealth = currentHealth + healthRecoverValue;
        Destroy(gameObject);

        if (totalHealth > maxHealth)
            return maxHealth;
        else
            return totalHealth;
    }
}
