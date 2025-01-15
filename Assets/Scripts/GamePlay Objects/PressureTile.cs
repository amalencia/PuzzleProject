using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PressureTile : MonoBehaviour
{
    [SerializeField] private UnityEvent OnActivation;
    [SerializeField] private UnityEvent OnDeactivation;
    [SerializeField] private Rigidbody correctRigidbody;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == correctRigidbody)
        {
            OnActivation.Invoke();
        }
    }

    public void OnPressurePlateDown()
    {
        animator.SetBool("IsPressedOn", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == correctRigidbody)
        {
            OnDeactivation.Invoke();
        }
    }

    public void OnPressureRelease()
    {
        animator.SetBool("IsPressedOn", false);
    }
}
