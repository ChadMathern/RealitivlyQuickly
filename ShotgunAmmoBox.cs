using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class ShotgunAmmoBox : MonoBehaviour
    {
        public int quantity;
        public bool isTriggerPickup;
        public GameObject shotgun;
        public GameObject shotgunParent;
        public AudioClip pickup;
        public float volume;



        private void OnEnable()
        {
            shotgunParent = GameObject.Find("Weapons");
            shotgun = shotgunParent.transform.Find("870_Shotgun").gameObject;
            SetInitialReferences();
        }

        private void Update()
        {

            transform.Rotate(0, 2, 0 * Time.deltaTime);
        }

        void SetInitialReferences()
        {
            shotgun.GetComponent<Shotgun2>().SetUI();

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
            Shotgun2 Shotgun = shotgun.GetComponent<Shotgun2>();
            if (other.CompareTag("Player") && isTriggerPickup && Shotgun.currentShells != Shotgun.maxShells)
            {
                TakeAmmo();
                AudioSource.PlayClipAtPoint(pickup, transform.position, volume);
            }
        }

        void TakeAmmo()
        {
            Shotgun2 Shotgun = shotgun.GetComponent<Shotgun2>();
            Shotgun.currentShells += quantity;
            if (Shotgun.currentShells > Shotgun.maxShells)
            {
                Shotgun.currentShells = Shotgun.maxShells;
            }
            Shotgun.SetUI();
            Destroy(gameObject);
        }
    }
}