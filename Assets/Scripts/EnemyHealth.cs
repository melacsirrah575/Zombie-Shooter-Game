using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;
    [SerializeField] AudioClip zombieDeathAudio;

    bool isDead = false;
    public bool IsDead() { return isDead; }


    float currentHitPoints;

    private void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        currentHitPoints -= damage;
        if(currentHitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(isDead) { return; }
        isDead = true;
        GetComponent<Animator>().SetTrigger("Dead");
        AudioSource.PlayClipAtPoint(zombieDeathAudio, transform.position);
    }
}
