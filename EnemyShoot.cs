using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RQ
{
    public class EnemyShoot : MonoBehaviour
    {
        public Transform Player;
        //public Transform enemyshooter;
        public bool IsAttacking = false;
        public Rigidbody bullet;
        public Transform bulletSpawn;
        //private Vector3 Distance;
        //private float DistanceFrom;
        private float fireRate = 1f;
        private float nextFire = 0;
        //private int minDist = 19;
        //private int moveSpeed = 3;
        private EnemyMaster enemyMaster;
        public AudioClip fireBall;
        public float volume;

        private void OnEnable()
        {
            SetInitialReferences();
            Player = enemyMaster.myTarget;
        }
        void Update()
        {
            if (enemyMaster.myTarget != null)
            {
                IsAttacking = true;
                transform.LookAt(enemyMaster.myTarget);
            }
            else
            {
                IsAttacking = false;
            }

            if (IsAttacking)
            {
                Shooting();
            }

        }
        
        void SetInitialReferences()
        {
            enemyMaster = GetComponent<EnemyMaster>();
            
        }

        void Shooting()
        {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;

                    //var Shoot = 
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                AudioSource.PlayClipAtPoint(fireBall, transform.position, volume);
                Destroy(fireBall, fireBall.length);
            } 
        }
    }
}