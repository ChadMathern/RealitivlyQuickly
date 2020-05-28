using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchGrenade : MonoBehaviour
{
   
    public GameObject impactGrenade;
    public Transform myTransform;
    public ParticleSystem Shockwave;
    public ParticleSystem Smoke;
    public float fireRate;
    public float force;
    public float gunVolume;
    private float nextTimeToFire = 0;
    Animator animator;
    public int currentLauncherAmmo;
    public int maxLauncherAmmo = 50;
    public Text currentLauncherBullets;

    public AudioClip LauncherDraw;
    public AudioClip LauncherShot;
    AudioSource audioSource;
    // Start is called before the first frame update

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(LauncherDraw, gunVolume);

    }
    void Start()
    {
        animator = GetComponent<Animator>();
        setInitialReferences();
        SetUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Weapons").GetComponent<WeaponSwitcher>().selectedWeapon == 4)
        {
            

            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentLauncherAmmo > 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                SpawnImpactGrenade();
                currentLauncherAmmo -= 1;
                animator.SetBool("Shoot", true);
                SetUI();
            }
            else
            {
                animator.SetBool("Shoot", false);
            }

            //Do anything you have to do to make your weapon move, fire.. etc.
        }

    }


    void setInitialReferences()
    {
        myTransform = transform;
    }
    void SpawnImpactGrenade()
    {
        audioSource.PlayOneShot(LauncherShot, gunVolume);
        Shockwave.Play();
        Smoke.Play();
        GameObject Grenade = Instantiate(impactGrenade, myTransform.transform.TransformPoint(0, 0, 2f), transform.rotation);
        Grenade.GetComponent<Rigidbody>().AddForce(myTransform.forward * force, ForceMode.Impulse);
    }
    public void SetUI()
    {
        if (currentLauncherBullets != null)
        {
            currentLauncherBullets.text = currentLauncherAmmo.ToString();
        }
    }

}

