using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] AudioClip pistolShootSound;
    [SerializeField] AudioClip shotgunShootSound;
    [SerializeField] AudioClip rifleShootSound;

    WeaponSwitcher weaponSwitcher;
    AudioSource audioSource;

    bool canShoot = true;

    private void Start()
    {
        weaponSwitcher = GetComponentInParent<WeaponSwitcher>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void OnEnable()
    {
        //Stops weapons from no longer shooting when swapping to a different before timeBetweenShots finishes
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();

        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            //Preventing multiple Coroutines from happening
            canShoot = false;

           StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ammoSlot.ReduceCurrentAmmo(ammoType);
            PlayAudio();
            PlayMuzzleFlash();
            ProcessRaycast();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayAudio()
    {
        //Not the most effective method but works for having only 3 guns
        if(weaponSwitcher.CurrentWeapon == 0)
        {
            audioSource.clip = pistolShootSound;
            audioSource.volume = 0.5f;
        } else if (weaponSwitcher.CurrentWeapon == 1)
        {
            audioSource.clip = shotgunShootSound;
            audioSource.volume = 1f;
        } else if (weaponSwitcher.CurrentWeapon == 2)
        {
            audioSource.clip = rifleShootSound;
            audioSource.volume = 0.75f;
        }

        audioSource.Play();
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
