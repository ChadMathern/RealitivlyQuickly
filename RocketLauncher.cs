using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour
{
    public Renderer rend;
    public GameObject player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponInv.currentWeapon == 3)
        {
            rend.enabled = true;

            //Do anything you have to do to make your weapon move, fire.. etc.
        }
        else
        {
            rend.enabled = false;
        }
    }
}