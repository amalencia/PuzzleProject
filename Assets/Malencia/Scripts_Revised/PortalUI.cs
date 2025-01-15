using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PortalUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _completionText;
    [SerializeField] private TextMeshProUGUI _numberText;
    //private bool _moveOnFromLevel;
    private float _timer;
    private IEnumerator _coroutine;


    public void PuzzleCompleted()
    {
        _completionText.text = "Puzzle Completed!";
        Invoke("EndLevelCountDown", 2f);
        
    }

    private void EndLevelCountDown()
    {
        _completionText.text = "Next Level in...";
        //_moveOnFromLevel = true;
        _coroutine = WaitAndPrint(5);
        StartCoroutine(_coroutine);
    }

    private IEnumerator WaitAndPrint(int waitTime)
    {
        _numberText.text = waitTime.ToString(); 
        int tracker = waitTime;
        float tracker2 = tracker;
        while (tracker >0)
        {
            tracker2 -= Time.deltaTime;
            if(tracker2 < (tracker -1))
            {
                tracker--;
                _numberText.text = tracker.ToString();
            }
            yield return null;   
        }

        Application.Quit();
    }
}
