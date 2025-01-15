using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollider : MonoBehaviour
{
    [SerializeField] private bool isTouching;
    [SerializeField] private PortalMovementModule movementModule;
    private void OnTriggerEnter(Collider other)
    {
        isTouching = true;
        movementModule.GunIsTouching(isTouching);
    }

    private void OnTriggerExit(Collider other)
    {
        isTouching = false;
        movementModule.GunIsTouching(isTouching);
    }
}
