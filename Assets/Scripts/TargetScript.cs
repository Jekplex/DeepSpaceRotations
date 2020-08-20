using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float minSpeed = 0.5f;
    public float maxSpeed = 3f;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedShift; // This var will be used to increase difficulty as gametime runs.

    private GameObject player;
    private PlayerScript playerScript;
    private HealthBarHUDTester health;
    private Rigidbody2D rb;

    private AudioSource playerHurtSoundSource;
    private AudioSource playerHealSoundSource;

    public void SetMoveSpeedShift(float speedShift)
    {
        moveSpeedShift = speedShift;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        rb = GetComponent<Rigidbody2D>();
        health = GameObject.Find("HealthBarHUDTester").GetComponent<HealthBarHUDTester>(); // Not ideal.

        //moveSpeed = Random.Range(0.5f, 3f);
        moveSpeed = Random.Range(minSpeed, maxSpeed) + moveSpeedShift;

        playerHurtSoundSource = GameObject.FindWithTag("GameMaster").transform.GetChild(1).GetComponent<AudioSource>();
        playerHealSoundSource = GameObject.FindWithTag("GameMaster").transform.GetChild(2).GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        // Calculates enemy movement.
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        // Handles collision with player object.
        if (collision.gameObject.CompareTag("Player"))
        {
            // deals damage to player
            // make sound.
            // destroy self.
            if (gameObject.CompareTag("TargetHeal"))
            {
                playerScript.Heal();

                // Play Heal sound
                playerHealSoundSource.Play();
            }
            else
            {
                playerScript.Hurt(); // playerScript has access to the health interface.

                // Play Hurt Sound.
                playerHurtSoundSource.Play();
            }

            Destroy(gameObject);
        }
    }

}
