using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RQ
{
    public class Fireball : MonoBehaviour
    {
        public GameObject player;

        private void OnEnable()
        {
            player = GameObject.Find("player");

        }

        private void OnTriggerEnter(Collider other)
        {
            //PlayerHealth Heal = player.GetComponent<PlayerHealth>();
            if (other.CompareTag("Player"))
            {
                TakeHealth();
            }
        }


        void TakeHealth()
        {
            PlayerCanvasHurt hurteffect = player.GetComponent<PlayerCanvasHurt>();
            PlayerHealth Heal = player.GetComponent<PlayerHealth>();
            Heal.ReduceHealth(healthChange: 10);
            hurteffect.TurnOnHurtEffect(0);
            Heal.SetUI();
            Destroy(gameObject);
        }

        IEnumerator DestroyFireball()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}

