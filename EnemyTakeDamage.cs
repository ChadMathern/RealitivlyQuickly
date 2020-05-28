using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class EnemyTakeDamage : MonoBehaviour
    {
        private EnemyMaster enemyMaster;
        public int damageMultiplier;
        public bool shouldRemoveCollider;

        private void OnEnable()
        {
            SetInitialReferences();
            enemyMaster.EventEnemyDie += RemoveThis;
        }

        private void OnDisable()
        {
            enemyMaster.EventEnemyDie -= RemoveThis;
        }

        void SetInitialReferences()
        {
            enemyMaster = transform.GetComponent<EnemyMaster>();
        }

        public void ProcessDamage(int damage)
        {
            int damageToApply = damage * damageMultiplier;
            enemyMaster.CallEventEnemyDeductHealth(damageToApply);
        }

        void RemoveThis()
        {
            if (shouldRemoveCollider)
            {
                if (GetComponent<Collider>() != null)
                {
                    Destroy(GetComponent<Collider>());
                }
                if (GetComponent<Rigidbody>() != null)
                {
                    Destroy(GetComponent<Rigidbody>());
                }
            }

            Destroy(this);


        }

    }
}
