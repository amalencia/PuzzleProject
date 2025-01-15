using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class DoorButton : MonoBehaviour, IInteracterable 
{
    [SerializeField] private UnityEvent OnInteracted;
    [SerializeField] private AudioSource pressedButton;
    [SerializeField] private Material highlightedMaterial;

    private MeshRenderer mesh; 
    private Material originalMaterial; 

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        if (mesh.materials.Length > 1)
        {
            originalMaterial = mesh.materials[1];
        }
    }
    public void OnHoverEnter()
    {
        if (mesh.materials.Length > 1)
        {
            Material[] materials = mesh.materials;
            materials[1] = highlightedMaterial;
            mesh.materials = materials;
        }
        Debug.Log("Why are you looking at me?"); 
    }

    public void OnHoverExit()
    {
        if(mesh.materials.Length > 1)
        {
            Material[] materials = mesh.materials;
            materials[1] = originalMaterial; 
            mesh.materials = materials;
        } 
        Debug.Log("Please come back! I want you to look at me! "); 
    }

    public void OnInteract(InteractModule interactModule)
    {
        OnInteracted.Invoke();
        pressedButton.Play();
        Debug.Log("Are you gonna hit on me?"); 
    }
}
