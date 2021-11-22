using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
    [SerializeField] AudioClip bulletPickup;
    [SerializeField] AudioClip shellPickup;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            if(ammoType == AmmoType.LightBullets || ammoType == AmmoType.HeavyBullets)
            {
                AudioSource.PlayClipAtPoint(bulletPickup, transform.position, 0.5f);
            } else
            {
                AudioSource.PlayClipAtPoint(shellPickup, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
