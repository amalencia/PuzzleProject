using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorKeyButton : MonoBehaviour
{
    [SerializeField] private UnityEvent OnKeyCollected;
    [SerializeField] private Material KeyColorMaterial;

    private MeshRenderer mesh;
    private Material originalMaterial;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        originalMaterial = mesh.material; 
    }

    public void OnKeyPickUp()
    {
        OnKeyCollected.Invoke(); 
        ChangeButtonColorToKeyChange();
    }

    private void ChangeButtonColorToKeyChange()
    {
        if(KeyColorMaterial != null)
        {
            mesh.material = KeyColorMaterial;
        }
        else
        {
            Debug.LogWarning("KeyColorMaterial is not assigned."); 
        }
    }
}
