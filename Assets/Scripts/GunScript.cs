using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;

    private GameObject player;
    private GameObject playerGun;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerGun = player.transform.GetChild(0).gameObject;
    }
    public void Fire()
    {
        Instantiate(bulletPrefab, playerGun.transform.position, player.transform.rotation);
    }

}
