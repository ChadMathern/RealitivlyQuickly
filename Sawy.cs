using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RQ
{
    public class Sawy : MonoBehaviour
    {
        public float swayIntensity;
        public float smooth;
        private Quaternion originRotation;


        private void Start()
        {
            originRotation = transform.localRotation;
        }
        void Update()
        {
            UpdateSway();
        }

        private void UpdateSway()
        {
            float xMouse = Input.GetAxis("Mouse X");
            float yMouse = Input.GetAxis("Mouse Y");

            Quaternion xadjust = Quaternion.AngleAxis(-swayIntensity * xMouse, Vector3.up);
            Quaternion yadjust = Quaternion.AngleAxis(swayIntensity * yMouse, Vector3.right);
            Quaternion targRotation = originRotation * xadjust * yadjust;

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targRotation, Time.deltaTime * smooth);


        }

    }
}
