using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class PlayerCanvasHurt : MonoBehaviour
    {
        public GameObject hurtCanvas;
        private PlayerMaster playerMaster;
        private float secondsTillHide = .3f;
        AudioSource audioSource;
        public AudioClip owie;
        public float gunVolume;
        // Start is called before the first frame update

        private void OnEnable()
        {
            audioSource = GetComponent<AudioSource>();
            SetInitialRefrences();
            playerMaster.EventPlayerHealthDecrease += TurnOnHurtEffect;
        }

        private void OnDisable()
        {
            playerMaster.EventPlayerHealthDecrease -= TurnOnHurtEffect;
        }
        void Start()
        {

        }

        void SetInitialRefrences()
        {
            playerMaster = GetComponent<PlayerMaster>();
            
        }

        public void TurnOnHurtEffect(int dummy)
        {
            if (hurtCanvas != null)
            {
                StopAllCoroutines();
                hurtCanvas.SetActive(true);
                audioSource.PlayOneShot(owie, gunVolume);
                StartCoroutine(ResetHurtCanvas());
            }
        }

        IEnumerator ResetHurtCanvas()
        {
            yield return new WaitForSeconds(secondsTillHide);
            hurtCanvas.SetActive(false);
        }
    }
}