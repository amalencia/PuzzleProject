using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _delayTime;

    public void LightOn()
    {
        Invoke("DelayedLightOn", _delayTime);
    }

    public void LightOff()
    {
        Invoke("DelayedLightOff", _delayTime);
    }

    private void DelayedLightOn()
    {
        _animator.SetBool("isConnectionComplete", true);
    }

    private void DelayedLightOff()
    {
        _animator.SetBool("isConnectionComplete", false);
    }
}
