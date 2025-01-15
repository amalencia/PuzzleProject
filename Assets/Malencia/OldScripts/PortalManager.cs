using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private PortalPool bluePool;
    [SerializeField] private PortalPool orangePool;
    public bool CheckIfReadyForCamera()
    {
        if(bluePool.IsThisPortalReady() && orangePool.IsThisPortalReady())
        {

            return true;
        }
        else
        {
            return false;
        }
    }
}
