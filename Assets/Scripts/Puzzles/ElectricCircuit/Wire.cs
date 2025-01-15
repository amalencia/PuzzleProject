using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PowerUp()
    {
        _animator.SetBool("isConnectionComplete", true);
    }

    public void PowerDown()
    {
        _animator.SetBool("isConnectionComplete", false);
    }
}
