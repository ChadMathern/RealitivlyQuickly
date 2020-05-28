using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RQ
{
    public class PlayerHealth : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        private PlayerMaster playerMaster;
        private PlayerCanvasHurt playerCanvasHurt;
        public int playerHealth;
        public Text healthText;

        // Start is called before the first frame update

        private void OnEnable()
        {
            SetInitialReferences();
            SetUI();
            playerMaster.EventPlayerHealthDecrease += ReduceHealth;
            playerMaster.EventPlayerHealthIncrease += IncreaseHealth;

        }
        private void OnDisable()
        {
            playerMaster.EventPlayerHealthDecrease -= ReduceHealth;
            playerMaster.EventPlayerHealthIncrease -= IncreaseHealth;
        }
        void Start()
        {
        
        }

        void SetInitialReferences()
        {
            playerMaster = GetComponent<PlayerMaster>();
            gameManagerMaster = GameObject.Find("GameManager").GetComponent<GameManagerMaster>();
        }

        public void ReduceHealth(int healthChange)
        {
            playerHealth -= healthChange;

            if (playerHealth <= 0)
            {
                playerHealth = 0;
                gameManagerMaster.CallGameOverEvent();

            }
            SetUI();
            
        }

        public void IncreaseHealth(int healthChange)
        {
            playerHealth += healthChange;
            if (playerHealth > 100)
            {
                playerHealth = 100;
            }

            SetUI();
        }

        public void SetUI()
        {
            if (healthText != null)
            {
                healthText.text = playerHealth.ToString();
            }
        }
    }
}