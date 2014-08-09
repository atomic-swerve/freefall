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
        addHearts(playerHealth.maxHealthPoints);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void addHearts(int numHearts)
    {
        for (int i = 0; i < numHearts; i++)
        {
            Transform newHeart = ((GameObject)Instantiate(heartGUI.gameObject)).transform;
            newHeart.parent = this.transform.parent;
            int x = hearts.Count;
            newHeart.GetComponent<GUITexture>().pixelInset = new Rect(x * spacingX, heartGUI.pixelInset.y, 32, 28);
            hearts.Add(newHeart);
        }
}
    }
