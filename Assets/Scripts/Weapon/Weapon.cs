using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform _holdPoint;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _cooldownTime;

    protected float _attackDuration;

    protected Cooldown _cooldown;

    protected string _targetTag;

    protected bool _canAttack;

    public UnityAction ActOnAttackHappened;

    protected virtual void Start()
    {
        _cooldown = new Cooldown(_cooldownTime);
    }

    public void SetUp(string tag)
    {
        _targetTag = tag;
    }

    public void EnableAttack()
    {
        _canAttack = true;
    }

    public virtual void DisableAttack()
    {
        _canAttack = false;
    }

    public virtual void AttachToCharacter(Transform holdPoint)
    {
        //Debug.Log(transform.position - _holdPoint.position);
        transform.SetParent(holdPoint);
        transform.localPosition = transform.position - _holdPoint.position;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void DetachFromCharacter()
    {
        transform.parent = null;
    }

    public void Attack()
    {
        if (_cooldown.IsCoolingDown)
        {
            return;
        }
        EnableAttack();
        AttackAction();
        ActOnAttackHappened.Invoke();
        Invoke("DisableAttack", _attackDuration);
        _cooldown.StartCoolingDown();
    }

    protected abstract void AttackAction();
    public abstract void BackToIdle();
}
