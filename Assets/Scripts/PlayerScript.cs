using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using TMPro;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Vector2 mousePosition;
    
    public GameObject Gun;

    public TextMeshProUGUI scoreLabel;
    [SerializeField] private int score = 0;

    public float restartGameDelay = 3f;
    
    public bool playerIsDead;

    public GameObject TwoXLabel;
    public GameObject EndGameScore;
    private RectTransform EndGameScoreTransform;

    public InputMaster controls;
    private GunScript PlayerGun;
    private HealthBarHUDTester assetHealth;
    private PlayerStats playerStats;

    private GameMaster GM;
    private GameEventScript GES;

    // game mech
    private bool isBlinded = false;
    private float blindDuration = 10f;
    private float blindDuration_;

    // Grab Pause Menu Script.
    public PauseMenu PauseMenu;

    public GameObject AntiCheatObj;
    private AntiCheat AC;

    public void Hurt()
    {
        assetHealth.Hurt(1f);

        if (playerStats.Health == 0.0f) // If dead, end game.
        {
            GM.EndGame();
        }
    }

    public void Heal()
    {
        assetHealth.Heal(1f);
    }

    

    public void AddToScore(int integer)
    {
        if (AC.GetPlayerCheats())
        {
            if (GM.doublePointsEnabled)
            {
                integer *= 2;
            }

            int counter;
            bool doneOnce = false;

            do
            {
                score -= 1;
                integer -= 1;

                // check
                counter = score % 50;

                if (counter == 0 && !doneOnce)
                {
                    //
                    GES.StartAnEvent(score);
                    doneOnce = true;
                }

            } while (integer > 0);
        }
        else
        {
            if (GM.doublePointsEnabled)
            {
                integer *= 2;
            }

            int counter;
            bool doneOnce = false;

            do
            {
                score += 1;
                integer -= 1;

                // check
                counter = score % 50;

                if (counter == 0 && !doneOnce)
                {
                    //
                    GES.StartAnEvent(score);
                    doneOnce = true;
                }

            } while (integer > 0);
        }
        

        updateScoreLabel();
    }

    public int GetScore()
    {
        return score;
    }

    void updateScoreLabel()
    {
        scoreLabel.text = score.ToString();
    }

    private void Awake()
    {
        controls = new InputMaster();
    }

    private void Start()
    {
        PlayerGun = Gun.GetComponent<GunScript>();
        
        assetHealth = GameObject.Find("HealthBarHUDTester").GetComponent<HealthBarHUDTester>(); // Not ideal.

        GM = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        playerStats = GetComponent<PlayerStats>();

        //
        checkForMouseHoldTimer_ = checkForMouseHoldTimer;

        // Mouse Cursor
        //UnityEngine.Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        GES = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameEventScript>();

        AC = AntiCheatObj.GetComponent<AntiCheat>();

    }

    private void OnEnable()
    {
        // Shoot
        controls.Player.Shoot.performed += context => Shoot();
        controls.Player.Shoot.Enable();

        // Pause
        controls.Player.Pause.performed += context => PauseGame();
        controls.Player.Pause.Enable();

    }

    private void OnDisable()
    {
        // Shoot
        controls.Player.Shoot.performed -= context => Shoot();
        controls.Player.Shoot.Disable();

        // Pause
        controls.Player.Pause.performed -= context => PauseGame();
        controls.Player.Pause.Disable();
    }


    


    void Shoot()
    {

        PlayerGun.Fire();
        //Debug.Log("Player Shot");
    }

    public void PauseGame()
    {
        PauseMenu.PauseGame();

        // Play sound.

    }

    //public void ResumeGame()
    //{
    //    controls.Player.Shoot.Enable();
    //}



    private float beforeTime;
    private float afterTime;
    private float timeDiff;
    private float timeDiffConstant;
    private bool timeDiffConsistent = false;


    // Update is called once per frame
    void Update()
    {
        lookAtMouse();

        if (playerCanHoldToRapidFire)
        {
            checkForMouseHold();
        }

        
        //if (playerIsDead)
        //{
        //    // Set Cursor To Normal.
        //    UnityEngine.Cursor.SetCursor(null, hotSpot, cursorMode);
        //}
    }

    void lookAtMouse()
    {
        // Needs both Input Systems to work. :/
        //mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        
        transform.up = direction;
        
        
        float dist = Vector2.Distance(transform.position, mousePosition);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * Mathf.Abs(dist), Color.red);

        // Credit:
        // https://forum.unity.com/threads/mouse-position-with-new-input-system.829248/

    }

    public bool playerCanHoldToRapidFire;
    public float checkForMouseHoldTimer = 0.3f;
    private float checkForMouseHoldTimer_;
    void checkForMouseHold()
    {
        //if (Keyboard.current[Key.Space].wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame) 
        //{
        //    Shoot();
        //}
        checkForMouseHoldTimer_ -= Time.deltaTime;

        if (Mouse.current.leftButton.isPressed && checkForMouseHoldTimer_ <= 0)
        {
            // Set timer so only every 400ms

            Shoot();
            checkForMouseHoldTimer_ = checkForMouseHoldTimer;

        }

    }

    // Mouse Crosshair/Cursor stuff

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;

}
