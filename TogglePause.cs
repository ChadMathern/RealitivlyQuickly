using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class TogglePause : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        private bool isPaused;
        public GameObject timeMaster;
        public GameObject MainCamera;
        public GameObject Player;

        private void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += CallTogglePause;
        }

        private void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= CallTogglePause;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManagerMaster>();
        }

        void CallTogglePause()
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
                timeMaster.SetActive(true);
                //MainCamera.GetComponent<MouseLook>().enabled = true;
                MainCamera.GetComponent<Viewbob>().enabled = true;
                //Player.GetComponent<MouseLook>().enabled = true;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
                timeMaster.SetActive(false);
                //MainCamera.GetComponent<MouseLook>().enabled = false;
                MainCamera.GetComponent<Viewbob>().enabled = false;
               //Player.GetComponent<MouseLook>().enabled = false;
            }
        }
    }
}
