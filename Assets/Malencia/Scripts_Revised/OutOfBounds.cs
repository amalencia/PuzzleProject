using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] private PortalRestartModule restartModule;

    private void OnTriggerEnter(Collider other)
    {
            restartModule.RestartLevel();
    }

    private void OnCollisionEnter(Collision collision)
    {
            restartModule.RestartLevel();
        
    }

}
