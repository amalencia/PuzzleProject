using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tmp
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private HealthModule _healthModule;

        private float _maxValue;

        private void Start()
        {
            _maxValue = _healthModule.GetMaxHealth();
            _healthModule.OnUnityEventHealthChanged.AddListener(SetHealthBar);
        }

        public void SetHealthBar(int health)
        {
            _slider.value = health / _maxValue;
            //Debug.Log(_slider.value.ToString() + " " + health.ToString());
        }
    }
}