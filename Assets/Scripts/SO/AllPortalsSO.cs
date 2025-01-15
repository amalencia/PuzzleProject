using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SOs/All Portals", fileName = "AllPortals")]
public class AllPortalsSO : ScriptableObject
{
    public PortalSO[] portals;

    public void SetAllActive()
    {
        foreach (var portal in portals)
        {
            portal.SetActive();
        }
    }
}
