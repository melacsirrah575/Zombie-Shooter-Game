using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] AudioClip playerHurt;
    [SerializeField] TextMeshProUGUI playerHealth;

    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        playerHealth.text = maxHealth.ToString();
    }

    public void PlayerTakeDamage(float damage)
    {
        AudioSource.PlayClipAtPoint(playerHurt, transform.position);

        currentHealth -= damage;
        playerHealth.text = currentHealth.ToString();

        if(currentHealth <= 0)
        {
            playerHealth.text = "0";
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
