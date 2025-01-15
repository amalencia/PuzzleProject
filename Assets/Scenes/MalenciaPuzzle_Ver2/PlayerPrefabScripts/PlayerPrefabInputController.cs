using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerPrefabInputController : MonoBehaviour
{
    [Header("Modules - Player Prefab")]
    [SerializeField] private MoveModulePlayPref movementModule;
    [SerializeField] private JumpModulePlayPref jumpModule;
    [SerializeField] private InteractModulePlayPref interactModule;
    [SerializeField] private ShootModulePlayPref shootModule;
    [SerializeField] private PortalModulePlayPref portalModule;
    [SerializeField] private CarModulePlayPref carModule;
    

    [Header("Universal Variables")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private bool _portalGunEnabled;
    [SerializeField] private bool _gunEnabled;
    [SerializeField] private bool _carEnabled;
    //private bool canLookWithMouse;

    [Header("Input System")]
    [SerializeField] private InputActionAsset _inputActionAsset;
    private InputAction moveAction;
    private InputAction rotateAction;
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction shootAction;
    private InputAction portalBlueAction;
    private InputAction portalPinkAction;
    private InputAction laserAction;
    private InputAction activeCarAction;
    private InputAction restartLevelAction;
    private InputAction stopCutsceneAction;
    private InputAction longJumpAction;
    private InputAction leaveMicroscope;

    //Car Input Actions
    private InputAction accelerateAction;
    private InputAction breakAction;
    private InputAction turnAction;
    private InputAction exitCarAction;

    private void Awake()
    {
        //Fix Cursor going straight down on Game Start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //canLookWithMouse = true;
        //Assign InputAction values to the variables
        moveAction = _inputActionAsset.FindActionMap("Player").FindAction("Move");
        rotateAction = _inputActionAsset.FindActionMap("Player").FindAction("Rotate");
        jumpAction = _inputActionAsset.FindActionMap("Player").FindAction("Jump");
        interactAction = _inputActionAsset.FindActionMap("Player").FindAction("Interact");
        shootAction = _inputActionAsset.FindActionMap("Player").FindAction("Shoot");
        portalBlueAction = _inputActionAsset.FindActionMap("Player").FindAction("PortalBlue");
        portalPinkAction = _inputActionAsset.FindActionMap("Player").FindAction("PortalPink");
        laserAction = _inputActionAsset.FindActionMap("Player").FindAction("Laser");
        activeCarAction = _inputActionAsset.FindActionMap("Player").FindAction("ActivateCar");
        restartLevelAction = _inputActionAsset.FindActionMap("Player").FindAction("RestartLevel");
        longJumpAction = _inputActionAsset.FindActionMap("Player").FindAction("LongJump");
        stopCutsceneAction = _inputActionAsset.FindActionMap("Player").FindAction("SkipCutscene");
        leaveMicroscope = _inputActionAsset.FindActionMap("Player").FindAction("LeaveMicroscope");
    }

    private void Start()
    {
        _mainCamera.transform.localEulerAngles = transform.localEulerAngles;
        Invoke("EnableMouseInput", 1f);
    }

    private void EnableMouseInput()
    {
        //canLookWithMouse = true;
    }

    private void OnEnable()
    {
        _inputActionAsset.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        _inputActionAsset.FindActionMap("Player").Disable();
    }

    private void OnRestart(InputAction.CallbackContext context)
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene != SceneManager.GetSceneAt(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
