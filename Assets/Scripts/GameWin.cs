using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    [SerializeField] Canvas winCanvas;

    private void Start()
    {
        winCanvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            winCanvas.enabled = true;
        }
    }
}
