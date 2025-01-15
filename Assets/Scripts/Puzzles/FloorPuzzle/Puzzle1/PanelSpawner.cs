using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelSpawner : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private GameObject original;
    [SerializeField] private float width;
    [SerializeField] private KeyGenerator keyGenerator;
    [SerializeField] private Material falseMaterial;
    private Material trueMaterial;
    private GameObject[][] panels = new GameObject[5][];

    private void Awake()
    {
        if (original == null)
        {
            meshFilter = GetComponentInChildren<MeshFilter>();
        }
        if (keyGenerator == null)
        {
            keyGenerator = FindAnyObjectByType<KeyGenerator>();
        }
        for (int i = 0; i < 5; i++)
        {
            panels[i] = new GameObject[5];
        }

        trueMaterial = original.GetComponent<MeshRenderer>().material;
        Bounds bounds = meshFilter.sharedMesh.bounds;
        width = bounds.size.x * 2.1f * original.transform.localScale.x;

    }
    // Start is called before the first frame update
    private void Start()
    {
        SpawnPanels();
        keyGenerator.NewKeyAvailable.AddListener(SetPanels);
    }

    private void SpawnPanels()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject temp = Instantiate(original, transform);
                panels[i][j] = temp;
                temp.transform.position += j * width * transform.right;
                temp.transform.position += i * width * -transform.up;
            }
        }
        original.SetActive(false);
    }
    private void SetPanels(bool[][] answerKey)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (answerKey[i][j] == true)
                {
                    panels[i][j].GetComponent<MeshRenderer>().material = trueMaterial;
                }
                else
                {
                    panels[i][j].GetComponent <MeshRenderer>().material = falseMaterial;
                }

            }
        }
    }

}
