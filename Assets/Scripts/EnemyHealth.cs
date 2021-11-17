using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;
    float currentHitPoints;

    private void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    public void TakeDamage(float damage)
    {
        currentHitPoints -= damage;
        if(currentHitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
