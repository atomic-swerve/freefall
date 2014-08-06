using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour {

    public int healthPoints = 5;
    public int maxHealthPoints = 5;
    public bool isEnemy = false;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Damage the game character
    public void Damage()
    {
        this.healthPoints--;

        if (healthPoints <= 0)
        {
            Destroy(gameObject);
            print("Enemy destroyed");
        }
    }
}
