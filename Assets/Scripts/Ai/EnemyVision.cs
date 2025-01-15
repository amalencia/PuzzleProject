using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private AIController mycontroller;
        private void OnTriggerEnter(Collider other)
        {
            mycontroller.ChangeState(new ChaseState(mycontroller, other.transform));
            Debug.Log("Player has entered the trigger");
        }

        private void OnTriggerExit(Collider other)
        {
            mycontroller.ChangeState(new PatrolState(mycontroller));
            Debug.Log("Player has left the trigger");
        }
    }
}