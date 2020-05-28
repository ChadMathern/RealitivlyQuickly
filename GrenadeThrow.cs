using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrow : MonoBehaviour
{
    public float ThrowForce = 40f;
    public GameObject grenadePrefab;
    public int currentGrenadeCount;
    public int maxGrenadeCount;
    public Text currentGrenadeAmmo;
    // Start is called before the first frame update
    void Start()
    {
        SetUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && currentGrenadeCount > 0)
        {
            ThrowGrenade();
            currentGrenadeCount -= 1;
            SetUI();
        }
        
    }

    void ThrowGrenade()
    {
       GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * ThrowForce, ForceMode.VelocityChange);
    }
    public void SetUI()
    {
        if (currentGrenadeAmmo != null)
        {
            currentGrenadeAmmo.text = currentGrenadeCount.ToString();
        }
    }
}
