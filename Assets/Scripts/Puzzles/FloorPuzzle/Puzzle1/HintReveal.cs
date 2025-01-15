using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintReveal : MonoBehaviour
{
    [SerializeField] private GameObject instructions;
    private int i = 0;

    private void Start()
    {
        instructions.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!instructions.activeSelf)
            {
                instructions.SetActive(true);
            }
            else if (i < 4)
            {
                instructions.transform.Rotate(new Vector3(45, 0, 0));
                i++;
            }
        }
    }

}

