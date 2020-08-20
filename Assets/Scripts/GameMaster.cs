using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject player;
    private PlayerScript playerScript;

    [Header("UI")]
    public GameObject gameOverPanel;
    public RectTransform ScoreValueLabelHolder;
    public TextMeshProUGUI HighscoreValue;
    
    

    [Header("Specials")]
    public bool doublePointsEnabled = false;
    public float doublePointDuration = 10f;
    public GameObject doublePointsIndicator;
    private float doublePointDuration_;

    public bool playerIsBlinded = false;
    public float playerIsBlindedDuration = 10f;
    public GameObject isBlindedIndicator;
    private float playerIsBlindedDuration_;
    

    public bool ZoomInEvent = false;
    public float ZoomInEventDuration = 10f;
    public GameObject ZoomInIndicator;
    private float ZoomInEventDuration_;

    public bool rapidFireEvent = false;
    public float rapidFireEventDuration = 10f;
    public GameObject rapidFireEventIndicator;
    private float rapidFireEventDuration_;

    [Header("Player Reward Settings")]
    public int target_Reward = 1;
    public int targetHeal_Reward = 1;
    //public int targetSpotlight_Reward = 1;
    //public int targetx2_reward = 1;

    [Header("Other")]
    public GameObject globalLight;
    public GameObject spawner;
    public GameObject mouseLight;

    private PrefabManager PM;

    private GameEventScript GES;

    //[SerializeField] private float gameTime;

    public Animator IndicatorShowAnim;
    public Animator MainCameraAnim;

    public bool playerWasHealed = false;
    public float playerWasHealed_DisplayDuration = 1f;
    private float playerWasHealed_DisplayDuration_;
    public GameObject HealIndicatorObject;
    public Animator HealIndicatorAnim;

    // Start is called before the first frame update
    void Start()
    {
        GES = gameObject.GetComponent<GameEventScript>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();

        // Load Durations; ( Now done during activation )
        doublePointDuration_ = doublePointDuration;
        playerIsBlindedDuration_ = playerIsBlindedDuration;
        rapidFireEventDuration_ = rapidFireEventDuration;
        playerWasHealed_DisplayDuration_ = playerWasHealed_DisplayDuration;

        PM = GameObject.FindWithTag("PrefabManager").GetComponent<PrefabManager>();

        enableFancyLighting(); // bandaid fix to a logical bug that i cba to find/fix.

        HighscoreValue.text = PlayerPrefs.GetInt("HighScore").ToString();

        HealIndicatorAnim = HealIndicatorObject.GetComponent<Animator>();

    }


    private void Update()
    {
        //
        if (doublePointsEnabled)
        {
            //Debug.Log(DoublePointsLengthInSeconds_);
            //Mathf.RoundToInt(DoublePointsLengthInSeconds_);

            doublePointDuration_ -= Time.deltaTime;

            if (doublePointDuration_ <= 0)
            {
                doublePointsEnabled = false; // stop loop
                doublePointsIndicator.SetActive(false); // hide point indicator
                GES.ResetTrigger();

            }
        }

        //if (playerWasHealed)
        //{

        //    playerWasHealed_DisplayDuration_ -= Time.deltaTime;

        //    if (playerWasHealed_DisplayDuration_ <= 0)
        //    {
        //        playerWasHealed = false;
        //        HealIndicatorObject.SetActive(false);
        //    }

        //}

        if (playerIsBlinded)
        {
            //
            playerIsBlindedDuration_ -= Time.deltaTime;
        
            if (playerIsBlindedDuration_ <= 0)
            {
                // Disable spotlight.
                mouseLight.GetComponentInChildren<Light2D>().enabled = false;
                // Enable regular lighting.
                enableFancyLighting();

                // Indicator
                isBlindedIndicator.SetActive(false);

                //
                playerIsBlinded = false;

                GES.ResetTrigger();

            }
        }

        if (ZoomInEvent)
        {
            ZoomInEventDuration_ -= Time.deltaTime;

            if (ZoomInEventDuration_ <= 0)
            {
                //Camera.main.orthographicSize = 6; // default size.
                MainCameraAnim.SetTrigger("CameraZoomBack");

                ZoomInIndicator.SetActive(false);
                ZoomInEvent = false;
                GES.ResetTrigger();
            }
        }

        if (rapidFireEvent)
        {
            rapidFireEventDuration_ -= Time.deltaTime;
            // we can either give it a duration or let it run until next event; coder choice :))

            if (rapidFireEventDuration_ <= 0)
            {
                playerScript.playerCanHoldToRapidFire = false;

                rapidFireEventIndicator.SetActive(false);
                rapidFireEvent = false;
                GES.ResetTrigger();

            }
        }
    }


    public void EndGame()
    {

        // update highscore.
        //int temp = PlayerPrefs.GetInt("HighScore");
        if (playerScript.GetScore() > PlayerPrefs.GetInt("HighScore"))
        {
            //Debug.Log("If statement worked.");
            PlayerPrefs.SetInt("HighScore", playerScript.GetScore());
            //HighscoreValue.text = PlayerPrefs.GetInt("HighScore").ToString();
            HighscoreValue.text = playerScript.GetScore().ToString();
        }

        Time.timeScale = 0.0f;
        gameOverPanel.SetActive(true);

        ScoreValueLabelHolder.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerScript.GetScore().ToString(); //  probably needs caching.
        
        //endGameScoreLabel.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerScript.getScore().ToString(); //  probably needs caching.

        playerScript.enabled = false; // Disables player controlls.

        //Debug.Log(Time.timeSinceLevelLoad);

        

        //Invoke("Restart", restartGameDelay);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        PM.target.GetComponent<TargetScript>().SetMoveSpeedShift(1.0f);
        PM.targetHeal.GetComponent<TargetScript>().SetMoveSpeedShift(1.0f);

        // need to send signal to gamespeed.cs so that it knows to reset speed.

    }

    public void ActivateDoublePoints()
    {

        doublePointDuration_ = doublePointDuration;

        //Debug.Log(doublePointsEnabled);
        doublePointsEnabled = true;
        //Debug.Log(doublePointsEnabled);

        //doublePointsIndicator.SetActive(true);
        IndicatorShowAnim.SetTrigger("DoublePointsTrigger");

    }

    public void ActivateSpotlight()
    {
        // Locate all light sources and turn off.

        disableFancyLighting();

        // Ideally should be cached for smoother performance.

        playerIsBlindedDuration_ = playerIsBlindedDuration; // Value setup to temp var.

        playerIsBlinded = true; // activate if statement.

        //isBlindedIndicator.SetActive(true); // tell user
        IndicatorShowAnim.SetTrigger("IsBlindedTrigger");

        // enable spotlight.
        mouseLight.GetComponentInChildren<Light2D>().enabled = true;

    }

    public void ActivateZoomInEvent()
    {
        ZoomInEventDuration_ = ZoomInEventDuration;
        ZoomInEvent = true;

        //ZoomInIndicator.SetActive(true);
        IndicatorShowAnim.SetTrigger("ZoomInTrigger");

        //Camera.main.orthographicSize = 4;
        MainCameraAnim.SetTrigger("CameraZoomIn");
    }

    public void ActivateRapidFireEvent()
    {
        rapidFireEventDuration_ = rapidFireEventDuration;
        rapidFireEvent = true;

        //rapidFireEventIndicator.SetActive(true);
        IndicatorShowAnim.SetTrigger("RapidFireTrigger");

        playerScript.playerCanHoldToRapidFire = true;

    }

    void enableFancyLighting()
    {
        // Global Light
        globalLight.GetComponent<Light2D>().enabled = true;
        // Player Light
        player.GetComponentInChildren<Light2D>().enabled = true;
        // Mob Light
        //spawner.transform.GetComponentInChildren<Light2D>().enabled = true;

        // https://answers.unity.com/questions/205391/how-to-get-list-of-child-game-objects.html
        foreach (Transform child in spawner.transform)
        {
            child.gameObject.GetComponentInChildren<Light2D>().enabled = true;
        }

        foreach (GameObject item in PM.targets)
        {
            item.GetComponentInChildren<Light2D>().enabled = true;
        }

        //PM.target.GetComponentInChildren<Light2D>().enabled = true;
        //PM.targetHeal.GetComponentInChildren<Light2D>().enabled = true;
    }

    void disableFancyLighting()
    {
        // Global Light
        globalLight.GetComponent<Light2D>().enabled = false;
        // Player Light
        player.GetComponentInChildren<Light2D>().enabled = false;
        // Mob Light
       // spawner.transform.GetComponentInChildren<Light2D>().enabled = false;

        // Need to disable in mobs prefabs aswell :/
        // https://answers.unity.com/questions/205391/how-to-get-list-of-child-game-objects.html
        foreach (Transform child in spawner.transform)
        {
            child.gameObject.GetComponentInChildren<Light2D>().enabled = false;
        }

        foreach (GameObject item in PM.targets)
        {
            item.GetComponentInChildren<Light2D>().enabled = false;
        }

        //PM.target.GetComponentInChildren<Light2D>().enabled = false;
        //PM.targetHeal.GetComponentInChildren<Light2D>().enabled = false;
    }
}
