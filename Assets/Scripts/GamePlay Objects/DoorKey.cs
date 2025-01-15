using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorKey : MonoBehaviour
{
    //This should be used for the script that requires key. 
    [SerializeField] private UnityEvent OnInteracted;
    [SerializeField] private AudioSource openDoorSfx; 
    private DoorController doorController;

    private void Start()
    {
        doorController = GetComponent<DoorController>();
        if(doorController != null)
        {
            Debug.LogError("DoorController is not assigned in the DoorKey script on " + gameObject.name); 
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnInteracted.Invoke();
            if (doorController != null)
            {
                doorController.SetKeyCollected(true);
            }
            HideKey();
        }
    }

    private void HideKey()
    {
        openDoorSfx.Play();
        Destroy(gameObject, 1.5f); 
    }
}
