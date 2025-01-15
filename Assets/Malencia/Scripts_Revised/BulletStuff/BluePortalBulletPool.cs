using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePortalBulletPool : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] List<BlueObject> availableObjects = new List<BlueObject>();
    [SerializeField] private BlueObject originalObject;
    [SerializeField] private const int amountOfCopies = 1;
    [SerializeField] private PortalPool bluePool;
    [SerializeField] private PortalMovementModule PortalMovementModule;
    [SerializeField] private PortalGameManager PortalGameManager;
    [SerializeField] private AudioSource AudioSource;

    private void Awake()
    {
        for (int i = 0; i <= amountOfCopies; i++)
        {
            BlueObject tempObject = Instantiate(originalObject, transform);
            tempObject.LinkToPool(this);
            tempObject.LinkToPortalGameManager(PortalGameManager);
            tempObject.LinkToAudioSource(AudioSource);
            //tempObject.LinkToPortalPool(bluePool);
            tempObject.GetRotation(PortalMovementModule);
            tempObject.gameObject.SetActive(false);
            availableObjects.Add(tempObject);
        }

    }

    private void CreateCopy()
    {
        BlueObject tempObject = Instantiate(originalObject, transform);
        tempObject.LinkToPool(this);
        tempObject.LinkToPortalGameManager(PortalGameManager);
        tempObject.LinkToAudioSource(AudioSource);
        //tempObject.LinkToPortalPool(bluePool);
        tempObject.GetRotation(PortalMovementModule);
        tempObject.gameObject.SetActive(false);
        availableObjects.Add(tempObject);
    }

    public BlueObject RetrieveAvailableItem()
    {
        if (availableObjects.Count == 0)
        {
            CreateCopy();
        }

        BlueObject tempObject = availableObjects[0];
        availableObjects.RemoveAt(0);
        tempObject.gameObject.SetActive(true);
        return tempObject;
    }

    public void ResetBullet(BlueObject cloneObject)
    {
        cloneObject.gameObject.SetActive(false);
        availableObjects.Add(cloneObject);
    }
}
