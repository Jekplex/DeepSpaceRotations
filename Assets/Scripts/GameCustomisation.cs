using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using Color = UnityEngine.Color;

public class GameCustomisation : MonoBehaviour
{
    // Regardless of script name...
    // This script is focused on changing the player's colour.

    // Background Manager is responsible for changing the wallpaper.


    private GameObject player;
    private SpriteRenderer playerSR;

    public TMP_Dropdown playerColourDropdown;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerSR = player.GetComponent<SpriteRenderer>();

        int temp = PlayerPrefs.GetInt("PlayerColour", 0);
        SetPlayerColour(temp);

        // DropDown
        playerColourDropdown.value = temp;
    }

    public void SetPlayerColour(int val)
    {
        // White + ROYGBIV

        if (val == 0)
        {
            playerSR.color = Color.white;
        }
        if (val == 1)
        {
            playerSR.color = Color.red;
        }
        if (val == 2)
        {
            playerSR.color = new Color(1.0f, 0.64f, 0.0f); // Orange
        }
        if (val == 3)
        {
            playerSR.color = Color.yellow;
        }
        if (val == 4)
        {
            playerSR.color = Color.green;
        }
        if (val == 5)
        {
            playerSR.color = Color.blue;
        }
        if (val == 6)
        {
            playerSR.color = new Color(0.25f, 0.0f, 1.0f); // Indigo
        }
        if (val == 7)
        {
            playerSR.color = new Color(0.50f, 0.0f, 1.0f); // Violet
        }
        else
        {
            return;
        }

        PlayerPrefs.SetInt("PlayerColour", val);
    }


}
