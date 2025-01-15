using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBody : MonoBehaviour
{
    [SerializeField] private Melee _melee;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == _melee.TargetTag && _melee.CanAttack)
        {
            other.gameObject.GetComponent<Tmp.HealthModule>().DeductHealth(_melee.Damage);
        }
    }
}
