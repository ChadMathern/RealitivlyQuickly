using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class EnemyAnimation : MonoBehaviour
    {
        private EnemyMaster enemyMaster;
        private Animator myAnimator;

        private void OnEnable()
        {
            SetInitialReferences();
            enemyMaster.EventEnemyDie += DisableAnimator;
            enemyMaster.EventEnemyAttack += SetAnimationAttack;
        }

        private void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableAnimator;
            enemyMaster.EventEnemyAttack -= SetAnimationAttack;
        }

        void SetInitialReferences()
        {
            enemyMaster = GetComponent<EnemyMaster>();

            if (GetComponent<Animator>() != null)
            {
                myAnimator = GetComponent<Animator>();
            }
        }

        void SetAnimationIdle()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
          
                }
            }
        }

        void SetAnimationAttack()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger("Swing");
                }
            }
        }

        void DisableAnimator()
        {
            if (myAnimator != null)
            {
                myAnimator.enabled = false;
            }
        }


    }
}
