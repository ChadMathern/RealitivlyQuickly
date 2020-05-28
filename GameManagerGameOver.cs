using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class GameManagerGameOver : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        public GameObject PanelGameOver;

        private void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.GameOverEvent += TurnOnGameOverPanel;
        }

        private void OnDisable()
        {
            gameManagerMaster.GameOverEvent += TurnOnGameOverPanel;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManagerMaster>();
        }

        void TurnOnGameOverPanel()
        {
            if (PanelGameOver != null)
            {
                PanelGameOver.SetActive(true);
            }
        }
    }
}
