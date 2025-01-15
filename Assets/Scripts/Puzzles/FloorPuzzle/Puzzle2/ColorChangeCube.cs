using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorChangeCube : MonoBehaviour, IInteracterable
{
    [SerializeField] internal int row, col;  
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material purple;
    [SerializeField] private Material red;
    [SerializeField] private Material green;
    [SerializeField] private Material blue;
    [SerializeField] private Material white;
    [SerializeField] private Material interactHighlight;
    [SerializeField] private ResetBoxes reset;
    [SerializeField] private int currentColor = 0;

    public UnityEvent<int, int, int> OnColorChanged;
    
    private Material starting;
    private Material highlight;

    private void Awake()
    {
      if (mesh == null)
        {
            mesh = GetComponentInChildren<MeshRenderer>();
        }
       
    }
    private void Start()
    {
        if (reset == null)
        {
            reset = FindAnyObjectByType<ResetBoxes>();
        }
        reset.ResetAllBoxes.AddListener(ResetColor);
        List<Material> materials = new();
        mesh.GetSharedMaterials(materials);
        starting = materials[0];
        currentColor = IDCurrentMaterial(materials[0]);
        highlight = materials[1];
    }
    private void ChangeColor(int ID)
    {
        Material temp = IDToMaterial(ID);    
        mesh.materials = new Material[] { temp, mesh.materials[1] };
        currentColor = ID;
        OnColorChanged.Invoke(row, col, currentColor);
    }
    private int IDCurrentMaterial(Material current)
    {
        if (current.Equals(purple)) return 1;
        if (current.Equals(red)) return 2;
        if (current.Equals(green)) return 3;
        if (current.Equals(blue)) return 4;
        if (current.Equals(white)) return 5;
        else return 0;
    }
    private Material IDToMaterial(int ID)
    {
        if (ID == 0) return null;
        if (ID == 1) return purple;
        if (ID == 2) return red;
        if (ID == 3) return green;
        if (ID == 4) return blue;
        if (ID == 5) return white;
        else return null;   
    }

    private void ResetColor()
    {
        ChangeColor(IDCurrentMaterial(starting));
    }
    public void OnHoverEnter()
    {
        
        mesh.materials = new Material[] { IDToMaterial(currentColor), interactHighlight };
    }

    public void OnHoverExit()
    {
        mesh.materials = new Material[] { IDToMaterial(currentColor), highlight };
    }

    public void OnInteract(InteractModule interactModule)
    {
        currentColor++;
        if (currentColor > 5)
        {
            currentColor = 1;
        }
        ChangeColor(currentColor);
    }
}
