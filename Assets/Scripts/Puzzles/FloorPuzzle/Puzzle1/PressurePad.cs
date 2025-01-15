using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{   public int row , col ; 
    [SerializeField] protected float pressedDownYPosition;
    [SerializeField] protected Material[] buttonPressed;
    [SerializeField] protected MeshRenderer mesh;
    public bool state = false;
    public UnityEvent<bool, int, int> ButtonPressed;
    private Material[] buttonUp;
    [SerializeField] protected CountdownTimer timer;
    [SerializeField] protected ResetButton resetButton;
    protected Vector3 originalPosition;
    protected Vector3 downPosition;

    protected void Awake()
    {
        if (mesh == null)
        {
            mesh = GetComponent<MeshRenderer>();
        }
        buttonUp = mesh.materials;

        if (timer == null)
        {
            timer = FindAnyObjectByType<CountdownTimer>();
        }
        timer.OnTimerReset.AddListener(ResetPad);
        if (resetButton == null)
        {
            resetButton = FindAnyObjectByType<ResetButton>();
        }
        resetButton.ResetAllPads.AddListener(ResetPad);
        originalPosition = transform.position;
        downPosition = transform.position -= new Vector3(0, pressedDownYPosition, 0);
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && state == false)
        {
            transform.position = downPosition;
            mesh.materials = buttonPressed;
            state = true;
            ButtonPressed.Invoke(state, row, col);
        }
    }
    
    protected void ResetPad()
    {
        state = false;
        mesh.materials = buttonUp;
        ButtonPressed.Invoke(state, row, col);
        transform.position = originalPosition;
    }
}
