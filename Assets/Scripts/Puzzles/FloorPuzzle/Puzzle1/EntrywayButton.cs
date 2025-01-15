using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrywayButton : BasicButton
{
    [SerializeField] private Animator innerDoor;
    [SerializeField] private Animator outerDoor;
    [SerializeField] private float closeTime;
    public override void OnInteract(InteractModule interactModule)
    {
        base.OnInteract(interactModule);
        StartCoroutine(ConfirmPress());
        innerDoor.SetBool("Door", true);
        outerDoor.SetBool("Door", true);
        StartCoroutine(CloseAfter(closeTime));
    }
    IEnumerator CloseAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        innerDoor.SetBool("Door", false);
        outerDoor.SetBool("Door", false);
    }
}
