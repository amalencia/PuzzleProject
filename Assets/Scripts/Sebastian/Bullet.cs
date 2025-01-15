using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyComponent))
        {
            enemyComponent.TakeDamage(damage); 
        }
    }
}
