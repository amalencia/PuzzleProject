using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroFlythroughManager : MonoBehaviour
{
    [SerializeField] private Camera flyThroughCam;
    [SerializeField] private CutsceneStartType startType;
    [SerializeField] private PlayableDirector director;
    // Start is called before the first frame update
    public void Start()
    {
        director.Play();
    }
    public void OnCutsceneEnd()
    {
        Destroy(gameObject);
    }

public enum CutsceneStartType
{
    OnLevelStart, OnLevelFinish
}
}
