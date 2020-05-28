using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
    {
    public class EnemyHealth : MonoBehaviour
    {
        private EnemyMaster enemyMaster;
        public int enemyHealth = 100;
        public GameObject deathEffect;

        // Start is called before the first frame update

        private void OnEnable()
        {
            SetInitialReferences();
            enemyMaster.EventEnemyDeductHealth += DeductHealth;
        }

        private void OnDisable()
        {
            enemyMaster.EventEnemyDeductHealth -= DeductHealth;
        }

        void SetInitialReferences()
        {
            enemyMaster = GetComponent<EnemyMaster>();
        }

        void DeductHealth(int healthChange)
        {
            enemyHealth -= healthChange;
            if (enemyHealth <= 0)
            {
                enemyHealth = 0;
                enemyMaster.CallEventEnemyDie();
                Instantiate(deathEffect, transform);
                Destroy(gameObject);
                
            }
        }
    }
}
