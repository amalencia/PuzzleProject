using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Tmp
{
    public class CutsceneManager : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _introDirector;
        [SerializeField] private PlayableDirector _inBetweenDirector;
        [SerializeField] private PlayableDirector _outtroDirector;

        [SerializeField] private AllPortalsSO _portalsSO;

        private PlayableDirector _currentDirector;

        void Start()
        {
            int n = CheckNumOfActivePortals();
            if (n == _portalsSO.portals.Length)
            {
                _currentDirector = _introDirector;
                _introDirector.gameObject.SetActive(true);
                _inBetweenDirector.gameObject.SetActive(false);
                _outtroDirector.gameObject.SetActive(false);
            }
            else if (n == 0)
            {
                _currentDirector = _outtroDirector;
                _introDirector.gameObject.SetActive(false);
                _inBetweenDirector.gameObject.SetActive(false);
                _outtroDirector.gameObject.SetActive(true);
            }
            else
            {
                _currentDirector = _inBetweenDirector;
                _introDirector.gameObject.SetActive(false);
                _inBetweenDirector.gameObject.SetActive(true);
                _outtroDirector.gameObject.SetActive(false);
            }
        }

        private int CheckNumOfActivePortals()
        {
            int n = 0;
            foreach (var portalSO in _portalsSO.portals)
            {
                if (portalSO.IsActive())
                {
                    n += 1;
                }
            }
            return n;
        }

        public void PlayCutscene()
        {
            _currentDirector.Play();
        }
    }
}