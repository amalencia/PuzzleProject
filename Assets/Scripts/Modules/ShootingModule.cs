using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingModule : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;

    [Header("Shooting")]
    //Reference to the camera 
    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody Bullet;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float bulletSpeed = 10f; 

    public void Shoot()
    {
        //Creating a prefab of the bullet 
        PooledObject tempPooled = pool.RetrieveAvailableItem();

        Rigidbody bulletInstantiate = tempPooled._rb;
        
        bulletInstantiate.position = shootingPoint.position;
        bulletInstantiate.rotation = shootingPoint.rotation;
        bulletInstantiate.AddForce(bulletSpeed * shootingPoint.forward, ForceMode.Impulse);
        //Destroy bullets 
        tempPooled.ResetBackToPool(2f);
    }
}
