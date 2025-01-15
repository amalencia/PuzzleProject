using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CompanionCube : MonoBehaviour, IPortalInteractable
{
    private ParticleSystem _particleSystem;
    [SerializeField] private UnityEvent OnInteracted;
    [SerializeField] private TextMeshProUGUI _completionText; 



    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    void IPortalInteractable.OnHoverEnter()
    {
        _particleSystem.Play();

    }

    void IPortalInteractable.OnHoverExit()
    {
        _particleSystem.Stop();
    }

    void IPortalInteractable.OnInteract(PortalInteractModule interactModule)
    {
        OnInteracted.Invoke();
    }
}
