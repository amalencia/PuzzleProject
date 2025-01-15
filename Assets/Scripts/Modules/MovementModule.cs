using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementModule : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    //The speed is for the controller movement 
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform headUpDown; 

    //This Vector2 is only for inputs for A, and D
    private Vector3 moveDirection;
    //The mouseSense is for how fast you can move across the screen 
    private Vector2 aimDirection;
    private bool canLookWithMouse;

    public void MovePlayer(Vector3 direction)
    {
        moveDirection.x = direction.x;
        moveDirection.z = direction.z;

        float temMultiplier = 1;

        //This was previous, Controller.Move(((transform.right * moveDirection.x) + transform.forward * moveDirection.z) * Time.deltaTime * moveSpeed * temMultiplier);
        controller.Move(((transform.right * moveDirection.x) + transform.forward * moveDirection.z) * Time.deltaTime * moveSpeed * temMultiplier);
    }
    public void RotatePlayer(Vector3 direction)
    {
        aimDirection.x = direction.x;
        aimDirection.y += direction.y * Time.deltaTime;

        aimDirection.y = Mathf.Clamp(aimDirection.y, -85f, 85f);

        if (headUpDown) headUpDown.localEulerAngles = new Vector3(aimDirection.y, 0, 0); 

        //To move the camera on it's left or right (y axis)  
        transform.Rotate(Vector3.up, aimDirection.x * Time.deltaTime);
    }
}
