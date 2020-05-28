using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class GameManagerEventMaster : MonoBehaviour
    {
        public delegate void GeneralEvent();
        public event GeneralEvent myGeneralEvent;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CallMyGeneralEvent()
        {
            if (myGeneralEvent != null)
            {
                myGeneralEvent();
            }
        }
    }
}
