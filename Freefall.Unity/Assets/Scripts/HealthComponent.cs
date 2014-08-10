using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour {

    public int healthPoints = 5;
    public int maxHealthPoints = 5;
    public bool isEnemy = false;
    public bool isInvincible = false;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Damage the game character
    public void Damage()
    {
        if (!isInvincible)
        {
            this.healthPoints--;
            print("Health: " + healthPoints);
        }

        if (healthPoints <= 0)
        {
            healthPoints = 0;
            if (isEnemy)
            {
                Destroy(gameObject);
            }
            else
            {
                print("Game Over");
            }
        }
    }

    // Damage the game character
    public void Heal()
    {
        if (healthPoints < maxHealthPoints)
        {
            this.healthPoints++;
        }
    }
}
