using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void PlayerTakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            //Handle Player Death
            Debug.Log("Player has been hit!");
        }
    }
}
