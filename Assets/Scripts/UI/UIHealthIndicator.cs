using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIHealthIndicator : MonoBehaviour 
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI youCanText;

    private string leaveMoonRoomtext = "Press Q to jump slightly higher";
    private string leaveCarRoomText = "Press E to convert into a car"; 
    private Coroutine disableTextCoroutine; 
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<HealthModule>().OnUnityEventHealthChanged.AddListener(SetHealthText); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealthText(int healthValue)
    {
        healthText.text = healthValue.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MoonRoom"))
        {
            youCanText.text = "Your jumps are higher in this room.";
            EnableAndDisableText(); 
        }
        else if (other.CompareTag("CarRoom"))
        {
            youCanText.text = "Pressing E would turn you into a car."; 
            EnableAndDisableText();
        }
        else if (other.CompareTag("CheckPoint"))
        {
            youCanText.text = "Press R to interact"; 
            EnableAndDisableText();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MoonRoom"))
        {
            youCanText.text = leaveMoonRoomtext;
            EnableAndDisableText();
        }
        else if(other.CompareTag("CarRoom"))
        {
            youCanText.text = leaveCarRoomText; 
            EnableAndDisableText();
        }
        else if (other.CompareTag("CheckPoint"))
        {
            EnableAndDisableText() ;
        }
    }

    private void EnableAndDisableText()
    {
        youCanText.gameObject.SetActive(true); 

        if(disableTextCoroutine != null)
        {
            StopCoroutine(disableTextCoroutine);
        }

        disableTextCoroutine = StartCoroutine(DisableTextAfterSeconds(3f)); 
    }

    private IEnumerator DisableTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        youCanText.gameObject.SetActive(false); 
    }
}
