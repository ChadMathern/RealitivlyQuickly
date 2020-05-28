using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RQ
{
    public class PistolAmmoBox : MonoBehaviour
    {
        public int quantity;
        public bool isTriggerPickup;
        public GameObject pistol;
        public GameObject pistolParent;
        public AudioClip pickup;
        public float volume;


        private void OnEnable()
        {
            pistolParent = GameObject.Find("Weapons");
            pistol = pistolParent.transform.Find("Pistol").gameObject;
            SetInitialReferences();
        }

        private void Update()
        {
            transform.Rotate(0, 2, 0 * Time.deltaTime);
        }

        void SetInitialReferences()
        {
            pistol.GetComponent<Pistol>().SetUI();

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
            Pistol Pistol = pistol.GetComponent<Pistol>();
            if (other.CompareTag("Player") && isTriggerPickup && Pistol.currentPistolAmmo != Pistol.maxPistolAmmo)
            {
                TakeAmmo();
                AudioSource.PlayClipAtPoint(pickup, transform.position, volume);
            }
        }

        void TakeAmmo()
        {
            Pistol Pistol = pistol.GetComponent<Pistol>();
            Pistol.currentPistolAmmo += quantity;
            if (Pistol.currentPistolAmmo > Pistol.maxPistolAmmo)
            {
                Pistol.currentPistolAmmo = Pistol.maxPistolAmmo;
            }
            Pistol.SetUI();
            Destroy(gameObject);
        }
    }
}
