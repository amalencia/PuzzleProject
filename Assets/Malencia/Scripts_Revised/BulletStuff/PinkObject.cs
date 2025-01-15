using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PinkObject : WarpObjects
{
    [SerializeField] private float bulletResetTime;
    public Rigidbody rb;
    private PinkPortalBulletPool poolOwner;
    private float timerToReset;
    private bool reseting;
    private const int pinkID = 0;
    [SerializeField] private PortalGameManager _portalManager;
    [SerializeField] private PortalPool pinkPool;
    [SerializeField] private PortalMovementModule _portalMovementModule;


    [SerializeField] private PortalGameManager PortalGameManager;

    [Header("Audi0")]
    [SerializeField] private AudioClip noPortal;
    [SerializeField] private AudioClip yesPortal;
    [SerializeField] private AudioSource PortalSource;

    public void LinkToPortalGameManager(PortalGameManager portalManager)
    {
        PortalGameManager = portalManager;
    }
    public void LinkToAudioSource(AudioSource source)
    {
        source = PortalSource;
    }

    public void LinkToPool(PinkPortalBulletPool owner)
    {
        poolOwner = owner;
    }

    public void LinkToPortalPool(PortalPool owner)
    {
        pinkPool = owner;
    }

    public void GetRotation(PortalMovementModule module)
    {
        _portalMovementModule = module;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Make sure the portal is only being created on the appropriate layer
        if (collision.gameObject.layer == 10)
        {
            //you need to determine the direction to set the portal via the player's current direction
            Quaternion cameraRotation = _portalMovementModule.transform.rotation;
            Vector3 portalRight = cameraRotation * Vector3.right;

            if (Mathf.Abs(portalRight.x) >= Mathf.Abs(portalRight.z))
            {
                if(portalRight.x >= 0)
                {
                    portalRight = Vector3.right;
                } else
                {
                    portalRight = -Vector3.right;
                }
            }
            else
            {
                if(portalRight.z >= 0)
                {
                    portalRight = Vector3.forward;
                } else
                {
                    portalRight = -Vector3.forward;
                }
            }

            Vector3 portalForward = -collision.GetContact(0).normal;
            Vector3 portalUp = -Vector3.Cross(portalRight, portalForward);

            Quaternion portalRotation = Quaternion.LookRotation(portalForward, portalUp);

            // Attempt to place the portal.
            bool wasPlaced = PortalGameManager.GetPortal(pinkID).PlacePortal(collision.collider, collision.GetContact(0).point, portalRotation);
            if(wasPlaced)
            {
                //PortalSource.clip = yesPortal;
               // PortalSource.Play();
            } else
            {
                //PortalSource.clip = noPortal;
               // PortalSource.Play(); ;
            }
            gameObject.SetActive(false);
        }
    }

    public void ResetBackToPool()
    {
        rb.velocity = Vector3.zero;
        reseting = false;
        poolOwner.ResetBullet(this);
    }

    public void ResetBackToPool(float time)
    {
        timerToReset = bulletResetTime;
        reseting = true;
    }

    private void Update()
    {
        if (reseting)
        {
            timerToReset -= Time.deltaTime;
            if (timerToReset <= 0)
            {
                ResetBackToPool();
            }
        }
    }
}
