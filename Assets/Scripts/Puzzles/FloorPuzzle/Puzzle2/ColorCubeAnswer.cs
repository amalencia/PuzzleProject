
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class ColorCubeAnswer : MonoBehaviour
{
    [SerializeField] private int rows = 5, columns = 5;
    private int[][] inputs;
    [SerializeField] private ColorChangeCube[] cubes;
    public UnityEvent<bool> submitAnswer;

    private void Awake()
    {
        inputs = new int[rows][];
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i] = new int[columns];
            for (int j = 0; j < columns; j++)
            {
                inputs[i][j] = 0;
            }
        }
    }
    private void Start()
    {
        StartCoroutine(LateStart());
    }
    private IEnumerator LateStart()
    {
        yield return new WaitForSecondsRealtime(3);
        SubscribeToColorChange();
    }
    private void SubscribeToColorChange()
    {
        cubes = FindObjectsOfType<ColorChangeCube>();
        foreach (ColorChangeCube c in cubes)
        {
            c.OnColorChanged.AddListener(OnColorChanged);
        }
        FindObjectOfType<ResetBoxes>().ResetAllBoxes.Invoke();
    }
    private void OnColorChanged(int row, int col, int colorID)
    {
        inputs[row][col] = colorID;
        Debug.Log("Change received from row: " + row + " col: " + col + " ID: " + colorID);
    }
    internal bool Check()
    {
        int[][] cols = new int[columns][];
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i] = new int[rows];
            for (int j = 0; j < rows; j++)
            {
                cols[i][j] = inputs[j][i];
            }
        }
        for (int i = 0; i < inputs.Length; i++)
        {
            if (!inputs[i].Contains(1) || !inputs[i].Contains(2) || !inputs[i].Contains(3) || !inputs[i].Contains(5) || !inputs[i].Contains(5))
            {
                return false;
            }
            if (!cols[i].Contains(1) || !cols[i].Contains(2) || !cols[i].Contains(3) || !cols[i].Contains(5) || !cols[i].Contains(5))
            {
                return false;
            }
        }
        return true;    
    }
}
