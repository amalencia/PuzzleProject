using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBulletObject : MonoBehaviour
{
    [SerializeField] private float bulletResetTime;
    public Rigidbody rb;
    private OrangeBulletPool poolOwner;
    private float timerToReset;
    private bool reseting;

    [SerializeField] private PortalPool orangePool;
    public void LinkToPool(OrangeBulletPool owner)
    {
        poolOwner = owner;
    }

    public void LinkToPortalPool(PortalPool owner)
    {
        orangePool = owner;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (/*collision.gameObject.layer == 6*/true)
        {
            Vector3 location = collision.GetContact(0).point;
            Vector3 normal = collision.GetContact(0).normal;
            orangePool.PortalLocation(location, normal);
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
