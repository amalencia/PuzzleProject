using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpModule : MonoBehaviour
{
    //We referencing the controller on the gameobject player 
    [SerializeField] CharacterController controller;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private float earthJumpForce = 5f;
    [SerializeField] private float moonJumpForce;
    [SerializeField] private float moonGravityDuration;
    [SerializeField] private float increasedMoonJumpForce; 

    private float JumpForce;
    private Vector3 velocity;
    public const float gravityAcceleration = -9.81f;
    public const float moonGravityAcceleration = -1.625f;    
    private bool useMoonGravity = false;

    // Start is called before the first frame update
    void Start()
    {
        JumpForce = earthJumpForce; 
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        Jump();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(MoonAbility(true)); 
        }
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            velocity.y = JumpForce;
        }
    }
    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.25f, floorLayer);
    }
    private void ApplyGravity()
    {
        float gravity = useMoonGravity ? moonGravityAcceleration : gravityAcceleration;
        if (!IsGrounded())
        {
            //gravity use to be gravityAcceleration 
            velocity.y += gravity * Time.deltaTime;
            if (velocity.y < -9f)
            {
                velocity.y = -9f;
            }
        }
        else if (velocity.y < 0)
        {
            velocity.y = 0;
        }

        
        controller.Move(velocity * Time.deltaTime);
    }
    //The gravity ability is here so if you need to change anything it would be here. So when making modules, cut everything here and below. 
    private void MoonApplyGravity(bool isMoonGravity)
    {
        useMoonGravity = isMoonGravity;
        JumpForce = isMoonGravity ? moonJumpForce : earthJumpForce;
    }
    //This boolen was ruining the code and now the code is feeling a lot better 
    public IEnumerator MoonAbility(bool isMoonGravity)
    {
        useMoonGravity = isMoonGravity;
        JumpForce = isMoonGravity ? moonJumpForce : earthJumpForce; 
        yield return new WaitForSeconds(moonGravityDuration);
        useMoonGravity = false;
        JumpForce = earthJumpForce; 
    }

    private void OnTriggerEnter(Collider other)
    {
        //Use tags whenever you want to shift the gravity of the player
        if(other.CompareTag("MoonRoom"))
        {
            MoonApplyGravity(true);
            JumpForce = increasedMoonJumpForce;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("MoonRoom"))
        {
            MoonApplyGravity(false);
        }
    }
}
