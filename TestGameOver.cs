using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class TestGameOver : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.O))
            {
                GetComponent<GameManagerMaster>().CallGameOverEvent();
            }
        }
    }
}
