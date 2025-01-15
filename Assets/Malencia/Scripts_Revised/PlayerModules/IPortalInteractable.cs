using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPortalInteractable 
{
    public void OnHoverEnter();
    public void OnHoverExit();
    public void OnInteract(PortalInteractModule interactModule);
}
