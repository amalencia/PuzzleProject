using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorPuzzleAnswer : MonoBehaviour
{
    [SerializeField] KeyGenerator keyGenerator;
    private int rows = 5 , columns = 5;
    private bool[][] inputTable;
    private bool[][] answerKey;
    [SerializeField] private GridRow[] gridRows;
    public UnityEvent<bool> submitAnswer;
  

    private void Awake()
    {
        inputTable = new bool[rows][];
        answerKey = new bool[rows][];
        if (gridRows == null)
        {
            gridRows = FindObjectsOfType<GridRow>();
        }
        if (keyGenerator == null )
        {
            keyGenerator = FindAnyObjectByType<KeyGenerator>();
        }
    }
    private void Start()
    {
        for (int i = 0; i < rows; i++) 
        {
            inputTable[i] = new bool[columns];
            answerKey[i] = new bool[columns];
        }
        SubscribeToButtons();
        keyGenerator.NewKeyAvailable.AddListener(UpdateAnswerKey);
    }
    private void SubscribeToButtons()
    {
        foreach (GridRow row in gridRows) 
        {
            foreach(PressurePad pad in row.pads)
            {
                Debug.Log("Answer key subscribed to pad at row: " + pad.row + " col: " +  pad.col); 
                pad.ButtonPressed.AddListener(UpdateInputTable);
            }
        }
    }
  
    private void UpdateAnswerKey(bool[][] newKey)
    {
        answerKey = newKey;
    }
    
    public bool CheckAnswer()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (inputTable[i][j] != answerKey[i][j])
                {
                    Debug.Log("Answer wrong at row: " + i + " col: " + j);
                    return false;
                    
                }
            }
        }
        return true;
    }
    public void UpdateInputTable(bool pressed, int row, int col)
    {
        inputTable[row][col] = pressed;
        Debug.Log("Input Table Updated at row: " + row + " col: " + col + " to " + pressed);
    }
}
