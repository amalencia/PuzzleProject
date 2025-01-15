using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cooldown
{
    private float _cooldownTime;
    private float _nextFireTime;

    public Cooldown(float cooldownTime)
    {
        _cooldownTime = cooldownTime;
    }

    public bool IsCoolingDown => Time.time < _nextFireTime;
    public void StartCoolingDown() => _nextFireTime = Time.time + _cooldownTime;
}
