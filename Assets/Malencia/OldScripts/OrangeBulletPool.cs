using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBulletPool : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] List<OrangeBulletObject> availableObjects = new List<OrangeBulletObject>();
    [SerializeField] private OrangeBulletObject originalObject;
    [SerializeField] private const int amountOfCopies = 1;
    [SerializeField] private PortalPool orangePool;

    private void Awake()
    {
        for (int i = 0; i <= amountOfCopies; i++)
        {
            OrangeBulletObject tempObject = Instantiate(originalObject, transform);
            tempObject.LinkToPool(this);
            tempObject.LinkToPortalPool(orangePool);
            tempObject.gameObject.SetActive(false);
            availableObjects.Add(tempObject);
        }

    }

    private void CreateCopy()
    {
        OrangeBulletObject tempObject = Instantiate(originalObject, transform);
        tempObject.LinkToPool(this);
        tempObject.LinkToPortalPool(orangePool);
        tempObject.gameObject.SetActive(false);
        availableObjects.Add(tempObject);
    }

    public OrangeBulletObject RetrieveAvailableItem()
    {
        if (availableObjects.Count == 0)
        {
            CreateCopy();
        }

        OrangeBulletObject tempObject = availableObjects[0];
        availableObjects.RemoveAt(0);
        tempObject.gameObject.SetActive(true);
        return tempObject;
    }

    public void ResetBullet(OrangeBulletObject cloneObject)
    {
        cloneObject.gameObject.SetActive(false);
        availableObjects.Add(cloneObject);
    }
}
