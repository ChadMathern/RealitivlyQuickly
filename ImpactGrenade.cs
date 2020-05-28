using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class ImpactGrenade : MonoBehaviour
    {

        public float radius = 5;
        public float force = 700;
        public GameObject explosionEffect;
        private Collider col;
        public int Damage = 75;
        public float gunVolume;
        public AudioClip impactExplode;
        private Transform myTransform;

        private void OnEnable()
        {
            
            myTransform = transform;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {


        }
        void OnCollisionEnter(Collision col)
        {
            //ContactPoint contact = col.contacts[0];
            Instantiate(explosionEffect, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(impactExplode, myTransform.position, gunVolume);
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
}
