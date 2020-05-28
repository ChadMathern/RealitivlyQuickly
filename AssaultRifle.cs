using UnityEngine;
using UnityEngine.UI;

namespace RQ
{
    public class AssaultRifle : MonoBehaviour
    {
        public Renderer rend;
        public GameObject player;
        public int Damage = 25;
        public float range = 100;
        public float fireRate = 5f;
        public float impactForce;
        public float gunVolume;
        public Camera fpscam;
        public ParticleSystem muzzleFlash;
        public GameObject impactEffect;
        public GameObject bloodSplatter;
        Animator animator;
        public int currentRifleAmmo;
        public int maxRifleAmmo = 200;
        public Text currentRifleBullets;

        private float nextTimeToFire = 0f;

        public AudioClip ARDraw;
        public AudioClip ARShot;
        private Transform myTransform;
        AudioSource audioSource;


        private void OnEnable()
        {
            audioSource = GetComponent<AudioSource>();
            myTransform = transform;
            audioSource.PlayOneShot(ARDraw, gunVolume);
        }
        private void Start()
        {
            animator = GetComponent<Animator>();
            SetUI();
        }
        void Update()
        {
            if (GameObject.Find("Weapons").GetComponent<WeaponSwitcher>().selectedWeapon == 2 && currentRifleAmmo > 0)
            {
                rend.enabled = true;

                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
                    currentRifleAmmo -= 1;
                    animator.SetBool("Shoot", true);
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
            audioSource.PlayOneShot(ARShot, gunVolume);
            RaycastHit hit;
            if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
            {
                //Debug.Log(hit.collider.name);
            }

            if (hit.rigidbody != null && hit.transform.tag == "Enemy")
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
            if (currentRifleBullets != null)
            {
                currentRifleBullets.text = currentRifleAmmo.ToString();
            }
        }
    }
}