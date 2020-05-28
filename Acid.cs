using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RQ
{


    public class Acid : MonoBehaviour
    {
        public GameObject player;
        int healthDecaySpeed = 2;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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
            PlayerHealth Hurt = player.GetComponent<PlayerHealth>();
            Hurt.ReduceHealth(healthChange: 100);
            hurteffect.TurnOnHurtEffect(0);
            Hurt.SetUI();
        }

    }
}