using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] [Range(1f, 60f)] float zoomedInFOV;

    bool zoomedInToggle = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(!zoomedInToggle)
            {
                zoomedInToggle = true;
                fpsCamera.fieldOfView = zoomedInFOV;
            } else
            {
                zoomedInToggle = false;
                fpsCamera.fieldOfView = zoomedOutFOV;
            }
        }
    }
}
