using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFreeController : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjectsToDisable;

    public void DisableGameObjects()
    {
        foreach (var obj in _gameObjectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
