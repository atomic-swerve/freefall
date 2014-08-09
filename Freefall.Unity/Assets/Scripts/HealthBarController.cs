using UnityEngine;
using System.Collections;

public class HealthBarController : MonoBehaviour {

    public GUITexture heartGUI;
    public Texture[] healthImages;
    private ArrayList hearts = new ArrayList();

    HealthComponent playerHealth;

    private float spacingX;

	// Use this for initialization
	void Start () {
        spacingX = heartGUI.pixelInset.width;
        playerHealth = GameObject.Find("TestSprite").GetComponent<HealthComponent>();
        AddHearts(playerHealth.maxHealthPoints);
	}
	
	// Update is called once per frame
	void Update () {
        //TestHealth();
	}

    public void AddHearts(int numHearts)
    {
        for (int i = 0; i < numHearts; i++)
        {
            Transform newHeart = ((GameObject)Instantiate(heartGUI.gameObject)).transform;
            newHeart.parent = this.transform.parent;
            newHeart.position = this.transform.position;
            int x = hearts.Count;
            newHeart.GetComponent<GUITexture>().pixelInset = new Rect(x * spacingX, heartGUI.pixelInset.y, 32, 28);
            hearts.Add(newHeart);
        }
    }

    public void HealHeart()
    {
        playerHealth.Heal();
        UpdateHealthBar();
    }

    public void DamageHeart()
    {
        playerHealth.Damage();
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        int i = 1;
        foreach (Transform heart in hearts)
        {
            if (playerHealth.healthPoints >= i)
            {
                heart.guiTexture.texture = healthImages[0]; //full heart
            }
            else
            {
                heart.guiTexture.texture = healthImages[1]; //empty heart
            }
            i++;
        }
    }

    //For testing health bar heal and damage
    public void TestHealth()
    {
        if (Input.GetButtonDown("B"))
        {
            HealHeart();
            print("Heal");
        }
        if (Input.GetButtonDown("A"))
        {
            DamageHeart();
            print("Hurt");
        }
    }
}
