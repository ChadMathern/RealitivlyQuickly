using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherAmmoBox : MonoBehaviour
{
    public int quantity;
    public bool isTriggerPickup;
    public GameObject launcher;
    public GameObject launcherParent;
    public AudioClip pickup;
    public float volume;


    private void OnEnable()
    {
        launcherParent = GameObject.Find("Weapons");
        launcher = launcherParent.transform.Find("Heavy").gameObject;
        SetInitialReferences();
    }

    private void Update()
    {
        transform.Rotate(0, 2, 0 * Time.deltaTime);
    }

    void SetInitialReferences()
    {
        launcher.GetComponent<LaunchGrenade>().SetUI();

        if (isTriggerPickup)
        {
            if (GetComponent<Collider>() != null)
            {
                GetComponent<Collider>().isTrigger = true;
            }
        }

        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        LaunchGrenade Launcher = launcher.GetComponent<LaunchGrenade>();
        if (other.CompareTag("Player") && isTriggerPickup && Launcher.currentLauncherAmmo != Launcher.maxLauncherAmmo)
        {
            TakeAmmo();
            AudioSource.PlayClipAtPoint(pickup, transform.position, volume);
        }
    }

    void TakeAmmo()
    {
        LaunchGrenade Launcher = launcher.GetComponent<LaunchGrenade>();
        Launcher.currentLauncherAmmo += quantity;
        if (Launcher.currentLauncherAmmo > Launcher.maxLauncherAmmo)
        {
            Launcher.currentLauncherAmmo = Launcher.maxLauncherAmmo;
        }
        Launcher.SetUI();
        Destroy(gameObject);
    }
}
