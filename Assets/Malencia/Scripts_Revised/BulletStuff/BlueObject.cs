using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueObject : WarpObjects
{
    [SerializeField] private float bulletResetTime;
    public Rigidbody rb;
    private BluePortalBulletPool poolOwner;
    private float timerToReset;
    private bool reseting;
    private const int blueID = 1;
    [SerializeField] private int portalSetLayer;
    [SerializeField] private int portalLayer;
    [SerializeField] private PortalGameManager _portalManager;
    [SerializeField] private PortalMovementModule _portalMovementModule;
    [SerializeField] private PortalPool bluePool;


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


    public void LinkToPool(BluePortalBulletPool owner)
    {
        poolOwner = owner;
    }

    public void LinkToPortalPool(PortalPool owner)
    {
        bluePool = owner;
    }

    public void GetRotation(PortalMovementModule module)
    {
        _portalMovementModule = module;
    }

    private void Start()
    {
        int portalSetLayer = LayerMask.NameToLayer("SetPortalLayer");
        int portalLayer = LayerMask.NameToLayer("Portal");
    }



    private void OnCollisionEnter(Collision collision)
    {
        //Make sure the portal is only being created on the appropriate layer
        if(collision.gameObject.layer == 10)
        {
            // Orient the portal according to camera look direction and surface direction.
            Quaternion cameraRotation = _portalMovementModule.transform.rotation;
            Vector3 portalRight = cameraRotation * Vector3.right;

            if (Mathf.Abs(portalRight.x) >= Mathf.Abs(portalRight.z))
            {
                if (portalRight.x >= 0)
                {
                    portalRight = Vector3.right;
                }
                else
                {
                    portalRight = -Vector3.right;
                }
            }
            else
            {
                if (portalRight.z >= 0)
                {
                    portalRight = Vector3.forward;
                }
                else
                {
                    portalRight = -Vector3.forward;
                }
            }

            Vector3 portalForward = -collision.GetContact(0).normal;
            Vector3 portalUp = -Vector3.Cross(portalRight, portalForward);

            Quaternion portalRotation = Quaternion.LookRotation(portalForward, portalUp);

            Vector3 normposition = collision.GetContact(0).normal;
            Vector3 position = collision.GetContact(0).point;

            // Attempt to place the portal.
            bool wasPlaced = PortalGameManager.GetPortal(blueID).PlacePortal(collision.collider, position, portalRotation);
            if (wasPlaced)
            {
               // PortalSource.clip = yesPortal;
               // PortalSource.Play();
            }
            else
            {
               // PortalSource.clip = noPortal;
                //PortalSource.Play();
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
