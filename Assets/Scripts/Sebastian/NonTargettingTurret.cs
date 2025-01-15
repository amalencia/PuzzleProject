using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTargettingTurret : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform laserAim;

    public float range;
    public int damage; //Amount of damage to deal to the player 
    public float damageInterval = 1f; //Creates an interval between each damage 
    private float lastDamageTime;

    private RaycastHit rayHit;
    private Ray ray;

    private HealthModule playerHealth;
    void Start()
    {
        playerHealth = FindObjectOfType<HealthModule>();
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        ray = new(laserAim.position, laserAim.up);
        if (Physics.Raycast(ray, out rayHit, range))
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
            lineRenderer.SetPosition(1, laserAim.position + laserAim.up * range);
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
