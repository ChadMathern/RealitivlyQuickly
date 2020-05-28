using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class GameManagerToggleCursor : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        private bool isCusorLocked = true;
        private void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += ToggleCursorState;
        }

        private void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= ToggleCursorState;
        }
        // Update is called once per frame
        void Update()
        {
            CheckIfCursorShouldBeLocked();
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManagerMaster>();
        }

        void ToggleCursorState()
        {
            isCusorLocked = !isCusorLocked;
        }

        void CheckIfCursorShouldBeLocked()
        {
            if (isCusorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
