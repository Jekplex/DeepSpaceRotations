using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class BulletScript : MonoBehaviour
{
    public int speed = 5;
    public float despawnTimeInSeconds = 3f;

    //[Header("Player Reward Settings")]
    private int target_Reward;
    private int targetHeal_Reward;
    //public int targetSpotlight_Reward;
    //public int targetx2_reward;

    private GameObject player;
    private PlayerScript playerScript;
    private GameMaster GM;

    private AudioSource source;
    public AudioClip targetKilledSound;

    private AudioSource playerHealSoundSource;
    private AudioSource playerFiresLaserSource;

    //private Animator HealIndicator_anim;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        GM = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

        target_Reward = GM.target_Reward;
        targetHeal_Reward = GM.targetHeal_Reward;

        source = GM.gameObject.GetComponentInChildren<AudioSource>();

        playerHealSoundSource = GameObject.FindWithTag("GameMaster").transform.GetChild(2).GetComponent<AudioSource>();
        
        playerFiresLaserSource = GameObject.FindWithTag("GameMaster").transform.GetChild(3).GetComponent<AudioSource>();
        playerFiresLaserSource.PlayOneShot(playerFiresLaserSource.clip);

        //HealIndicator_anim = GameObject.Find("Indicators").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);

        despawnTimeInSeconds -= Time.deltaTime;
        if (despawnTimeInSeconds < 0) { Destroy(gameObject); }
    }

    private bool hasCollided = false;

    // Collision Handling
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided)
        {
            if (collision.transform.CompareTag("Target"))
            {
                // Reward player.
                // Make sound
                // Destroy Target
                // Destroy Bullet.

                playerScript.AddToScore(target_Reward);

                //PlayDeathSound();

                Destroy(collision.gameObject);
                Destroy(gameObject);

                hasCollided = true;
            }

            if (collision.transform.CompareTag("TargetHeal"))
            {
                // Heal Player
                // Make Sound
                // Destroy Target
                // Destroy Bullet

                GM.HealIndicatorAnim.SetTrigger("Start");

                //HealIndicator_anim.SetTrigger("Healed");

                playerScript.AddToScore(targetHeal_Reward);
                
                playerScript.Heal();

                //// Heal INdicator
                //GM.playerWasHealed = true;
                


                //PlayDeathSound();
                playerHealSoundSource.Play();

                Destroy(collision.gameObject);
                Destroy(gameObject);

                hasCollided = true;
            }
        }
        

        //if (collision.transform.CompareTag("TargetSpotlight"))
        //{
        //    playerScript.AddToScore(targetSpotlight_Reward);
        //    //
        //    GM.ActivateSpotlight();
        //    Destroy(collision.gameObject);
        //    Destroy(gameObject);
        //}

        //if (collision.transform.CompareTag("Targetx2"))
        //{
        //    playerScript.AddToScore(targetx2_reward);
        //    GM.ActivateDoublePoints();
        //    Destroy(collision.gameObject);
        //    Destroy(gameObject);
        //}

        //void PlayDeathSound() 
        //{
        //    source.clip = targetKilledSound;
        //    source.Play();
        //}
    }
}
