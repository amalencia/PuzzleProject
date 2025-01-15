using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //This will change depending on if it's opening or closing 
    [SerializeField]
    private AudioSource openDoor; 
    [SerializeField]
    private float moveDistance;
    [SerializeField]
    private float moveDuration;
    [SerializeField]
    private bool needskey;
    private bool isKeyCollected;

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.right * moveDistance; 
    }
    public void OpenDoor()
    {
        if(!needskey || (needskey && !isKeyCollected))
        {
            StartCoroutine(MoveDoor(targetPosition));
            openDoor.Play();
        } 
    }

    public void CloseDoor()
    {
        StartCoroutine(MoveDoor(initialPosition)); 
    }
    public void SetKeyCollected(bool collected)
    {
        isKeyCollected = collected;
        if(needskey && isKeyCollected)
        {
            OpenDoor();
        }
    }

    private IEnumerator MoveDoor(Vector3 target)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position; 

        while(elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startingPos, target, elapsedTime / moveDuration); 
            elapsedTime += Time.deltaTime;
            yield return null;  
        }

        transform.position = target; 
    }
}
