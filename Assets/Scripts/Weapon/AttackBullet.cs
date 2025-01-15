using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private string _targetTag;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == _targetTag)
        {
            collision.gameObject.GetComponent<Tmp.HealthModule>().DeductHealth(_damage);
        }
        //gameObject.SetActive(false);
    }
}
