using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class SubmitButton : BasicButton
{
    [Header("Button Visuals")]

    [SerializeField] protected int numberOfFlashes;
    [SerializeField] protected float holdRed;
    [SerializeField] protected float holdOriginal;
    [SerializeField] protected Material red;
    [SerializeField] protected Material green;

    protected Material[] wrongAnswer;
    protected Material[] canOpen;
    public UnityEvent<bool> TryUnlock;
    [SerializeField] private FloorPuzzleAnswer checkAnswer;
    [SerializeField] private CountdownTimer timer;
    void Start()
    {
        if (checkAnswer == null)
        {
            checkAnswer = FindAnyObjectByType<FloorPuzzleAnswer>();
        }
        if (timer == null)
        {
            timer = FindAnyObjectByType<CountdownTimer>();
        }
        wrongAnswer = mesh.materials;
        wrongAnswer[1] = red;
        Debug.Log(wrongAnswer.Count());
        canOpen = mesh.materials;
        canOpen[1] = green;
        Debug.Log(canOpen.Count());
    }
    public override void OnInteract(InteractModule interactModule)
    {
        base.OnInteract(interactModule);
        TryUnlock.Invoke(checkAnswer.CheckAnswer());
        StartCoroutine(Flash(checkAnswer.CheckAnswer()));

    }
    IEnumerator Flash(bool answerCheck)
    {
        Material[] temp;
        if (answerCheck)
        {
            temp = canOpen;
            timer.StopTimer();
        }
        else
        {
            temp = wrongAnswer;
        }
        for (int i = 0; i < numberOfFlashes; i++)
        {
            mesh.materials = temp;
            yield return new WaitForSeconds(holdRed);
            mesh.materials = originalMaterial;
            yield return new WaitForSeconds(holdOriginal);
        }
        mesh.materials = originalMaterial;
    }

}
