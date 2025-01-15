using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : MonoBehaviour
{
    [SerializeField] private Battery _battery;
    [SerializeField] private LightBulb _lightBulb;
    [SerializeField] private Wire[] _wires;

    public void OnCircuitComplete()
    {
        _battery.ElectricityLeakageOff();
        foreach (var wire in _wires)
        {
            wire.PowerUp();
        }
        _lightBulb.LightOn();
    }

    public void OnCircuitBroken()
    {
        _battery.ElectricityLeakageOn();
        foreach (var wire in _wires)
        {
            wire.PowerDown();
        }
        _lightBulb.LightOff();
    }
}
