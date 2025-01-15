using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private GameObject[] turrets;
    [SerializeField] private GameObject cube;
    
    private int destroyedTurretsCount = 0;
    // Start is called before the first frame update
    private void Start()
    {
        foreach(var turret in turrets)
        {
            EnemyHealth enemyHealth = turret.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.OnTurretDestoryed.AddListener(OnTurretDestroyed);
            }
        }
    }
    
    private void OnTurretDestroyed()
    {
        destroyedTurretsCount++;
        if (destroyedTurretsCount >= turrets.Length)
        {
            DeactivatedCube();
        }
    }

    private void DeactivatedCube()
    {
        cube.SetActive(false);
    }
}
