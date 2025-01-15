using UnityEngine;
using UnityEngine.Events; 
using System; 

public class HealthModule : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    
    public UnityEvent<int> OnUnityEventHealthChanged;
    public UnityEvent OnPlayerDeath; //New event for player death

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void DeductHealth(int health)
    {
        currentHealth -= health;
        OnUnityEventHealthChanged.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Die(); 
        }
        Debug.Log("I lost health");
    }
    private void Die()
    {   
        OnPlayerDeath.Invoke();//Notify the checkpoint manager 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Resethealth();
        Debug.Log("Player has died");
    }
    public void Resethealth()
    {
        currentHealth = maxHealth;
        OnUnityEventHealthChanged.Invoke(currentHealth); 
    }
}
