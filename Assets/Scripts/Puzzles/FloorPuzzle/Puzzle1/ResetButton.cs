using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetButton : BasicButton
{
    public UnityEvent ResetAllPads;

    public override void OnInteract(InteractModule interactModule)
    {
        base.OnInteract(interactModule);
        ResetAllPads.Invoke();
        StartCoroutine(ConfirmPress());
    }
}
