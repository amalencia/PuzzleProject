using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This script should only get input from the input class 
//andsend to our modules
namespace Ren
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
        [SerializeField] private CommandInteractor commandModule;
        [SerializeField] private HealthModule healthModule;

        //[Header("Input System")]
        //[SerializeField] private InputActionAsset InputSystemActions;
        //private InputAction moveAction;
        //private InputAction jumpAction;
        //private InputAction interactAction;
        //private InputAction rotateAction;
        //private InputAction shootAction;
        //[SerializeField] private Vector3 moveDirection;
        //[SerializeField] private Vector2 aimDirection;

        private bool jumping;
        private bool canLockWithMouse;
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;


            //moveAction = InputSystemActions.FindActionMap("Player").FindAction("Move");
            //jumpAction = InputSystemActions.FindActionMap("Player").FindAction("Jump");
            //rotateAction = InputSystemActions.FindActionMap("Player").FindAction("Rotate");
            //interactAction = InputSystemActions.FindActionMap("Player").FindAction("Interact");
            //shootAction = InputSystemActions.FindActionMap("Player").FindAction("Shoot");
        }
        // Start is called before the first frame update
        private void Start()
        {
            cam.transform.localEulerAngles = transform.localEulerAngles;
            //Invoke("EnableMouseInput", 1f);

            //jumpAction.performed += OnJump;
            //interactAction.performed += OnInteract;
            //shootAction.performed += OnShoot;
        }

        //private void OnEnable()
        //{
        //    InputSystemActions.FindActionMap("Player");
        //}

        //private void OnDisable()
        //{
        //    InputSystemActions.FindActionMap("Player");

        //    jumpAction.performed -= OnJump;
        //    interactAction.performed -= OnInteract;
        //    shootAction.performed -= OnShoot;
        //}
        private void EnableMouseInput()
        {
            //canLockWithMouse = true;
        }

        //private void OnJump(InputAction.CallbackContext context)
        //{
        //    if (jumpModule != null)
        //    {
        //        jumpModule.Jump();
        //    }
        //}

        //private void OnInteract(InputAction.CallbackContext context)
        //{
        //    interactModule.InteractWithObject();
        //}

        //private void OnShoot(InputAction.CallbackContext context)
        //{
        //    if (shootingModule != null)
        //    {
        //        shootingModule.Shoot();
        //    }

        //}

        // Update is called once per frame
        void Update()
        {
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
            if (commandModule != null && Input.GetMouseButtonDown(1))
            {
                commandModule.CreateCommand();
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

            //    aimDirection.x = inputAim.x * mouseSense;
            //    aimDirection.y = -inputAim.y * mouseSense;

            //if (movementModule != null)
            //{
            //    movementModule.MovePlayer(moveDirection);
            //    movementModule.RotatePlayer(aimDirection);
            //}
        }
    }
}