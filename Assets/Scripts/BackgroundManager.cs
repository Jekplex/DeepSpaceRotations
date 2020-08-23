using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public enum Wallpaper
    {
        DeepSpace,
        OSUPro,
        Sombrero,
        Purple,
        GalaxyBrain,
        Mountains,
        Views,
        Jelly,
        Tiles,
        Clouds

    }

    public Wallpaper currentWallpaper;

    public GameObject[] Wallpapers;

    public TMP_Dropdown WallpaperDropdown;

    // Methods for handling change of backgrounds.
    // These methods need to store/save preferences to PlayerPrefs 
    // so that on launch changes are kept to player's liking.

    private void Start()
    {
        int tempVal = PlayerPrefs.GetInt("Wallpaper", 0);
        //Debug.Log(PlayerPrefs.GetInt("Wallpaper", 0));
        //Debug.Log(tempVal);

        if (tempVal == 0) // Deep Space
        {
            currentWallpaper = Wallpaper.DeepSpace;
        }
        if (tempVal == 1) // OSU Pro
        {
            currentWallpaper = Wallpaper.OSUPro;
        }
        if (tempVal == 2) // Sombrero
        {
            currentWallpaper = Wallpaper.Sombrero;
        }
        if (tempVal == 3) // Purple
        {
            currentWallpaper = Wallpaper.Purple;
        }
        if (tempVal == 4) // Galaxy Brain
        {
            currentWallpaper = Wallpaper.GalaxyBrain;
        }
        if (tempVal == 5) // Mountains
        {
            currentWallpaper = Wallpaper.Mountains;
        }
        if (tempVal == 6) // Views
        {
            currentWallpaper = Wallpaper.Views;
        }
        if (tempVal == 7) // Jelly
        {
            currentWallpaper = Wallpaper.Jelly;
        }
        if (tempVal == 8) // Tiles
        {
            currentWallpaper = Wallpaper.Tiles;
        }
        if (tempVal == 9) // Clouds
        {
            currentWallpaper = Wallpaper.Clouds;
        }

        // Actual change happens here.
        for (int i = 0; i < Wallpapers.Length; i++) // 10 is the number of actual wallpapers.
        {
            if (i == tempVal)
            {
                Wallpapers[i].SetActive(true);
                PlayerPrefs.SetInt("Wallpaper", i);
            }
            else
            {
                Wallpapers[i].SetActive(false);
            }
        }

        // We need this to effect the dropdown.
        WallpaperDropdown.value = tempVal;

    }

    public void SetBackground(int val)
    {
        Wallpaper beforeWallpaper = currentWallpaper;

        // Set the background.

        if (val == 0) // Deep Space
        {
            currentWallpaper = Wallpaper.DeepSpace;
        }
        if (val == 1) // OSU Pro
        {
            currentWallpaper = Wallpaper.OSUPro;
        }
        if (val == 2) // Sombrero
        {
            currentWallpaper = Wallpaper.Sombrero;
        }
        if (val == 3) // Purple
        {
            currentWallpaper = Wallpaper.Purple;
        }
        if (val == 4) // Galaxy Brain
        {
            currentWallpaper = Wallpaper.GalaxyBrain;
        }
        if (val == 5) // Mountains
        {
            currentWallpaper = Wallpaper.Mountains;
        }
        if (val == 6) // Views
        {
            currentWallpaper = Wallpaper.Views;
        }
        if (val == 7) // Jelly
        {
            currentWallpaper = Wallpaper.Jelly;
        }
        if (val == 8) // Tiles
        {
            currentWallpaper = Wallpaper.Tiles;
        }
        if (val == 9) // Clouds
        {
            currentWallpaper = Wallpaper.Clouds;
        }

        //

        if (beforeWallpaper != currentWallpaper)
        {
            // Update Actual Wallpaper.
            for (int i = 0; i < Wallpapers.Length; i++) // 10 is the number of actual wallpapers.
            {
                if (i == val)
                {
                    Wallpapers[i].SetActive(true);
                    PlayerPrefs.SetInt("Wallpaper", i);
                }
                else
                {
                    Wallpapers[i].SetActive(false);
                }
            }
        }
    }
}
