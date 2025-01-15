using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject DeathScreen;

    [SerializeField] private HealthModule healthModule;
    [SerializeField] private CheckpointManager checkpointManager; 


    private void Start()
    {
        healthModule.OnPlayerDeath.AddListener(ShowDeathScreen); 
    }
    private void ShowDeathScreen()
    {
        DeathScreen.SetActive(true);
        Time.timeScale = 0f; //Pause the game 

    }

    private void HideDeathScreen()
    {
        DeathScreen.SetActive(false);
        Time.timeScale = 1f; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Respawn()
    {
        HideDeathScreen();
        healthModule.Resethealth();
        checkpointManager.RespawnPlayer();
        Debug.Log("Player has respawned and the game is resumed."); 
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0); 
    }
}
