using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private float carAbilityTimer;

    [SerializeField] private float originalCarAbilityTimer; 

    public Vector3 rotationOffset;

    private void Start()
    {
        originalCarAbilityTimer = carAbilityTimer; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CarRoom"))
        {
            //Change the timer when the player enters the carroom 
            carAbilityTimer = 5;
            Debug.Log("Entered CarRoom, car ability timer set to " + carAbilityTimer + " seconds."); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("CarRoom"))
        {
            //Hopefully reset the timer to it's originally timer
            carAbilityTimer = originalCarAbilityTimer;
            Debug.Log("Exited CarRoom, car ability timer reset to " + carAbilityTimer + " seconds."); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(carAbility()); 
        }
    }
    private IEnumerator carAbility()
    {
        Quaternion carRotation = transform.rotation * Quaternion.Euler(rotationOffset);

        GameObject car = Instantiate(carPrefab, playerTransform.position, carRotation);
        playerPrefab.SetActive(false);
        yield return new WaitForSeconds(carAbilityTimer);
        playerPrefab.SetActive(true); 
        Destroy(car);
        //Create a manager that chooses between car and player 
    }
}
