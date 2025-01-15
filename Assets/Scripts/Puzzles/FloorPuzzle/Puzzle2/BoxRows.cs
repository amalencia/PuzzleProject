using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRows : MonoBehaviour
{
   
        [SerializeField] private int rowNumber;
        [SerializeField] private int[] states;
        public ColorChangeCube[] boxes;

        private void Awake()
        {
            boxes = GetComponentsInChildren<ColorChangeCube>();
            AssignRowAndCol();
        }
      
        private void AssignRowAndCol()
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].row = rowNumber;
                boxes[i].col = i;
            }
        }
    }
