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
        Vector2 attackBox = new Vector2(1,1);
        
        float attackDistance = 1.0f;

        if (Input.GetButtonDown("Attack") && Input.GetAxis("Y-Axis") > 0)
        {
            Attack(player.transform.position, attackBox, UP_DIRECTION, attackDistance);
        }
        if (Input.GetButtonDown("Attack") && Input.GetAxis("X-Axis") < 0)
        {
            Attack(player.transform.position, attackBox, LEFT_DIRECTION, attackDistance);
        }
        if (Input.GetButtonDown("Attack") && Input.GetAxis("X-Axis") > 0)
        {
            Attack(player.transform.position, attackBox, RIGHT_DIRECTION, attackDistance);
        }
        if (Input.GetButtonDown("Attack") && Input.GetAxis("Y-Axis") < 0)
        {
            if (player.jumping || player.gliding)
                Attack(player.transform.position, attackBox, DOWN_DIRECTION, attackDistance);
        }
        if (Input.GetButtonDown("Attack"))
        {
            Attack(player.transform.position, attackBox, player.facingVector, attackDistance);
        }
    }

    void Attack(Vector2 position, Vector2 attackBoxSize, Vector2 attackDirection, float attackDistance)
    {
        RaycastHit2D[] hitObjects = Physics2D.BoxCastAll(position, attackBoxSize, 0, attackDirection, attackDistance);

        for (int i = 0; i < hitObjects.Length; i++)
        {
            GameObject enemy = hitObjects[i].collider.gameObject;
            PlayerAttackInteraction attackInteraction = enemy.GetComponent<PlayerAttackInteraction>();
        }
    }
}
