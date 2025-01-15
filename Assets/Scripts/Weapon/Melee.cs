using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _attackAnimationName;

    public string TargetTag => _targetTag;
    public bool CanAttack => _canAttack;
    public int Damage => _damage;

    protected override void Start()
    {
        base.Start();
        _attackDuration = GetAnimationClipLength(_attackAnimationName);
    }
    protected override void AttackAction()
    {
        _animator.SetBool("IsAttacking", true);
        _animator.Play(_attackAnimationName, -1, 0f);
    }

    public override void BackToIdle()
    {
        _animator.SetBool("IsAttacking", false);
    }

    private float GetAnimationClipLength(string animationName)
    {
        // Get the AnimatorController from the Animator
        RuntimeAnimatorController ac = _animator.runtimeAnimatorController;

        // Iterate through all animation clips in the controller
        foreach (AnimationClip clip in ac.animationClips)
        {
            // Match the clip name with the provided animation name
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        // Return 0 if the animation clip was not found
        Debug.LogWarning($"Animation '{animationName}' not found!");
        return 0f;
    }
}
