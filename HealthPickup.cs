using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class HealthPickup : MonoBehaviour
    {
        public int quantity;
        public bool isTriggerPickup;
        public GameObject player;
        private PlayerMaster playerMaster;
        public AudioClip pickup;
        public float volume;




        private void OnEnable()
        {
            player = GameObject.Find("player");
            SetInitialReferences();
        }

        private void Update()
        {
            transform.Rotate(0, 0, 120 * Time.deltaTime);
        }

        void SetInitialReferences()
        {
            player.GetComponent<PlayerHealth>().SetUI();

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
            PlayerHealth Heal = player.GetComponent<PlayerHealth>();
            if (other.CompareTag("Player") && isTriggerPickup && Heal.playerHealth != 100)
            {
                TakeHealth();
                AudioSource.PlayClipAtPoint(pickup, transform.position, volume);
            }
        }


        void TakeHealth()
        {
            PlayerHealth Heal = player.GetComponent<PlayerHealth>();
            Heal.IncreaseHealth(healthChange: 20);
            if (Heal.playerHealth > 100)
            {
                Heal.playerHealth = 100;
            }
            Heal.SetUI();
            Destroy(gameObject);
        }
    }
}
