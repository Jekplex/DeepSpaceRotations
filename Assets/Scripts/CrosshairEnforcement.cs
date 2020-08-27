using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairEnforcement : MonoBehaviour
{

    public GameObject PauseMenu;
    public GameObject DeathScreen;
    //public Vector2 newHotspot;

    public Texture2D gameCrosshair;

    private Vector2 newHotspot;


    private void Start()
    {
        newHotspot = new Vector2(28,28); // These values are important. Throughly tested to be center.

        // Start default crosshair.
        UnityEngine.Cursor.SetCursor(gameCrosshair, newHotspot, CursorMode.Auto);

        // Lock cursor in window.
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (PauseMenu.activeSelf || DeathScreen.activeSelf)
        //{
        //    UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        //}
        //else
        //{
        //    UnityEngine.Cursor.SetCursor(gameCrosshair, Vector2.zero, CursorMode.Auto);
        //}
    }

    public void SetOS()
    {
        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
    }

    public void setGame()
    {
        UnityEngine.Cursor.SetCursor(gameCrosshair, newHotspot, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
    }
}
