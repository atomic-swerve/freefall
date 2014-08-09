using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    PlayerController player;

    Vector2 attackBox = new Vector2(7.5f, 7.5f); //modify based on size of player
    float attackDistance = 5.0f;

	// Use this for initialization
	void Start () {
	    player = this.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        PerformAttack();
	}

    void PerformAttack()
    {
        Vector2 attackPosition = new Vector2(player.transform.position.x, player.transform.position.y);

        if (Input.GetButtonDown("B") && Input.GetAxis("Y-Axis") > 0)
        {
            Attack(attackPosition, attackBox, Vector2.up, attackDistance);
        }
        if (Input.GetButtonDown("B") && Input.GetAxis("X-Axis") < 0)
        {
            Attack(attackPosition, attackBox, -Vector2.right, attackDistance);
        }
        if (Input.GetButtonDown("B") && Input.GetAxis("X-Axis") > 0)
        {
            Attack(attackPosition, attackBox, Vector2.right, attackDistance);
        }
        if (Input.GetButtonDown("B") && Input.GetAxis("Y-Axis") < 0)
        {
            if (player.Jumping || player.Gliding) 
                Attack(attackPosition, attackBox, -Vector2.up, attackDistance);
        }
        if (Input.GetButtonDown("B"))
        {
            Attack(attackPosition, attackBox, player.facingVector, attackDistance);
        }
    }

    void Attack(Vector2 position, Vector2 attackBoxSize, Vector2 attackDirection, float attackDistance)
    {
        RaycastHit2D[] hitObjectsRaycast = Physics2D.BoxCastAll(position, attackBoxSize, 0, attackDirection, attackDistance);

        for (int i = 0; i < hitObjectsRaycast.Length; i++)
        {
            GameObject hitObject = hitObjectsRaycast[i].collider.gameObject;
            HealthComponent objectHealth = hitObject.GetComponent<HealthComponent>();

            if (objectHealth != null)
            {
                if (objectHealth.isEnemy)
                {
                    print("Enemy damaged");
                    objectHealth.Damage();
                }
            }
        }
    }
}
