using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] private GameObject _electricityVFX;
    [SerializeField] private AudioSource _electricityLeakageAudio;

    private void Start()
    {
        _electricityLeakageAudio.Play();
    }

    public void ElectricityLeakageOn()
    {
        _electricityVFX.SetActive(true);
        _electricityLeakageAudio.Play();
    }

    public void ElectricityLeakageOff()
    {
        _electricityVFX.SetActive(false);
        _electricityLeakageAudio.Stop();
    }
}
