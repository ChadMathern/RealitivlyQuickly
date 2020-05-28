using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RQ
{
    public class SniperRifle : MonoBehaviour
    {
        public Renderer rend;
        public GameObject player;
        public Camera fpscam;
        public int Damage = 100;
        public float range = 1000;
        public float fireRate;
        public ParticleSystem muzzleFlash;
        public GameObject impactEffect;
        public GameObject bloodSplatter;
        public float impactForce;
        Animator animator;
        public int currentSniperAmmo;
        public int maxSniperAmmo = 15;
        public Text currentSniperBullets;
        public AudioClip sniperDraw;
        public AudioClip sniperShot;
        public float gunVolume;

        private Transform myTransform;
        private float nextTimeToFire = 0f;
        AudioSource audioSource;
        // Use this for initialization

        private void OnEnable()
        {
            audioSource = GetComponent<AudioSource>();
            myTransform = transform;
            audioSource.PlayOneShot(sniperDraw, gunVolume);
        }
        void Start()
        {
            
            animator = GetComponent<Animator>();
            SetUI();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameObject.Find("Weapons").GetComponent<WeaponSwitcher>().selectedWeapon == 3 && currentSniperAmmo > 0)
            {
                rend.enabled = true;

                if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
                    animator.SetBool("Shoot", true);
                    currentSniperAmmo -= 1;
                    SetUI();
                }
                else
                {
                    animator.SetBool("Shoot", false);
                }

                //Do anything you have to do to make your weapon move, fire.. etc.
            }
            else
            {
                rend.enabled = false;
            }
        }
        void Shoot()
        {
            muzzleFlash.Play();
            audioSource.PlayOneShot(sniperShot, gunVolume);
            audioSource.PlayOneShot(sniperDraw, gunVolume);
            RaycastHit hit;
            if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
            {
                //Debug.Log(hit.collider.name);
            }

            if (hit.rigidbody != null && hit.collider.tag == "Enemy")
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
                Instantiate(bloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                if (hit.rigidbody.gameObject.GetComponent<EnemyTakeDamage>() != null)
                {
                    hit.rigidbody.GetComponent<EnemyTakeDamage>().ProcessDamage(Damage);
                }
            }
            else
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
        public void SetUI()
        {
            if (currentSniperBullets != null)
            {
                currentSniperBullets.text = currentSniperAmmo.ToString();
            }
        }
    }
}
