using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class SniperAmmoBox : MonoBehaviour
    {
        public int quantity;
        public bool isTriggerPickup;
        public GameObject sniper;
        public GameObject sniperParent;
        public AudioClip pickup;
        public float volume;

        private void OnEnable()
        {
            sniperParent = GameObject.Find("Weapons");
            sniper = sniperParent.transform.Find("L96_Sniper_Rifle").gameObject;
            SetInitialReferences();
        }

        private void Update()
        {
            transform.Rotate(0, 2, 0 * Time.deltaTime);
        }

        void SetInitialReferences()
        {
            sniper.GetComponent<SniperRifle>().SetUI();

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
            SniperRifle Sniper = sniper.GetComponent<SniperRifle>();
            if (other.CompareTag("Player") && isTriggerPickup && Sniper.currentSniperAmmo != Sniper.maxSniperAmmo)
            {
                TakeAmmo();
                AudioSource.PlayClipAtPoint(pickup, transform.position, volume);
            }
        }

        void TakeAmmo()
        {
            SniperRifle Sniper = sniper.GetComponent<SniperRifle>();
            Sniper.currentSniperAmmo += quantity;
            if (Sniper.currentSniperAmmo > Sniper.maxSniperAmmo)
            {
                Sniper.currentSniperAmmo = Sniper.maxSniperAmmo;
            }
            Sniper.SetUI();
            Destroy(gameObject);
        }
    }
}
