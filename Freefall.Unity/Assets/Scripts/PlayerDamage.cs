using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

    PlayerController player;
    HealthComponent playerHealth;
    AudioAPI audioAPI;

    public int knockBackX = -120;
    public int knockBackY = 50;
    public float invincibleTime = 2;
    public float moveDisableTime = 0.5f;

    // Use this for initialization

    void Start()
    {
        player = this.GetComponent<PlayerController>();
        playerHealth = this.GetComponent<HealthComponent>();
        audioAPI = GameObject.Find("AudioManager").GetComponent<AudioAPI>();
    }

    // Update is called once per frame
    void Update()
    {
        TestDamageKnockback();
    }

    public void DamagePlayer()
    {
        player.Gliding = false;
        player.Jumping = false;
        player.Crouching = false;

        audioAPI.PlaySFX("freefall_hit");

        rigidbody2D.velocity = new Vector2(knockBackX, knockBackY);

        playerHealth.Damage();

        if (playerHealth.healthPoints == 0)
            PlayerDeath();

        StartCoroutine(TurnInvincible(invincibleTime));
    }

    public void PlayerDeath()
    {
        audioAPI.StopSound();
        audioAPI.PlaySong("freefall_title", false);
    }

    private IEnumerator TurnInvincible(float seconds)
    {
        playerHealth.isInvincible = true;
        StartCoroutine(DisableMovement(moveDisableTime));
        yield return new WaitForSeconds(seconds);
        playerHealth.isInvincible = false;
    }

    private IEnumerator DisableMovement(float seconds)
    {
        player.CanMove = false;
        yield return new WaitForSeconds(seconds);
        player.CanMove = true;
    }

    public void TestDamageKnockback()
    {
        if (Input.GetButtonDown("B"))
            DamagePlayer();
    }
}
