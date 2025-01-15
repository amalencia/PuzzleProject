using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBulletPool : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] List<BlueBulletObject> availableObjects = new List<BlueBulletObject>();
    [SerializeField] private BlueBulletObject originalObject;
    [SerializeField] private const int amountOfCopies = 1;
    [SerializeField] private PortalPool bluePool;

    private void Awake()
    {
        for (int i = 0; i <= amountOfCopies; i++)
        {
            BlueBulletObject tempObject = Instantiate(originalObject, transform);
            tempObject.LinkToPool(this);
            tempObject.LinkToPortalPool(bluePool);
            tempObject.gameObject.SetActive(false);
            availableObjects.Add(tempObject);
        }

    }

    private void CreateCopy()
    {
        BlueBulletObject tempObject = Instantiate(originalObject, transform);
        tempObject.LinkToPool(this);
        tempObject.LinkToPortalPool(bluePool);
        tempObject.gameObject.SetActive(false);
        availableObjects.Add(tempObject);
    }

    public BlueBulletObject RetrieveAvailableItem()
    {
        if (availableObjects.Count == 0)
        {
            CreateCopy();
        }

        BlueBulletObject tempObject = availableObjects[0];
        availableObjects.RemoveAt(0);
        tempObject.gameObject.SetActive(true);
        return tempObject;
    }

    public void ResetBullet(BlueBulletObject cloneObject)
    {
        cloneObject.gameObject.SetActive(false);
        availableObjects.Add(cloneObject);
    }
}
