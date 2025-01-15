using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePickable : MonoBehaviour, IInteracterable
{
    [SerializeField] private Rigidbody _rb;
    private Transform point;
    [SerializeField] private bool pickedUp;
    private Material originalColour;
    private Material baseMaterial;
    [SerializeField] private Material highlightedColour;
    [SerializeField] private CubeSpawner spawner;
    private void Start()
    {
        Material[] materials = GetComponentInChildren<MeshRenderer>().materials;
        baseMaterial = materials[0];
        originalColour = materials[1];
        if (highlightedColour == null)
        {
            highlightedColour.SetColor("highlight", Color.cyan);
        }
    }
    public void OnHoverEnter()
    {
        GetComponentInChildren<MeshRenderer>().materials = new Material[] { baseMaterial, highlightedColour };

    }

    public void OnHoverExit()
    {
        GetComponentInChildren<MeshRenderer>().materials = new Material[] { baseMaterial, originalColour };
    }

    public void OnInteract(InteractModule interactModule)
    {
        if (transform.parent == null || transform.parent == spawner)
        {
            //We are going to pick up the cube 

            _rb.useGravity = false;
            _rb.isKinematic = true;
            point = interactModule.GetHoldTransform();
            transform.position = point.position;
            transform.rotation = point.rotation;

            transform.SetParent(point);
            pickedUp = true;
        }
    }

    private void Drop()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;
        pickedUp = false;
        transform.SetParent(null);
    }
    void Update()
    {
        if (pickedUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Drop();
            }
        }
    }

    public void LinkToPool(CubeSpawner owner)
    {
        spawner = owner;
    }
}

