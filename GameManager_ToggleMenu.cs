using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class GameManager_ToggleMenu : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        public GameObject menu;
        // Start is called before the first frame update
        void Start()
        {
            ToggleMenu();
        }

        // Update is called once per frame
        void Update()
        {
            CheckMenuToggleRequest();
        }

        private void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.GameOverEvent += ToggleMenu;
        }

        private void OnDisable()
        {
            gameManagerMaster.GameOverEvent -= ToggleMenu;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManagerMaster>();
        }

        void CheckMenuToggleRequest()
        {
            if (Input.GetKeyUp(KeyCode.P) && !gameManagerMaster.isGameOver)
            {
                ToggleMenu();
            }
        }

        void ToggleMenu()
        {
            if (menu != null)
            {
                menu.SetActive(!menu.activeSelf);
                gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
                gameManagerMaster.CallEventMenuToggle();
            }
            else
            {
                Debug.LogWarning("You need to assign a UI GameObject to the toggle menu script in the inspector");
            }
        }
    }
}
