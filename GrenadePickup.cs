using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    public int quantity;
    public bool isTriggerPickup;
    public GameObject grenade;
    public AudioClip pickup;
    public float volume;


    private void OnEnable()
    {
        grenade = GameObject.Find("Main Camera");
        SetInitialReferences();
    }

    private void Update()
    {
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }

    void SetInitialReferences()
    {
        grenade.GetComponent<GrenadeThrow>().SetUI();

        if (isTriggerPickup)
        {
            if (GetComponent<CapsuleCollider>() != null)
            {
                GetComponent<CapsuleCollider>().isTrigger = true;
            }
        }

        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GrenadeThrow Grenade = grenade.GetComponent<GrenadeThrow>();
        if (other.CompareTag("Player") && isTriggerPickup && Grenade.currentGrenadeCount != Grenade.maxGrenadeCount)
        {
            TakeAmmo();
            AudioSource.PlayClipAtPoint(pickup, transform.position, volume);
        }
    }

    void TakeAmmo()
    {
        GrenadeThrow Grenade = grenade.GetComponent<GrenadeThrow>();
        Grenade.currentGrenadeCount += quantity;
        if (Grenade.currentGrenadeCount > Grenade.maxGrenadeCount)
        {
            Grenade.currentGrenadeCount = Grenade.maxGrenadeCount;
        }
        Grenade.SetUI();
        Destroy(gameObject);
    }
}
