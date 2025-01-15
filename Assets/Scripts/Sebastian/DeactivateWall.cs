using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject cube;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(wall != null)
            {
                wall.SetActive(false);
            }

            if (cube != null)
            {
                Destroy(cube); 
            }
        }
    }
}
