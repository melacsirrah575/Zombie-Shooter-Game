using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 40f;
    [SerializeField] Slider batteryDisplaySlider;

    Light playerLight;

    private void Start()
    {
        playerLight = GetComponent<Light>();
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
        DisplayBatterySlider();
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

    private void DisplayBatterySlider()
    {
        batteryDisplaySlider.minValue = 0f;
        batteryDisplaySlider.maxValue = 3.5f;
        batteryDisplaySlider.value = playerLight.intensity;
    }
}
