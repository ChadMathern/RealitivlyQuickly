using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RQ
{
    public class Shotgun2 : MonoBehaviour
    {
        public GameObject Shotgun;
        public float fireRate;
        public Renderer rend;
        public GameObject player;
        public Camera fpsCam;
        public GameObject bulletholePrefab;
        public int Damage = 20;
        public int currentShells;
        public int pellets = 12;
        public int maxShells = 50;
        public float bloom;
        public float range = 1000f;
        public float impactForce;
        public float currentCooldown;
        public float gunVolume;
        public ParticleSystem muzzleFlash;
        Animator animator;
        public GameObject impactEffect;
        public GameObject bloodSplatter;
        public Text CurrentShellsAmmo;

        public AudioClip shotgunDraw;
        public AudioClip shotgunBlast;

        private float nextTimeToFire = 0f;
        private Transform myTransform;

        AudioSource audioSource;
        // Start is called before the first frame update
        private void OnEnable()
        {
            audioSource = GetComponent<AudioSource>();
            myTransform = transform;
            audioSource.PlayOneShot(shotgunDraw, gunVolume);
        }
        void Start()
        {
            
            animator = GetComponent<Animator>();
            SetUI();
        }

        // Update is called once per frame
        void Update()
        {

            if (GameObject.Find("Weapons").GetComponent<WeaponSwitcher>().selectedWeapon == 1)
            {
                //rend.enabled = true;

                if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentShells > 0)

                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    ShotgunRay();
                    currentShells -= 1;
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

        void ShotgunRay()
        {
            muzzleFlash.Play();
            audioSource.PlayOneShot(shotgunBlast, gunVolume);
            {
                Transform t_spawn = fpsCam.transform;

                //cooldown
                currentCooldown = fireRate;

                for (int i = 0; i < Mathf.Max(1, pellets); i++)
                {
                    //bloom
                    Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
                    t_bloom += Random.Range(-bloom, bloom) * t_spawn.up;
                    t_bloom += Random.Range(-bloom, bloom) * t_spawn.right;
                    t_bloom -= t_spawn.position;
                    t_bloom.Normalize();

                    //raycast
                    RaycastHit t_hit = new RaycastHit();
                    if (Physics.Raycast(t_spawn.position, t_bloom, out t_hit, 1000f))
                    {
                        //Debug.Log(t_hit.collider.name);

                        if (t_hit.rigidbody != null && t_hit.transform.tag == "Enemy")
                        {
                            t_hit.rigidbody.AddForce(-t_hit.normal * impactForce);
                            Instantiate(bloodSplatter, t_hit.point, Quaternion.LookRotation(t_hit.normal));
                            if (t_hit.rigidbody.gameObject.GetComponent<EnemyTakeDamage>() != null)
                            {
                                t_hit.rigidbody.GetComponent<EnemyTakeDamage>().ProcessDamage(Damage);
                            }
                        }
                        else
                        {
                            Instantiate(impactEffect, t_hit.point, Quaternion.LookRotation(t_hit.normal));
                        }
                        //GameObject t_newHole = Instantiate(bulletholePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
                        //t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                        //Destroy(t_newHole, 5f);
                    }
                }
            }
        }

        public void SetUI()
        {
            if (CurrentShellsAmmo != null)
            {
                CurrentShellsAmmo.text = currentShells.ToString();
            }
        }



    }
}
