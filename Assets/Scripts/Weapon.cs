using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] [Range(0.01f, 2f)] float timeBetweenShots = .5f;

    bool canShoot = true;

    private void OnEnable()
    {
        //Stops weapons from no long shooting when swapping to a different before timeBetweenShots finishes
        canShoot = true;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            //Preventing multiple Coroutines from happening
            canShoot = false;

           StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ammoSlot.ReduceCurrentAmmo(ammoType);
            PlayMuzzleFlash();
            ProcessRaycast();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreatHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            //Returns null if player shoots something without EnemyHealth script attached
            if (target == null) { return; }
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreatHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //GameObject duration is same as ParticleEffect duration
        Destroy(impact, .1f);
    }
}
