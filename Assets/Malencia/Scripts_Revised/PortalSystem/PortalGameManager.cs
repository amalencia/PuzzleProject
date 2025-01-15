using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGameManager : MonoBehaviour
{
    [SerializeField] private Portal bluePortal;
    [SerializeField] private Portal pinkPortal;

    public bool PortalsReadyForRendering()
    {
        return true;


        //This needs to be return true if both portals are placed
    }

    public Portal GetPortal(int portalNumber)
    {
        if (portalNumber == 0) return pinkPortal;
        else { return bluePortal; }
    }


}
