using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PauseMenu : MonoBehaviour
{
    public GameObject thePauseMenu;
    public GameObject customiseMenu;
    public GameObject areYouSureMenu;

    public Texture2D gameCrosshair;

    private PlayerScript playerScript;

    public CrosshairEnforcement crosshairEnforcement;

    public GameObject backgroundMusic;
    private AudioSource backgroundMusicSource;

    private bool gameIsPaused = false;

    private void Start()
    {
        // grab playerScript
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        backgroundMusicSource = backgroundMusic.GetComponent<AudioSource>();

        // THis object activate. Child not. ON start.
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
    }


    // Function that actually pauses the game.
    public void PauseGame()
    {
        // if game is paused then unpause else pause.
        if (gameIsPaused)
        {
            gameIsPaused = false;
            // Run Time Scale
            Time.timeScale = 1f;
        
            // close pause menu
            thePauseMenu.SetActive(false);
            customiseMenu.SetActive(false);
            areYouSureMenu.SetActive(false);

            playerScript.controls.Player.Shoot.Enable();

            
            crosshairEnforcement.setGame();

            // Resume Music
            backgroundMusicSource.UnPause();

        }
        else
        {
            gameIsPaused = true;
            // Pause Time Scale
            Time.timeScale = 0f;

            // Bring up pause menu
            thePauseMenu.SetActive(true);

            playerScript.controls.Player.Shoot.Disable();

            crosshairEnforcement.SetOS();

            // Pause Music
            backgroundMusicSource.Pause();



        }


    }

    //public void ResumeGame()
    //{
    //    // Run Time Scale
    //    Time.timeScale = 1f;
    //
    //    // Enable Player Controls.
    //    playerScript.enabled = true;
    //}

}
