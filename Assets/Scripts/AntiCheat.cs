using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AntiCheat : MonoBehaviour
{

    // We've got to get the time stamp at press and release and calc difference.
    // If consistent for 5 clicks then troll player.

    private bool playerCheats = false;

    public float cheatThreshold = 0.145f; // This is basically reaction time.
    // By setting it this, i'm claiming that 145ms reaction time consistently is inhuman. based on mouse movement - recorrection etc..

    public bool GetPlayerCheats()
    {
        return playerCheats;
    }

    //private float pressedTime;
    //private float releasedTime;

    //private bool timerActive = false;

    private float timer = 0.0f;
    private int clickCounter;

    //IEnumerator StopAndStart()
    //{
    //    timerActive = false;
    //    yield return new WaitForSeconds(0.001f);
    //    timerActive = true;
    //}

    [SerializeField] private List<float> clickCountList;

    private void Start()
    {
        clickCountList = new List<float>(); // init list

        //StartCoroutine(WaitForAC_Check());
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            // Store clickCounter
            StoreClickCounter();
            ResetClickCounter();
        }

        //if (clickCountList[4] > 16) // 16 is the record for CPS (CLicks per second). 
        //{
        //    playerCheats = true;
        //}
    }

    //IEnumerator WaitForAC_Check()
    //{
    //    while (gameObject.activeSelf)
    //    {
    //        yield return new WaitForSeconds(5);
    //
    //        // Do AC Check
    //        for (int i = 0; i < clickCountList.Count; i++)
    //        {
    //            if (clickCountList[i] > 16)
    //            {
    //
    //            }
    //        }
    //
    //    }
    //    
    //
    //}

    public void incrementClick()
    {
        clickCounter++;
    }

    private void StoreClickCounter()
    {
        clickCountList.Add(clickCounter);

        if (clickCountList.Count == 6)
        {
            clickCountList.RemoveAt(0);

        }
    }

    private void ResetClickCounter()
    {
        clickCounter = 0;
        timer = 0;
    }

    //[SerializeField] private List<float> timerList;
    //
    //private void Start()
    //{
    //    timerList = new List<float>(); // init list
    //}
    //
    //private void StoreTime(float time)
    //{
    //    timerList.Add(time);
    //
    //    if (timerList.Count == 6)
    //    {
    //        timerList.RemoveAt(0);
    //    }
    //}
    //

    //private void DoCheatCheck()
    //{
    //    // FIRST ATTEMPT
    //    //
    //    //int counter = 0;
    //    //
    //    //for (int i = 0; i < timerList.Count; i++)
    //    //{
    //    //    if (timerList[i] == timerList[i + 1])
    //    //    {
    //    //        counter++;
    //    //    }
    //    //    else
    //    //    {
    //    //        return;
    //    //    }
    //    //}
    //    //
    //    //if (counter == 5)
    //    //{
    //    //    Debug.Log("CHEATER ALERT!");
    //    //}
    //
    //    //int counter = 0;
    //
    //    for (int i = 0; i < timerList.Count; i++)
    //    {
    //        if (timerList[i] <= cheatThreshold)
    //        {
    //            continue;
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }
    //
    //    Debug.Log("CHEATER ALERT!");
    //    // I want to troll the cheater.
    //    playerCheats = true;
    //
    //}
}
