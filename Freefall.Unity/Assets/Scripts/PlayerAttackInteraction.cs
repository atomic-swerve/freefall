using UnityEngine;
using System.Collections;

public class PlayerAttackInteraction : PlayerInteraction {

    PlayerController player;

	// Use this for initialization
	void Start () {
	    player = this.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        Attack();
	}

    override protected InteractionState PerformInteraction()
    {
        Attack();
        return InteractionState.Complete;
    }

    void Attack()
    {
        Vector2 attackBox = new Vector2(1,1);

        Vector2 rightDirection = new Vector2(1,0);
        Vector2 leftDirection = new Vector2(-1,0);

        if (Input.GetButtonDown("Attack") && (Input.GetAxis("X-Axis") > 0))
        {
            RaycastHit2D[] hitObjects = Physics2D.BoxCastAll(this.transform.position, attackBox, 0, rightDirection, 1);
            if (hitObjects.Length == 0)
                print("Nothing hit");
            else
            {
                string text = "Hit Right ";
                for (int i = 0; i < hitObjects.Length; i++)
                {
                    text += "(" + hitObjects[i].transform.position + "), ";
                    Destroy(hitObjects[i].collider.gameObject);
                }
                print(text);
            }
        }
        else if (Input.GetButtonDown("Attack") && (Input.GetAxis("X-Axis") < 0))
        {
            RaycastHit2D[] hitObjects = Physics2D.BoxCastAll(this.transform.position, attackBox, 0, leftDirection, 1);
            if (hitObjects.Length == 0)
                print("Nothing hit");
            else 
            {
                string text = "Hit Left ";
                for (int i = 0; i < hitObjects.Length; i++)
                {
                    text += "(" + hitObjects[i].transform.position + "), ";
                    Destroy(hitObjects[i].collider.gameObject);
                }
                print(text);
            }
        }
    }
}
