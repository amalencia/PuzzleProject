using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CarModule : MonoBehaviour
{
    [SerializeField] WheelCollider frontLeft, frontRight, backLeft, backRight;
 
    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;
    public float flipForce = 10f;
    public float flipDuration = 1f; 

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;
    private bool isFlipping = false; 

    private void FixedUpdate()
    {
        if (isFlipping) return; 

        //Get forward/reverse acceleration fromt the vertical axis (W and S keys) 
        currentAcceleration = acceleration * -Input.GetAxis("Vertical"); 

        //If we're pressing space, give currentBreakForce a value 
        if(Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingForce; 
        }
        else
        {
            currentBreakForce = 0f; 
        }

        //Applying acceleration to front wheels 
        frontLeft.motorTorque = currentAcceleration;
        frontRight.motorTorque = currentAcceleration;

        frontLeft.brakeTorque = currentBreakForce;
        frontRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;

        //Take care of the steering. 
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        CheckandFlipCar(); 
    }

    private void CheckandFlipCar()
    {
        if(transform.up.y < 0)
        {
            StartCoroutine(FlipCar());  
        }
    }

    private IEnumerator FlipCar()
    {
        isFlipping = true; 

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * flipForce, ForceMode.Impulse);

        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(180f, 0f, 0f); 

        while(elapsedTime < flipDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / flipDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        isFlipping = false; 
    }

    private void DestroyCar()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject); 
        }
    }
}
