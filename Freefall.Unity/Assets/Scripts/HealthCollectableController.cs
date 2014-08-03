using UnityEngine;
using System.Collections;

public class HealthCollectableController : MonoBehaviour {

    public static int HEALTH_RECOVER_VALUE = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int Heal(int currentHealth, int maxHealth)
    {
        int totalHealth = currentHealth + HEALTH_RECOVER_VALUE;
        Destroy(gameObject);

        if (totalHealth > maxHealth)
            return maxHealth;
        else
            return totalHealth;
    }
}
