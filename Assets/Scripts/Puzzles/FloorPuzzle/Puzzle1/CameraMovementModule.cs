using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovementModule : MonoBehaviour
{
    public UnityEvent OnExitMicroscope;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float maxUp;
    [SerializeField] private float maxDown;
    [SerializeField] private float maxLeft;
    [SerializeField] private float maxRight;
    private Vector2 aimDirection;
    private bool canLook = false;

    private void Awake()
    {
        maxUp = 360 - maxUp;
        maxLeft = 360 - maxLeft;
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.localRotation = Quaternion.identity;
        
    }

    private void Update()
    {
        if (canLook)
        {
            aimDirection = Vector2.zero;
            aimDirection.x = Input.GetAxisRaw("Mouse X") * lookSpeed * Time.deltaTime;
            aimDirection.y = -Input.GetAxisRaw("Mouse Y") * lookSpeed * Time.deltaTime;
            RotateCamera();
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                OnExitMicroscope.Invoke();
            }
        }
    }

    private void RotateCamera()
    {
        transform.Rotate(Vector3.forward * aimDirection.y);

        //To move the camera on it's left or right (y axis)  
        transform.Rotate(Vector3.up, aimDirection.x);
        if (transform.localRotation.x != 0f)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        if (transform.localEulerAngles.y <   maxLeft && transform.localEulerAngles.y > 270)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, maxLeft, transform.localEulerAngles.z);
        }
        if (transform.localEulerAngles.y > maxRight && transform.localEulerAngles.y < 90)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, maxRight, transform.localEulerAngles.z);
        }
        if (transform.localEulerAngles.z <  maxUp && transform.localEulerAngles.z > 270)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, maxUp);
        }
        if (transform.localEulerAngles.z > maxDown && transform.localEulerAngles.z < 90)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, maxDown);
        }

           
    }
    public void CanLook(bool look)
    {
        canLook = look;
    }

}
