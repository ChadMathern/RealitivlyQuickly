using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{

    public class RifleAmmoBox : MonoBehaviour
    {
        public int quantity;
        public bool isTriggerPickup;
        public GameObject rifle;
        public GameObject rifleParent;
        public AudioClip pickup;
        public float volume;


        private void OnEnable()
        {
            rifleParent = GameObject.Find("Weapons");
            rifle = rifleParent.transform.Find("M4_Carbine").gameObject;
            SetInitialReferences();
        }

        private void Update()
        {
            transform.Rotate(0, 2, 0 * Time.deltaTime);
        }

        void SetInitialReferences()
        {
            rifle.GetComponent<AssaultRifle>().SetUI();

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
            AssaultRifle Rifle = rifle.GetComponent<AssaultRifle>();
            if (other.CompareTag("Player") && isTriggerPickup && Rifle.currentRifleAmmo != Rifle.maxRifleAmmo)
            {
                TakeAmmo();
                AudioSource.PlayClipAtPoint(pickup, transform.position, volume);
            }
        }

        void TakeAmmo()
        {
            AssaultRifle Rifle = rifle.GetComponent<AssaultRifle>();
            Rifle.currentRifleAmmo += quantity;
            if (Rifle.currentRifleAmmo > Rifle.maxRifleAmmo)
            {
                Rifle.currentRifleAmmo = Rifle.maxRifleAmmo;
            }
            Rifle.SetUI();
            Destroy(gameObject);
        }
    }
}
