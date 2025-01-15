using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SOs/Game Settings", fileName = "GameSettings")]
public class GameSettingsSO : ScriptableObject
{
    public bool isInitialized = false;

    private void OnEnable()
    {
        isInitialized = false;
    }

    private void OnDisable()
    {
        isInitialized = false;
    }
}
