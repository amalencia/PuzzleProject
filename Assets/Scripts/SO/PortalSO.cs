using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SOs/Center Room Portal", fileName = "CenterRoomPortal")]
public class PortalSO : ScriptableObject
{
    [SerializeField] private bool _isActive;
    [SerializeField] private string _sceneName;

    public UnityAction<string> OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(_sceneName);
        }
    }

    public void SetNotActive()
    {
        _isActive = false;
    }

    public void SetActive()
    {
        _isActive = true;
    }

    public bool IsActive()
    {
        return _isActive;
    }
}
