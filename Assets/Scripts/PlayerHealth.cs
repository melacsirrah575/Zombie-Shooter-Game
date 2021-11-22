using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] AudioClip playerHurt;

    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void PlayerTakeDamage(float damage)
    {
        AudioSource.PlayClipAtPoint(playerHurt, transform.position);
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
