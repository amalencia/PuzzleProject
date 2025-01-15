using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public abstract class  BasicButton : MonoBehaviour , IInteracterable
{
    [SerializeField] private UnityEvent OnInteracted;
    [SerializeField] private Material blueHighlight;
    [SerializeField] private Material greenHighlight;

    protected MeshRenderer mesh;
    protected Material[] originalMaterial;
    protected Material[] highlightedMaterials;
    protected Material[] buttonPressConfirm;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        originalMaterial = mesh.materials;
      
        highlightedMaterials = mesh.materials;
        highlightedMaterials[1] = blueHighlight;

        buttonPressConfirm = mesh.materials;
        buttonPressConfirm[1] = greenHighlight;
    }
    public void OnHoverEnter()
    {
        mesh.materials = highlightedMaterials;
      
    }

    public void OnHoverExit()
    {
        mesh.materials = originalMaterial;
      
    }

    public virtual void OnInteract(InteractModule interactModule)
    {
        OnInteracted.Invoke();
        
    }
    protected IEnumerator ConfirmPress()
    {
        for (int i = 0; i < 3; i++)
        {
            mesh.materials = buttonPressConfirm;
            yield return new WaitForSeconds(0.25f);
            mesh.materials = originalMaterial;
            yield return new WaitForSeconds(0.25f);
        }
        mesh.materials = originalMaterial;
    }
}
