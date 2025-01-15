using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalModule : MonoBehaviour
{
    [SerializeField] private Rigidbody projectile;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Camera _camera;
    [SerializeField] private ObjectPool pool;
    [SerializeField] private float bulletForce;
    [SerializeField] private float resetBulletTime2;

    [Header("LaserPointer")]
    [SerializeField] private Material redLine;
    [SerializeField] private Material blueLine;
    [SerializeField] private LineRenderer laserPointer;
    [SerializeField] private LayerMask portalLayer;
    [SerializeField] private float laserDistance;
    private Vector3 laserEnd;
    private bool LaserOn;

    [Header("PortalGun")]
    [SerializeField] private Rigidbody bluePortalBullet;
    [SerializeField] private Rigidbody orangePortalBullet;
    [SerializeField] private BlueBulletPool bluePool;
    [SerializeField] private OrangeBulletPool orangePool;

    public void PortalGunBlue()
    {
        BlueBulletObject tempPooled = bluePool.RetrieveAvailableItem();
        Rigidbody bulletInstnatiated = tempPooled.rb;

        bulletInstnatiated.position = shootingPoint.position;
        bulletInstnatiated.rotation = shootingPoint.rotation;

        bulletInstnatiated.AddForce(bulletForce * _camera.transform.forward, ForceMode.Impulse);
        tempPooled.ResetBackToPool(resetBulletTime2);
    }

    public void PortalGunOrange()
    {
        OrangeBulletObject tempPooled = orangePool.RetrieveAvailableItem();
        Rigidbody bulletInstnatiated = tempPooled.rb;

        bulletInstnatiated.position = shootingPoint.position;
        bulletInstnatiated.rotation = shootingPoint.rotation;

        bulletInstnatiated.AddForce(bulletForce * _camera.transform.forward, ForceMode.Impulse);
        tempPooled.ResetBackToPool(resetBulletTime2);
    }

    public void LaserPointer()
    {
        LaserOn = true;
    }

    public void LaserOff()
    {
        LaserOn = false;
        laserPointer.positionCount = 0;
    }

    private void Laser()
    {
        Ray ray = new Ray(shootingPoint.position, _camera.transform.forward);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData))
        {
            laserEnd = hitData.point;
        }
        else
        {
            laserEnd = ray.GetPoint(laserDistance);
        }
        Vector3[] array = new Vector3[2];
        array[0] = shootingPoint.position;
        array[1] = laserEnd;

        if (Physics.Raycast(ray, out hitData, laserDistance, portalLayer))
        {
            laserPointer.material = blueLine;
        }
        else
        {
            laserPointer.material = redLine;
        }
        laserPointer.startWidth = 0.2f;
        laserPointer.endWidth = 0.2f;
        laserPointer.widthMultiplier = 0.2f;
        laserPointer.positionCount = 2;
        laserPointer.SetPositions(array);
    }

    private void Update()
    {
        if (LaserOn)
        {
            Laser();
        }
    }
}
