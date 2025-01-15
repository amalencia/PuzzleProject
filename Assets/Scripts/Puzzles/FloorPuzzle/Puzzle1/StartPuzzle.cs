using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartPuzzle : MonoBehaviour
{
    public UnityEvent StartTimer;
    private bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !started)
        {
            StartTimer.Invoke();
            started = true;
        }
    }
}
