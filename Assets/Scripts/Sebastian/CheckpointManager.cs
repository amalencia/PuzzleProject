using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject player;
    private Vector3 currentCheckpoint;
    HealthModule healthModule;

    private void Start()
    {
        currentCheckpoint = player.transform.position; //Initial checkpoint 
        Debug.Log(currentCheckpoint.ToString());    
        healthModule = player.GetComponent<HealthModule>();

        if(healthModule != null )
        {
            //healthModule.OnPlayerDeath.AddListener(RespawnPlayer); 
        }
    }

    public void SetCheckPoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
        Debug.Log("New checkpoint set at: " + newCheckpoint); 
    }

    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint;
        Debug.Log(player.transform.position.ToString());
        Debug.Log("Player has respawned at checkpoint"); 
    }
}
