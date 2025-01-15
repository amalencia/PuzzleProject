using System.Collections;
using UnityEngine;

public class ExitButton : BasicButton
{
    [Header("Button Visuals")]
    
    [SerializeField] private int wrongAnswerFlashes;
    [SerializeField] private float holdRed;
    [SerializeField] private float holdOriginal;
    [SerializeField] private Material red;
    [SerializeField] private Material green;

    private Material[] wrongAnswer;
    private Material[] canOpen;
    private bool unlocked = false;
    [Header("Door Control")]
    [SerializeField] private SubmitButton submitButton;
    [SerializeField] private Animator innerDoor;
    [SerializeField] private Animator outerDoor;
    [SerializeField] private float closeTime;
    private void Start()
    {
        if (submitButton == null)
        {
            submitButton = FindAnyObjectByType<SubmitButton>();
        }
        submitButton.TryUnlock.AddListener(Unlock);
        wrongAnswer = mesh.materials;
        wrongAnswer[1] = red;
        canOpen = mesh.materials;
        canOpen[1] = green;
    }

    private void Unlock(bool lockState)
    {
        unlocked = lockState;
        if (unlocked)
        {
            mesh.materials = canOpen;
            StartCoroutine(FlashGreen());  
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    public override void OnInteract(InteractModule interactModule)
    {
        base.OnInteract(interactModule);
        if (unlocked)
        {
            innerDoor.SetBool("Door", true);
            outerDoor.SetBool("Door", true);
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }
    IEnumerator FlashRed()
    {
        for (int i = 0; i < wrongAnswerFlashes; i++)
        {
            mesh.materials = wrongAnswer;
            yield return new WaitForSeconds(holdRed);
            mesh.materials = originalMaterial;
            yield return new WaitForSeconds(holdOriginal);
        }
        mesh.materials = originalMaterial;
    }
    IEnumerator FlashGreen()
    {
        while (unlocked)
        {
            mesh.materials = canOpen;
            yield return new WaitForSeconds(holdRed);
            mesh.materials = originalMaterial;
            yield return new WaitForSeconds(holdOriginal);
        }
    }
}
