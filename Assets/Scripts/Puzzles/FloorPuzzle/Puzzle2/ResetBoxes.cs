using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetBoxes : BasicButton
{
    public UnityEvent ResetAllBoxes;

    public override void OnInteract(InteractModule interactModule)
    {
        base.OnInteract(interactModule);
        ResetAllBoxes.Invoke();
        StartCoroutine(ConfirmPress());
    }
}

