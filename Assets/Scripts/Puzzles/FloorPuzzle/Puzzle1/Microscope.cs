
using UnityEngine;


public class Microscope : MonoBehaviour , IInteracterable
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject blackBoxCam;
    [SerializeField] private GameObject highlight;

    
    private void Awake()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<Ren.InputController>().gameObject;
        }
        if (blackBoxCam == null)
        {
            blackBoxCam = FindAnyObjectByType<CameraMovementModule>().gameObject;
        }
        blackBoxCam.GetComponent<CameraMovementModule>().OnExitMicroscope.AddListener(PlayerCam);
    }
    public void OnHoverEnter()
    {
        highlight.SetActive(true);
    }

    public void OnHoverExit()
    {
        highlight.SetActive(false);
    }

    public void OnInteract(InteractModule interactModule)
    {
        BlackBoxCam();
    }

    public void PlayerCam()
    {
        blackBoxCam.GetComponent<CameraMovementModule>().CanLook(false);
        blackBoxCam.SetActive(false);
        player.SetActive(true);
        Camera temp = player.GetComponentInChildren<Camera>();
        temp.enabled = true;
     
    }
    public void BlackBoxCam()
    {
        player.SetActive(false);
        blackBoxCam.SetActive(true);
        Camera temp = blackBoxCam.GetComponentInChildren<Camera>();
        temp.enabled = true;
        blackBoxCam.GetComponent<CameraMovementModule>().CanLook(true);
      
    }
 
}
