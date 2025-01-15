using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tmp
{
    public class PuzzleRoom : MonoBehaviour
    {
        public UnityEvent EvtOnExitRoom;

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                EvtOnExitRoom.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}