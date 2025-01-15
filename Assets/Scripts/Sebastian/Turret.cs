using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //This turret script will just follow the player 
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform laserAim;
    [SerializeField] private Transform turretSight;
    [SerializeField] private Transform AimTransform; //Player's transform 
    [SerializeField] private bool shouldTargetPlayer = true;
    [SerializeField] private LayerMask playerLayer;

    public float range = 200;
    public int damage = 10; //Amount of damage to deal to the player 
    public float damageInterval = 1f; //Creates an interval between each damage 

    private float lastDamageTime;
    private bool isPlayerInRange = true; 

    private RaycastHit rayHit;
    private Ray ray;

    private HealthModule playerHealth;
    void Start()
    {
        playerHealth = FindObjectOfType<HealthModule>();
        lineRenderer.positionCount = 2;

        if (turretSight == null || laserAim == null)
        {
            Debug.LogError("LaserAim or TurretSIght is not assigned."); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldTargetPlayer && isPlayerInRange && AimTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(turretSight.position, AimTransform.position);

            if(distanceToPlayer <= range)
            {
                //This updates the laserAim to follow the player's position 
                laserAim.LookAt(AimTransform);

                ray = new Ray(laserAim.position, laserAim.forward);

                if (Physics.Raycast(ray, out rayHit, range, playerLayer))
                {
                    lineRenderer.SetPosition(0, laserAim.position);
                    lineRenderer.SetPosition(1, rayHit.point);

                    if (rayHit.collider.CompareTag("Player") && Time.time - lastDamageTime >= damageInterval)
                    {
                        playerHealth.DeductHealth(damage);
                        lastDamageTime = Time.time;
                    }
                }
                else
                {
                    lineRenderer.SetPosition(0, laserAim.position);
                    lineRenderer.SetPosition(1, laserAim.position + laserAim.forward * range);
                }
            }
            else
            {
                lineRenderer.SetPosition(0, laserAim.position);
                lineRenderer.SetPosition(1, laserAim.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(laserAim.position, ray.direction * range);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rayHit.point, 0.23f);
    }
}
