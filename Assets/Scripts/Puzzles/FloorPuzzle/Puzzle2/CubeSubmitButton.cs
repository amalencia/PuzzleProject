using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CubeSubmitButton : SubmitButton
{
    [Header("Button Visuals")]

    [SerializeField] private ColorCubeAnswer cubeCheck;
    void Start()
    {
        if (cubeCheck == null)
        {
            cubeCheck = FindAnyObjectByType<ColorCubeAnswer>();
        }
    }
    public override void OnInteract(InteractModule interactModule)
    {
        TryUnlock.Invoke(cubeCheck.Check());
        StartCoroutine(Flash(cubeCheck.Check()));
    }
    IEnumerator Flash(bool answerCheck)
    {
        Material[] temp;
        if (answerCheck)
        {
            temp = canOpen;
        }
        else
        {
            temp = wrongAnswer;
        }
        if (temp != null)
        {
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

}

