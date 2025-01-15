
using UnityEngine;


public class GridRow : MonoBehaviour
{
    [SerializeField] private int rowNumber;
    [SerializeField] private bool[] states;
    public PressurePad[] pads;

    private void Awake()
    {
        pads = GetComponentsInChildren<PressurePad>();
        AssignRowAndCol();
    }
    private void CheckPositions()
    {
        // GetComponents not guaranteed to return elements in order
        // Use a bubble sort to ensure each pad is assigned the correct col number
        for (int secondIndex = 0; secondIndex < pads.Length - 1; secondIndex++)
        {
            for (int index = 0; index < pads.Length - 1 - secondIndex; index++)
            {
                if (pads[index].gameObject.transform.position.y > pads[index + 1].gameObject.transform.position.y)
                {
                    PressurePad temp = pads[index];
                    pads[index] = pads[index + 1];
                    pads[index].col = pads[index + 1].col;   
                    pads[index + 1] = temp;
                    pads[index + 1].col = temp.col;
                }

            }
        }
    }
   private void AssignRowAndCol()
    {
        for (int i = 0; i < pads.Length; i++)
        {
            pads[i].row = rowNumber;
            pads[i].col = i;
        }
        CheckPositions();
    }
}
