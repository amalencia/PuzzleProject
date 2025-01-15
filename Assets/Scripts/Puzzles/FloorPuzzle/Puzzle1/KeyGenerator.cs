using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class KeyGenerator : MonoBehaviour
{
    private bool[][] answerKey;
    public UnityEvent<bool[][]> NewKeyAvailable;
    [SerializeField] private CountdownTimer timer;
    [SerializeField] private int minTruePanels;
    [SerializeField] private int maxTruePanels;
    
    void Awake()
    {
        if (timer == null)
        {
            timer = FindAnyObjectByType<CountdownTimer>();
        }
        timer.OnTimerReset.AddListener(GenerateRandomKey);
        CreateBlankKey();
    }

    private void CreateBlankKey()
    {
        answerKey = new bool[5][];
        for (int i = 0; i < 5; i++)
        {
            answerKey[i] = new bool[5];
            for (int j = 0; j < 5; j++)
            {
                answerKey[i][j] = new bool();
            }
        }
    }
    private void SetKeyToFalse()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                answerKey[i][j] = false;
            }
        }
    }
    private void GenerateRandomKey()
    {
        SetKeyToFalse();
        int numberOfPanels = Random.Range(minTruePanels, maxTruePanels);
        int[] truePanels = new int[numberOfPanels];
        for (int i = 0; i < truePanels.Length; i++)
        {
            truePanels[i] = Random.Range(0, 24);
            int row = Mathf.FloorToInt(truePanels[i]/5);
            int col = truePanels[i] % 5;
            answerKey[row][col] = true;
        }
        NewKeyAvailable.Invoke(answerKey);
    }
   public bool[][] GetAnswerKey()
    {
        return answerKey;
    }
}
