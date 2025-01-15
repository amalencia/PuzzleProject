using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator innerDoor;
    [SerializeField] private Animator outerDoor;
    [SerializeField] private float closeTime;
    private void OnTriggerEnter(Collider other)
    {
        innerDoor.SetBool("Door", true);
        outerDoor.SetBool("Door", true);
        StartCoroutine(CloseAfter(closeTime));
    }
    IEnumerator CloseAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        innerDoor.SetBool("Door", false);
        outerDoor.SetBool("Door", false);
    }

}
