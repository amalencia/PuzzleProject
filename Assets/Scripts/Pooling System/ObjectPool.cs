using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] private List<PooledObject> availableObject = new List<PooledObject>();
    [SerializeField] private PooledObject originalObject;
    [SerializeField] private int amountOfCopies;

    private void Awake()
    {
        for(int index = 0; index < amountOfCopies; index++)
        {
            CreateCopy(); 
        }
    }

    private void CreateCopy()
    {
        PooledObject tempObject = Instantiate(originalObject, transform);
        tempObject.LinkToPool(this);
        tempObject.gameObject.SetActive(false);
        availableObject.Add(tempObject);
    }

    public PooledObject RetrieveAvailableItem()
    {
        if(availableObject.Count == 0)
        {
            CreateCopy();
        }
        PooledObject tempObject = availableObject[0];
        availableObject.RemoveAt(0);
        tempObject.gameObject.SetActive(true); 
        return tempObject;
    }

    public void ResetBullet(PooledObject cloneObject)
    {
        cloneObject.gameObject.SetActive(false);
        availableObject.Add(cloneObject); 
    }
}
