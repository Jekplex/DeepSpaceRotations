using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;

public class AntiCheat : MonoBehaviour
{

    private bool playerIsCheating;
    
    public bool GetPlayerIsCheatingBool()
    {
        return playerIsCheating;
    }

    public int cheatThreshold = 16; // world record for one second.
    public int cheatThreshold2 = 14; // world record for 5 seconds.

    public int lowestThresholdForCase3 = 7; //CPS


    private float timer = 0.0f;
    private int clickCounter = 0;
    [SerializeField] private int[] clickCounterArray;
    //[SerializeField] private List<int> clickCounterList;

    public void incrementClick()
    {
        clickCounter++;
    }

    private void Start()
    {
        clickCounterArray = new int[10]; // init array w/ 10 slots
        //clickCounterList = new List<int>();

        for (int i = 0; i < clickCounterArray.Length; i++)
        {
            clickCounterArray[i] = -1;
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            // Store number of clicks per second.
            Store();

            // CheatCheck
            CheatCheck();

            // Reset for next second.
            myReset();
        }
    }

    private void Store()
    {
        // Try and find an empty slot.
        for (int i = 0; i < clickCounterArray.Length; i++)
        {
            if (clickCounterArray[i] == -1)
            {
                clickCounterArray[i] = clickCounter;
                return;
            }
            else
            {
                continue;
            }
        }

        // Cant find it?
        // Shift to make space for new element.

        for (int i = 0; i < clickCounterArray.Length - 1; i++)
        {
            clickCounterArray[i] = clickCounterArray[i + 1];
        }
        clickCounterArray[clickCounterArray.Length - 1] = clickCounter;
        
    }

    private void myReset()
    {
        timer = 0.0f;
        clickCounter = 0;
    }

    private void CheatCheck()
    {
        // This attempt I converted over to CPS as a measurement as ms between clicks weren't as consistent as expected.
        // InHouse testing shows that this anticheat works very well. However it needs to be externally tested.

        // There are three different types of cheat checks done here.
        // 1. Out of realistic bounds within one second. x > 16cps
        // 2. Out of realistic bounds within five seconds. x > 14cps
        // 3. Unrealistic consistency within ten seconds. x1 = x2 = x3...

        // 1.
        
        if (clickCounter > cheatThreshold) // is CPS above 16?
        {
            // If yes, then cheating.
            PlayerConfirmedCheating(1);
        }

        // 2.
        int counter = 0;
        for (int i = 0; i < 5; i++)
        {
            if (clickCounterArray[i] == -1)
            {
                continue;
            }
            else
            {
                if (clickCounterArray[i] > cheatThreshold2)
                {
                    counter++;
                }
            }

        }
        if (counter == 4)
        {
            PlayerConfirmedCheating(2);
        }

        // 3.
        counter = 0; // counter 9 max.
        for (int i = 0; i < clickCounterArray.Length -1; i++)
        {
            if (clickCounterArray[i] == -1)
            {
                continue;
            }
            else
            {
                // patch 1
                if (clickCounterArray[i] >= lowestThresholdForCase3)
                {
                    if (clickCounterArray[i] == clickCounterArray[i + 1])
                    {
                        counter++;
                    }
                }

            }
            
        }

        if (counter == 7) // was 7 or 9
        {
            PlayerConfirmedCheating(3);
        }

    }

    private void PlayerConfirmedCheating(int code = 0)
    {
        if (code == 1)
        {
            playerIsCheating = true;
            Debug.Log("Player is cheating! CASE 1");
            return;
        }
        if (code == 2)
        {
            playerIsCheating = true;
            Debug.Log("Player is cheating! CASE 2");
            return;
        }
        if (code == 3)
        {
            playerIsCheating = true;
            Debug.Log("Player is cheating! CASE 3");
            return;
        }

        if (code == 0)
        {
            playerIsCheating = true;
            Debug.Log("Player is cheating! CASE NULL");
            return;
        }
        
    }


    // SECOND ATTEMP.
    // We've got to get the time stamp at press and release and calc difference.
    // If consistent for 5 clicks then troll player.

    //private bool playerCheats = false;
    //
    //public int cheatThreshold = 16; // world record for one second.
    //public int cheatThreshold2 = 14; // world record for 5 seconds.
    //
    //public bool GetPlayerCheatsBool()
    //{
    //    return playerCheats;
    //}
    //
    ////private float pressedTime;
    ////private float releasedTime;
    //
    ////private bool timerActive = false;
    //
    //private float timer = 0.0f;
    //private int clickCounter;
    //
    //
    //[SerializeField] private List<float> clickCounterList;
    //
    //private void Start()
    //{
    //    clickCounterList = new List<float>(); // init list
    //    //StartCoroutine(WaitForAC_Check());
    //}
    //
    //void Update()
    //{
    //    timer += Time.deltaTime;
    //
    //    if (timer >= 1.0f)
    //    {
    //        // Store clickCounter
    //        StoreClickCounter();
    //        ResetClickCounter();
    //    }
    //
    //}
    //
    //public void incrementClick() // This is used in player script Shoot();
    //{
    //    clickCounter++;
    //}
    //
    //private void StoreClickCounter()
    //{
    //    clickCounterList.Add(clickCounter);
    //
    //    if (clickCounterList.Count == 11)
    //    {
    //        clickCounterList.RemoveAt(0);
    //
    //    }
    //
    //    // cheatCheck
    //    DoCheatCheck();
    //
    //}
    //
    //public void DoCheatCheck()
    //{
    //    if (clickCounter > cheatThreshold) // if CPS is above 16.
    //    {
    //        Debug.Log("Cheater Alert!");
    //        playerCheats = true;
    //        return;
    //    }
    //
    //    // 2 checks here...
    //    // check 1 is for abnormalities.
    //    // check 2 is for abnormal consistencies.
    //
    //    int counter = 0;
    //    int counter2 = 0;
    //    if (clickCounterList.Count == 10) // if List has long list
    //    {
    //        for (int i = 0; i < clickCounterList.Count; i++) // Loop through and if element is greater than
    //        {
    //            if (clickCounterList[i] > cheatThreshold2)
    //            {
    //                counter++;
    //            }
    //
    //            //if (i + 1 <= clickCounterList.Count)
    //            //{
    //            //    if (clickCounterList[i] == clickCounterList[i + 1])
    //            //    {
    //            //        counter2++;
    //            //    }
    //            //}
    //
    //            
    //        }
    //
    //    }
    //    
    //    if (counter >= 5 || counter2 == 9)
    //    {
    //        playerCheats = true;
    //        Debug.Log("Cheater Alert!");
    //        return;
    //    }
    //    
    //
    //}
    //
    //private void ResetClickCounter()
    //{
    //    clickCounter = 0;
    //    timer = 0;
    //}

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
