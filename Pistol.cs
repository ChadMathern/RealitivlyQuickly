using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RQ
{
    public class Pistol : MonoBehaviour
    {
        public Renderer rend;
        public GameObject player;
        public Camera fpscam;
        public int Damage = 10;
        public float range = 100;
        public float fireRate;
        public ParticleSystem muzzleFlash;
        public GameObject impactEffect;
        public GameObject bloodSplatter;
        public float impactForce;
        public int currentPistolAmmo;
        public int maxPistolAmmo;
        public Text currentPistolBullets;
        Animator animator;
        public AudioClip pistolDraw;
        public AudioClip[] gunShot;
        public float gunVolume;

        private float nextTimeToFire = 0f;
        private Transform myTransform;
        AudioSource audioSource;
        // Use this for initialization
        private void OnEnable()
        {
            audioSource = GetComponent<AudioSource>();
            myTransform = transform;
            audioSource.PlayOneShot(pistolDraw, gunVolume);

        }

        private void OnDisable()
        {
           
        }


        void Start()
        {
      
            animator = GetComponent<Animator>();
            SetUI();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameObject.Find("Weapons").GetComponent<WeaponSwitcher>().selectedWeapon == 0 && currentPistolAmmo > 0)
            {
                rend.enabled = true;

                if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
                    currentPistolAmmo -= 1;
                    animator.SetBool("Shoot", true);
                    SetUI();
                }
                else
                {
                    animator.SetBool("Shoot", false);
                }
            }
            else
            {

                rend.enabled = false;
            }
        }
        void Shoot()
        {
            muzzleFlash.Play();
            PlayShootSound();
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
            if (currentPistolBullets != null)
            {
                currentPistolBullets.text = currentPistolAmmo.ToString();
            }
        }

        void PlayShootSound()
        {
            if (gunShot.Length > 0)
            {
                int index = Random.Range(0, gunShot.Length);
                audioSource.PlayOneShot(gunShot[index], gunVolume);
            }
        }
    }
}