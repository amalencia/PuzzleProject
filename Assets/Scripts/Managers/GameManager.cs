using UnityEngine.Events;
using UnityEngine;
using System.Collections;

namespace Tmp
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private InputController _playerInput;

        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private AllPortalsSO _allPortals;

        private Checkpoint _checkpoint;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            if (_gameSettings != null && !_gameSettings.isInitialized)
            {
                _allPortals.SetAllActive();
                _gameSettings.isInitialized = true;
            }
        }

        public void SetCheckpoint(Checkpoint checkpoint)
        {
            _checkpoint = checkpoint;
        }

        public void OnPlayerDied()
        {
            StartCoroutine(Co_RespawnPlayer());
        }

        IEnumerator Co_RespawnPlayer()
        {
            _playerInput.GetComponent<CharacterController>().enabled = false;
            _playerInput.transform.position = _checkpoint.transform.position;
            _playerInput.transform.rotation = _checkpoint.transform.rotation;
            yield return null;
            _playerInput.GetComponent<CharacterController>().enabled = true;
        }

        public void LockplayerInput()
        {
            _playerInput.enabled = false;
        }

        public void UnlockplayerInput()
        {
            _playerInput.enabled = true;
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running in a built application, quit the application
        Application.Quit();
#endif
        }
    }
}