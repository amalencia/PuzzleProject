using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteracterable
{
    public void OnHoverEnter();
    public void OnHoverExit();
    public void OnInteract(InteractModule interactModule); 
}
