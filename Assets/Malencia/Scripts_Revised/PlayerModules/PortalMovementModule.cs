using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMovementModule : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform headUpDown;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform _player;
    private Vector3 moveDirection;
    private Vector2 aimDirection;
    private bool gunStopper;
    private Quaternion baseRotation;
    [SerializeField] private Quaternion baseRotationQuaternion;
    private float timer;
    private float resetTime;

    public void GunIsTouching(bool  isTouching)
    {
        gunStopper = isTouching;
    }

    private void Start()
    {
        baseRotation = transform.rotation;
        baseRotationQuaternion = transform.rotation;
        resetTime = .5f;
        timer = 0;
        
    }

    public void MoveCharacter(Vector3 direction)
    {
        if (gunStopper)
        {
            moveDirection.x = direction.x;
            controller.Move(((transform.right * moveDirection.x) + -transform.forward*2) * Time.deltaTime * movementSpeed);
            return;
        }

        moveDirection.x = direction.x;
        moveDirection.z = direction.z;

        float tempMultiplier = 1;
        controller.Move(((transform.right * moveDirection.x) + transform.forward * moveDirection.z) * Time.deltaTime * movementSpeed * tempMultiplier);
    }

    public void RotateCharacter(Vector3 direction)
    {
        aimDirection.x = direction.x * Time.deltaTime;
        aimDirection.y += direction.y * Time.deltaTime;

        aimDirection.y = Mathf.Clamp(aimDirection.y, -85f, 85f);

        if (headUpDown) headUpDown.localEulerAngles = new Vector3(aimDirection.y, 0, 0);
        transform.Rotate(Vector3.up, aimDirection.x);
    }

    public void ResetRotation()
    {
        baseRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
    }

    private void Update()
    {
       

        if (Mathf.Abs(_player.transform.rotation.eulerAngles.x) > 170f || (Mathf.Abs(_player.transform.rotation.eulerAngles.z) > 170))
        {
           
            timer += Time.deltaTime;
            if (timer > resetTime)
            {
               
                _player.transform.rotation = baseRotationQuaternion;
                timer = 0;
            }
        }
    }

}
