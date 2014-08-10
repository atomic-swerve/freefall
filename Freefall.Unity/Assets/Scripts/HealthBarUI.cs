using UnityEngine;
using System.Collections;

public class HealthBarUI : MonoBehaviour {

    HealthComponent playerHealth;

    // Use this for initialization
    void Start()
    {
        playerHealth = GameObject.Find("TestSprite").GetComponent<HealthComponent>();
    }

    void Update()
    {
        //TestHealth();
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        Animator[] animators = this.GetComponentsInChildren<Animator>();
        for (int i = 0; i < playerHealth.maxHealthPoints; i++)
        {
            if (playerHealth.healthPoints > i)
            {
                animators[i].SetBool("Full", true);
            }
            else
                animators[i].SetBool("Full", false);
        }
    }

    public void TestHealth()
    {
        if (Input.GetButtonDown("B"))
        {
            playerHealth.Heal();
            print("Heal");
        }
        if (Input.GetButtonDown("A"))
        {
            playerHealth.Damage();
            print("Hurt");
        }
    }
}
