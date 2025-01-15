using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalInputController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float mouseSensitivity;

    [Header("Modules")]
    [SerializeField] private PortalShootingModule shootingModule;
    [SerializeField] private PortalMovementModule movementModule;
    [SerializeField] private PortalJumpModule jumpModule;
    [SerializeField] private PortalInteractModule interactModule;
    [SerializeField] private PortalRestartModule restartModule;
    [SerializeField] private bool _cutsceneIsPlaying;

    private bool jumping;
    private bool canLookAround = true;
    private Vector3 moveDirection;
    private Vector2 aimDirection;

    [Header("InputSystem")]
    [SerializeField] private InputActionAsset InputSystemActions;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction rotateAction;
    private InputAction portalGunBlue;
    private InputAction portalGunPink;
    private InputAction laserPointerAction;
    private InputAction interactAction;
    private InputAction restartLevelAction;
    private InputAction stopCutscene;
    [SerializeField] private GameObject cutscene;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;
    [SerializeField] private TextMeshProUGUI text4;
    [SerializeField] private TextMeshProUGUI tex51;
    [SerializeField] private TextMeshProUGUI skiptext;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //Assign Input Action values to the variables
        moveAction = InputSystemActions.FindActionMap("Player").FindAction("Move");
        jumpAction = InputSystemActions.FindActionMap("Player").FindAction("Jump");
        rotateAction = InputSystemActions.FindActionMap("Player").FindAction("Rotate");
        portalGunBlue = InputSystemActions.FindActionMap("Player").FindAction("PortalGunBlue");
        laserPointerAction = InputSystemActions.FindActionMap("Player").FindAction("LaserPointer");
        portalGunPink = InputSystemActions.FindActionMap("Player").FindAction("PortalGunPink");
        interactAction = InputSystemActions.FindActionMap("Player").FindAction("Interact");
        restartLevelAction = InputSystemActions.FindActionMap("Player").FindAction("RestartPuzzle");
        stopCutscene = InputSystemActions.FindActionMap("Player").FindAction("SkipCutscene");

        //subscribe to the necessary actions
        jumpAction.performed += OnJump;
        portalGunBlue.performed += OnPortalBlue;
        laserPointerAction.performed += OnLaser;
        portalGunPink.performed += OnPortalOrange;
        laserPointerAction.canceled += OffLaser;
        interactAction.performed += OnInteract;
        restartLevelAction.performed += OnRestart;
        stopCutscene.performed += SkipCutscene;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(restartModule != null && collision.gameObject.layer == 12)
        {
            restartModule.RestartLevel();
        }
    }
    private void OnRestart(InputAction.CallbackContext context)
    {
        if(restartModule != null && !_cutsceneIsPlaying)
        {
            restartModule.RestartLevel();
        }
        
    }

    private void SkipCutscene(InputAction.CallbackContext context)
    {
        cutscene.SetActive(false);
        text1.text = null;
        text2.text = null;
        text3.text = null;
        text4.text = null;
        tex51.text = null;
        skiptext.text = null;
        _cutsceneIsPlaying = false;

    }

    private void OnPortalOrange(InputAction.CallbackContext context)
    {
        if (shootingModule != null && !_cutsceneIsPlaying)
        {
            shootingModule.PortalGunPink();
        }
         
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        interactModule.InteractWithObject();
    }

    private void OffLaser(InputAction.CallbackContext context)
    {
        shootingModule.LaserOff();
    }

    private void OnPortalBlue(InputAction.CallbackContext context)
    {
        if (shootingModule != null && !_cutsceneIsPlaying)
        {
            shootingModule.PortalGunBlue();
        }
            
    }

    private void OnLaser(InputAction.CallbackContext context)
    {
        if(shootingModule != null && !_cutsceneIsPlaying)
        {
            shootingModule.LaserPointer();
        }
        
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (jumpModule != null && !_cutsceneIsPlaying)
        {
            jumpModule.Jump();
        }
    }

    private void OnEnable()
    {
        InputSystemActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        //InputSystemActions.FindActionMap("Player").Disable();
        //unsubscribe to prevent memory leaks

        jumpAction.performed -= OnJump;
        portalGunBlue.performed -= OnPortalBlue;
        laserPointerAction.performed -= OnLaser;
        portalGunPink.performed -= OnPortalOrange;
        laserPointerAction.canceled -= OffLaser;
        interactAction.performed -= OnInteract;
        restartLevelAction.performed -= OnRestart;
    }

    private void Update()
    {
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();
        moveDirection = Vector2.zero;
        moveDirection.x = inputDirection.x;
        moveDirection.z = inputDirection.y;

        Vector2 inputAim = rotateAction.ReadValue<Vector2>();
        aimDirection = Vector2.zero;

        if (canLookAround)
        {
            aimDirection.x = inputAim.x * mouseSensitivity;
            aimDirection.y = -inputAim.y * mouseSensitivity;
        }

        if (movementModule != null && !_cutsceneIsPlaying)
        {
            movementModule.MoveCharacter(moveDirection);
            movementModule.RotateCharacter(aimDirection*1.5f);
        }
    }

    public void LockThePlayer()
    {
        _cutsceneIsPlaying = true;
    }

    public void UnlockThePlayer()
    {
        _cutsceneIsPlaying=false;
    }

}
