using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBulletObject : MonoBehaviour
{
    [SerializeField] private float bulletResetTime;
    public Rigidbody rb;
    private BlueBulletPool poolOwner;
    private float timerToReset;
    private bool reseting;

    [SerializeField] private PortalPool bluePool;
    public void LinkToPool(BlueBulletPool owner)
    {
        poolOwner = owner;
    }

    public void LinkToPortalPool(PortalPool owner)
    {
        bluePool = owner;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (true/*collision.gameObject.layer == 6*/)
        {
            Vector3 location = collision.GetContact(0).point;
            Vector3 normal = collision.GetContact(0).normal;
            bluePool.PortalLocation(location, normal);
            Destroy(gameObject);
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
