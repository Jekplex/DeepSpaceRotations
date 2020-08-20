using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventScript : MonoBehaviour
{

    // We want every 50 points a different/random event takes place.

    private int counter = 0; // ?

    private GameObject player;
    private PlayerScript playerScript;

    // 0 - x2 / 1 - spotlight.
    [SerializeField] private bool[] boolEvents; // the events as bools
    [SerializeField] private int[] weightEvents; // the percentages attached to the events.

    private GameMaster GM; // Game Master

    [SerializeField] private bool triggered = false; //?

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // grabs player and player script.
        playerScript = player.GetComponent<PlayerScript>();

        GM = gameObject.GetComponent<GameMaster>(); // gets gamemaster script.
    }

    //private void Update()
    //{
    //    // Link two variables together.
    //    //if (counter != playerScript.GetScore())
    //    //{
    //    //    counter = playerScript.GetScore() % 50;
    //    //}
    //
    //    //// Actual code to handle counter.
    //    //if (counter == 0 && !triggered)
    //    //{
    //        
    //    //    // Activate an event
    //    //    StartAnEvent(playerScript.GetScore());
    //
    //    //    // Possible events: x2, Spotlight, Zoomin, Slowmo.
    //        
    //    //}
    //
    //    
    //    
    //}

    public void StartAnEvent(int playerScore)
    {
        if (playerScore != 0 && !triggered) // Don't start an event if they just started a game. And don't start if triggered is true.
        {
            triggered = true; // Activate Event Trigger

            int randomNumber = Random.Range(0, 100); // Random Number Gen

            // WeightEvent System.
            for (int i = 0; i < weightEvents.Length; i++)
            {
                if (randomNumber <= weightEvents[i])
                {
                    // Activate Event;
                    boolEvents[i] = true;
                    break;
                }
                else
                {
                    randomNumber -= weightEvents[i];
                }
            }
            // An event is picked based on the random number and the weights attached to each event.
            //

            // This goes through the eventBool list and activates the events that matches it's slot.
            for (int i = 0; i < boolEvents.Length; i++)
            {
                if (boolEvents[i])
                {
                    switch (i)
                    {
                        case 0:
                            // starts double points event.
                            GM.ActivateDoublePoints();
                            break;
                        case 1:
                            // starts spotlight event
                            GM.ActivateSpotlight();
                            break;
                        case 2:
                            // starts zoom in event
                            GM.ActivateZoomInEvent();
                            break;
                        case 3:
                            // starts rapid fire event.
                            GM.ActivateRapidFireEvent();
                            break;
                        default:
                            Debug.Log("Error!");
                            break;
                    }
                }
            }

            // Done.
        }        

    }

    public void ResetTrigger()
    {
        triggered = false;
        // Reset Trigger
        
        // Loops through boolEvents and turns every bool off/false.
        for (int i = 0; i < boolEvents.Length; i++)
        {
            boolEvents[i] = false;
        }
    }
}
