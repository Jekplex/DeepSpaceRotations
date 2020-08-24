using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AntiCheat : MonoBehaviour
{

    // We've got to get the time stamp at press and release and calc difference.
    // If consistent for 5 clicks then troll player.

    private bool playerCheats = false;

    public float cheatThreshold = 0.025f;

    public bool GetPlayerCheats()
    {
        return playerCheats;
    }

    private float pressedTime;
    private float releasedTime;

    private bool timerActive = false;
    private float timer = 0.0f;
    private int count;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            timerActive = true;
            //Debug.Log("Left button has been pressed.");
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            timerActive = false;
            //Debug.Log("Left button has been released.");
        }

        //
        if (timerActive)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (timer != 0)
            {
                // Store timer
                StoreTime(timer);

                //Debug.Log(timer);
                timer = 0.0f;
            }
            
        }

        if (timerList.Count == 5)
        {
            // Do cheat check
            DoCheatCheck();
        }
    }

    [SerializeField] private List<float> timerList;

    private void Start()
    {
        timerList = new List<float>(); // init list
    }

    private void StoreTime(float time)
    {
        timerList.Add(time);

        if (timerList.Count == 6)
        {
            timerList.RemoveAt(0);
        }
    }

    private void DoCheatCheck()
    {
        // FIRST ATTEMPT
        //
        //int counter = 0;
        //
        //for (int i = 0; i < timerList.Count; i++)
        //{
        //    if (timerList[i] == timerList[i + 1])
        //    {
        //        counter++;
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        //
        //if (counter == 5)
        //{
        //    Debug.Log("CHEATER ALERT!");
        //}

        //int counter = 0;

        for (int i = 0; i < timerList.Count; i++)
        {
            if (timerList[i] <= cheatThreshold)
            {
                continue;
            }
            else
            {
                return;
            }
        }

        Debug.Log("CHEATER ALERT!");
        // I want to troll the cheater.
        playerCheats = true;

    }
}
