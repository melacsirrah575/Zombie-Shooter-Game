using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 40f;

    Light playerLight;

    private void Start()
    {
        playerLight = GetComponent<Light>();
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    public void RestoreLightAngle(float restoreAngle)
    {
        playerLight.spotAngle = restoreAngle;
    }

    public void AddLightIntensity(float intensityAmount)
    {
        playerLight.intensity += intensityAmount;
    }

    private void DecreaseLightAngle()
    {
        if(playerLight.spotAngle <= minimumAngle)
        {
            return;
        } else
        {
            playerLight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }

    private void DecreaseLightIntensity()
    {
        playerLight.intensity -= lightDecay * Time.deltaTime;
    }
}
