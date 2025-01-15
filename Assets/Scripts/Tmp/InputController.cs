using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Tmp
{
    public class InputController : MonoBehaviour
    {
        [Header("Camera & Sensitivity")]
        //To direct the camera with the mouse
        [SerializeField] private Camera cam;
        [SerializeField] private float mouseSense;

        [Header("Modules")]
        [SerializeField] private ShootingModule shootingModule;
        [SerializeField] private MovementModule movementModule;
        [SerializeField] private JumpModule jumpModule;
        [SerializeField] private InteractModule interactModule;
        [SerializeField] private HealthModule healthModule;


        //[Header("Input System")]
        //[SerializeField] private InputActionAsset InputSystemActions;
        //private InputAction moveAction;
        //[SerializeField] private InputAction jumpAction;
        //private InputAction interactAction;
        //private InputAction rotateAction;
        //private InputAction shootAction;
        //[SerializeField] private Vector3 moveDirection;
        //[SerializeField] private Vector2 aimDirection;
        //public bool inputEnabled;

        private bool jumping;
        private bool canLookWithMouse;
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;


            //moveAction = InputSystemActions.FindActionMap("Player").FindAction("Move");
            //jumpAction = InputSystemActions.FindActionMap("Player").FindAction("Jump");
            //rotateAction = InputSystemActions.FindActionMap("Player").FindAction("Rotate");
            //interactAction = InputSystemActions.FindActionMap("Player").FindAction("Interact");
            //shootAction = InputSystemActions.FindActionMap("Player").FindAction("Shoot");

            //jumpAction.performed += OnJump;
            //interactAction.performed += OnInteract;
            //shootAction.performed += OnShoot;
        }
        // Start is called before the first frame update
        private void Start()
        {
            cam.transform.localEulerAngles = transform.localEulerAngles;
            Invoke("EnableMouseInput", 1f);

        }
        private void EnableMouseInput()
        {
            canLookWithMouse = true;
        }

        //private void OnEnable()
        //{
        //    InputSystemActions.FindActionMap("Player").Enable();
        //}

        //private void OnDisable()
        //{
        //    InputSystemActions.FindActionMap("Player").Disable();

        //    jumpAction.performed -= OnJump;
        //    interactAction.performed -= OnInteract;
        //    shootAction.performed -= OnShoot;
        //}

        //private void OnJump(InputAction.CallbackContext context)
        //{
        //    if (jumpModule != null && inputEnabled)
        //    {
        //        jumpModule.Jump();
        //    }
        //}

        //private void OnInteract(InputAction.CallbackContext context)
        //{
        //    if(interactModule != null && inputEnabled)
        //    {
        //        interactModule.InteractWithObject();
        //    }
            
        //}

        //private void OnShoot(InputAction.CallbackContext context)
        //{
        //    if (shootingModule != null && inputEnabled)
        //    {
        //        shootingModule.Shoot();
        //    }

        //}

        // Update is called once per frame
        void Update()
        {
            if (!canLookWithMouse)
            {
                return;
            }

            Vector3 moveDirection = Vector2.zero;
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.z = Input.GetAxisRaw("Vertical");

            Vector2 aimDirection = Vector2.zero;
            aimDirection.x = Input.GetAxisRaw("Mouse X") * mouseSense;
            aimDirection.y = -Input.GetAxisRaw("Mouse Y") * mouseSense;

            if (shootingModule != null && Input.GetMouseButtonDown(0))
            {
                shootingModule.Shoot();
            }
            if (interactModule != null && Input.GetKeyDown(KeyCode.E))
            {
                interactModule.InteractWithObject();
            }
            if (jumpModule != null && Input.GetKeyDown(KeyCode.Space))
            {
                jumpModule.Jump();
            }
            if (movementModule != null)
            {
                movementModule.MovePlayer(moveDirection);
                movementModule.RotatePlayer(aimDirection);
            }


            //Vector2 inputDirection = moveAction.ReadValue<Vector2>();
            //moveDirection = Vector2.zero;
            //moveDirection.x = inputDirection.x;
            //moveDirection.z = inputDirection.y;

            //Vector2 inputAim = rotateAction.ReadValue<Vector2>();
            //aimDirection = Vector2.zero;

            //aimDirection.x = inputAim.x * mouseSense;
            //aimDirection.y = -inputAim.y * mouseSense;

            //if (movementModule != null && inputEnabled)
            //{
            //    movementModule.MovePlayer(moveDirection);
            //    movementModule.RotatePlayer(aimDirection);
            //}
        }
    }
}