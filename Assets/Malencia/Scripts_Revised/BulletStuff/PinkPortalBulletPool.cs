using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkPortalBulletPool : MonoBehaviour
{
    [SerializeField] List<PinkObject> availableObjects = new List<PinkObject>();
    [SerializeField] private PinkObject originalObject;
    [SerializeField] private const int amountOfCopies = 1;
    [SerializeField] private PortalPool pinkPool;
    [SerializeField] private PortalMovementModule PortalMovementModule;
    [SerializeField] private PortalGameManager PortalGameManager;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        for (int i = 0; i <= amountOfCopies; i++)
        {
            PinkObject tempObject = Instantiate(originalObject, transform);
            tempObject.LinkToPool(this);
            tempObject.LinkToPortalGameManager(PortalGameManager);
            tempObject.LinkToAudioSource(audioSource);
            //tempObject.LinkToPortalPool(pinkPool);
            tempObject.GetRotation(PortalMovementModule);
            tempObject.gameObject.SetActive(false);
            availableObjects.Add(tempObject);
        }

    }

    private void CreateCopy()
    {
        PinkObject tempObject = Instantiate(originalObject, transform);
        tempObject.LinkToPool(this);
        tempObject.LinkToPortalGameManager(PortalGameManager);
        //tempObject.LinkToAudioSource(audioSource);
        //tempObject.LinkToPortalPool(pinkPool);
        tempObject.GetRotation(PortalMovementModule);
        tempObject.gameObject.SetActive(false);
        availableObjects.Add(tempObject);
    }

    public PinkObject RetrieveAvailableItem()
    {
        if (availableObjects.Count == 0)
        {
            CreateCopy();
        }

        PinkObject tempObject = availableObjects[0];
        availableObjects.RemoveAt(0);
        tempObject.gameObject.SetActive(true);
        return tempObject;
    }

    public void ResetBullet(PinkObject cloneObject)
    {
        cloneObject.gameObject.SetActive(false);
        availableObjects.Add(cloneObject);
    }
}
