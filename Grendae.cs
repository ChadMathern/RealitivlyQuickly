using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class Grendae : MonoBehaviour
    {
        public float delay = 3f;
        public float radius = 5;
        public float force = 700;
        public int Damage = 100;
        bool hasExploded = false;
        float countdown;
        public float gunVolume;
        public GameObject explosionEffect;
        public AudioClip grenadeExplode;
        public AudioClip grenadeBounce;
        private Transform myTransform;
        // Start is called before the first frame update

        private void OnEnable()
        {
            myTransform = transform;
        }
        void Start()
        {
            countdown = delay;
        }

        // Update is called once per frame
        void Update()
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f && !hasExploded)
            {
                explode();
                hasExploded = true;
            }

            void explode()
            {
                Instantiate(explosionEffect, transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(grenadeExplode, myTransform.position, gunVolume);
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

                foreach (Collider nearbyObject in colliders)
                {
                    Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(force, transform.position, radius);
                        if (rb.gameObject.GetComponent<EnemyTakeDamage>() != null)
                        {
                            rb.GetComponent<EnemyTakeDamage>().ProcessDamage(Damage);
                        }
                    }
                }



                Destroy(gameObject);
            }
        }

        void OnCollisionEnter(Collision col)
        {
            AudioSource.PlayClipAtPoint(grenadeBounce, myTransform.position, gunVolume);
        }
    }
}
