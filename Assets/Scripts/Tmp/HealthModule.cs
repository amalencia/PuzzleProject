using UnityEngine;
using UnityEngine.Events; 
using System;

namespace Tmp
{
    public class HealthModule : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        private int currentHealth;

        public UnityEvent<int> OnUnityEventHealthChanged;
        public UnityEvent OnCharacterDied;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;


        }
        // Update is called once per frame
        void Update()
        {

        }
        public void DeductHealth(int health)
        {
            currentHealth -= health;
            OnUnityEventHealthChanged.Invoke(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        private void Die()
        {
            if (gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;
                OnCharacterDied.Invoke();
            }
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }
    }
}