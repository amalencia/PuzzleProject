using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour, IInteracterable
{
    [SerializeField] private Rigidbody _rb; 
    private Transform point; 

    public void OnHoverEnter()
    {
        
    }

    public void OnHoverExit()
    {
        
    }

    public void OnInteract(InteractModule interactModule)
    {
        if(transform.parent == null)
        {
            //We are going to pick up the cube 

            _rb.useGravity = false;
            _rb.isKinematic = true;
            point = interactModule.GetHoldTransform();
            transform.position = point.position;
            transform.rotation = point.rotation;

            transform.SetParent(point); 
        }
        else
        {
            Drop();
        }
    }

    public void Drop()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;
        transform.SetParent(null);
    }
}
