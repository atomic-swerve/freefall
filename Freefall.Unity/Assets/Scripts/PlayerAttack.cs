using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    PlayerController player;

    Vector2 UP_DIRECTION = new Vector2(0, 1);
    Vector2 DOWN_DIRECTION = new Vector2(0, -1);
    Vector2 RIGHT_DIRECTION = new Vector2(1, 0);
    Vector2 LEFT_DIRECTION = new Vector2(-1, 0);

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
        Vector2 attackBox = new Vector2(7.5f, 7.5f);
        Vector2 attackPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        
        float attackDistance = 5.0f;

        if (Input.GetButtonDown("B") && Input.GetAxis("Y-Axis") > 0)
        {
            Attack(attackPosition, attackBox, UP_DIRECTION, attackDistance);
        }
        if (Input.GetButtonDown("B") && Input.GetAxis("X-Axis") < 0)
        {
            Attack(attackPosition, attackBox, LEFT_DIRECTION, attackDistance);
        }
        if (Input.GetButtonDown("B") && Input.GetAxis("X-Axis") > 0)
        {
            Attack(attackPosition, attackBox, RIGHT_DIRECTION, attackDistance);
        }
        if (Input.GetButtonDown("B") && Input.GetAxis("Y-Axis") < 0)
        {
            if (player.Jumping || player.Gliding) 
                Attack(attackPosition, attackBox, DOWN_DIRECTION, attackDistance);
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

            if (objectHealth.isEnemy)
            {
                print("Enemy damaged");
                objectHealth.Damage();
            }
        }
    }
}
