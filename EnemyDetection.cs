using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class EnemyDetection : MonoBehaviour
    {
        private EnemyMaster enemyMaster;
        private Transform myTransform;
        public Transform head;
        public LayerMask PlayerLayer;
        public LayerMask sightLayer;
        private float checkRate;
        private float nextCheck;
        private float detectRadius = 80;
        private RaycastHit hit;


        private void OnEnable()
        {
            SetInitialReferences();
            enemyMaster.EventEnemyDie += DisableThis;
        }

        private void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThis;
        }

        void SetInitialReferences()
        {
            enemyMaster = GetComponent<EnemyMaster>();
            myTransform = transform;

            if (head == null)
            {
                head = myTransform;
            }

            checkRate = Random.Range(0.8f, 1.2f);
        }
        void Update()
        {

            CarryOutDetection();
        }
        void CarryOutDetection()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;

                Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectRadius, PlayerLayer);

                if (colliders.Length > 0)
                {
                    foreach (Collider PotentialTargetCollider in colliders)
                    {
                        if (PotentialTargetCollider.CompareTag(GameManagerReferences._playerTag))
                        {
                            if (CanPotentialTargetBeSeen(PotentialTargetCollider.transform))
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    enemyMaster.CallEventEnemyLostTarget();
                }
            }

        }

        void DisableThis()
        {
            this.enabled = false;
        }

        bool CanPotentialTargetBeSeen (Transform potentialTarget)
        {
            if (Physics.Linecast(head.position, potentialTarget.position,out hit,sightLayer))
            {
                if (hit.transform == potentialTarget)
                {
                    enemyMaster.CallEventEnemySetNavTarget(potentialTarget);
                    return true;
                    
                }
                else
                {
                    enemyMaster.CallEventEnemyLostTarget();
                    return false;
                }
            }
            else
            {
                enemyMaster.CallEventEnemyLostTarget();
                return false;
            }
        }
    }
}
