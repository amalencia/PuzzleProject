using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tmp
{
    public class UIManager : MonoBehaviour
    {
        [Header("Health UI")]
        [SerializeField] private HealthModule _playerHealthModule;
        [SerializeField] private HealthBar _healthBar;

        private void Start()
        {
            _playerHealthModule.OnUnityEventHealthChanged.AddListener(UpdateHealthUI);
            ResetHealthUI();
        }

        public void ResetHealthUI()
        {
            UpdateHealthUI(_playerHealthModule.GetMaxHealth());
        }

        private void UpdateHealthUI(int health)
        {
            //_healthText.text = "Health : " + health.ToString();
            _healthBar.SetHealthBar(health);
        }
    }
}