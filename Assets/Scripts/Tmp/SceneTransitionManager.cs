using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

namespace Tmp
{
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _fadeTime;

        [SerializeField] private AllPortalsSO _portalsSO;

        public UnityEvent EvtOnFadeInComplete;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Co_FadeIn());

            if (_portalsSO != null)
            {
                foreach (var portalSO in _portalsSO.portals)
                {
                    portalSO.OnEventRaised += FadeOutSceneTransition;

                }
            }
        }

        private void OnDestroy()
        {
            if (_portalsSO != null)
            {
                foreach (var portalSO in _portalsSO.portals)
                {
                    portalSO.OnEventRaised -= FadeOutSceneTransition;
                }
            }
        }

        IEnumerator Co_FadeIn()
        {
            float time = 0;
            while (time < _fadeTime)
            {
                yield return null;
                _image.color = new Color(0, 0, 0, 1 - time / _fadeTime);
                time += Time.deltaTime;
            }
            EvtOnFadeInComplete.Invoke();
        }

        IEnumerator Co_FadeOutAndSceneTransition(string sceneName)
        {
            float time = 0;
            while (time < _fadeTime)
            {
                yield return null;
                _image.color = new Color(0, 0, 0, 1 - time / _fadeTime);
                time += Time.deltaTime;
            }
            SceneManager.LoadScene(sceneName);
        }

        public void FadeOutSceneTransition(string sceneName)
        {
            StartCoroutine(Co_FadeOutAndSceneTransition(sceneName));
        }
    }
}