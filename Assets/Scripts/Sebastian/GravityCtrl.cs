using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCtrl : MonoBehaviour
{
    public GravityOrbit Gravity;
    private Rigidbody playerController;

    public float RotationSpeed = 20;
    public float gravityScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<Rigidbody>(); 
    }

    //Urgent - Revist the video 
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Gravity)
        {
            Vector3 gravityUp = Vector3.zero; 

            if(Gravity.FixedDirection)
            {
                gravityUp = -Gravity.transform.forward; 
            }
            else
            {
                gravityUp = (transform.position - Gravity.transform.forward).normalized;
            }

            Vector3 localUp = transform.forward;

            Quaternion targetrotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation; 

            transform.forward = Vector3.Lerp(transform.forward, gravityUp, RotationSpeed * Time.deltaTime);

            playerController.AddForce((-gravityUp * Gravity.Gravity) * playerController.mass); 
        }
    }
}
