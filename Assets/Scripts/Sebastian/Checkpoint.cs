using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tmp
{
    public class Checkpoint : MonoBehaviour
    {
        public UnityEvent<Checkpoint> EvtOnCheckpointReached;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (EvtOnCheckpointReached != null)
                {
                    GetComponent<Collider>().enabled = false;
                    Debug.Log("checkpoint reached");
                    EvtOnCheckpointReached.Invoke(this);
                }
            }
        }
    }
}