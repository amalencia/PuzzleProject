using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalJumpModule : MonoBehaviour
{
    public const float _earthGravity = -9.81f;
    private Vector3 _velocity;

    [SerializeField] private CharacterController controller;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask floorLayer;

    public void Jump()
    {
        if (IsGrounded() && controller)
        {
            _velocity.y = jumpForce;
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, .2f, floorLayer);
    }

    private void ApplyGravity()
    {
        if (!IsGrounded())
        {
            _velocity.y += _earthGravity * Time.deltaTime;
            if (_velocity.y < -10f)
            {
                _velocity.y = -10f;
            }
        }
        else if (_velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        controller.Move(_velocity * Time.deltaTime);
    }

    private void Update()
    {
        ApplyGravity();
    }
}
