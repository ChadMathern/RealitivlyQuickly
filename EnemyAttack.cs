using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class EnemyAttack : MonoBehaviour
    {
        private EnemyMaster enemyMaster;
        private Transform attackTarget;
        private Transform myTransform;
        public float attackRate = .1f;
        private float nextAttack;
        private float attackRange = 8f;
        private int attackDamage = 5;
        public AudioClip swing;
        public float volume;
        private void OnEnable()
        {
            SetInitialReferences();
            enemyMaster.EventEnemyDie += DisableThis;
            enemyMaster.EventEnemySetNavTarget += SetAttackTarget;
        }

        private void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThis;
            enemyMaster.EventEnemySetNavTarget -= SetAttackTarget;
        }

        void SetInitialReferences()
        {
            enemyMaster = GetComponent<EnemyMaster>();
            myTransform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            TryToAttack();
        }

        void SetAttackTarget(Transform targetTransform)
        {
            attackTarget = targetTransform;
        }

        void TryToAttack()
        {
            if (attackTarget != null)
            {
                if (Time.time>nextAttack)
                {
                    nextAttack = Time.time + attackRate;
                    if (Vector3.Distance(myTransform.position, attackTarget.position) <= attackRange)
                    {
                        Vector3 lookAtVector = new Vector3(attackTarget.position.x, myTransform.position.y, attackTarget.position.z);
                        myTransform.LookAt(lookAtVector);
                        enemyMaster.CallEventEnemyAttack();
                        enemyMaster.isOnRoute = false;
                        AudioSource.PlayClipAtPoint(swing, transform.position, volume);
                    }
                }
            }
        }

        void OnEnemyAttack()
        {
            if (attackTarget != null)
            {
                if (Vector3.Distance(myTransform.position, attackTarget.position) <= attackRange && attackTarget.GetComponent<PlayerMaster>() != null)
                {
                    Vector3 toOther = attackTarget.position - myTransform.position;

                    if(Vector3.Dot(toOther,myTransform.forward) > 0.1f)
                    {
                        attackTarget.GetComponent<PlayerMaster>().CalleEventPlayerHealthDecrease(attackDamage);
                    }
                }
            }
        }

        void DisableThis()
        {
            this.enabled = false;
        }

    }
}
