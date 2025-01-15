using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : WarpObjects
{
    [SerializeField] private PortalMovementModule PortalMovementModule;
    [SerializeField] private GameObject cloneObject;
    protected override void Awake()
    {
        cloneObject.SetActive(false);
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();

    }
    public override void Teleport()
    {
      

            base.Teleport();
            PortalMovementModule.ResetRotation();
        
    }
}
